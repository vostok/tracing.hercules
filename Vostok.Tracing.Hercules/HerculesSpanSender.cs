using System;
using JetBrains.Annotations;
using Vostok.Commons.Formatting;
using Vostok.Commons.Time;
using Vostok.Hercules.Client.Abstractions;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Hercules
{
    /// <summary>
    /// An implementation of <see cref="ISpanSender"/> based on <see cref="IHerculesSink"/> from Hercules client library.
    /// </summary>
    [PublicAPI]
    public class HerculesSpanSender : ISpanSender
    {
        private readonly HerculesSpanSenderConfig config;

        public HerculesSpanSender([NotNull] HerculesSpanSenderConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void Send(ISpan span)
        {
            config.Sink.Put(config.Stream, builder => BuildHerculesEvent(builder, span));
        }

        private void BuildHerculesEvent(IHerculesEventBuilder builder, ISpan span)
        {
            builder.SetTimestamp(span.EndTimestamp ?? span.BeginTimestamp);

            builder
                .AddValue(TagNames.TraceId, span.TraceId)
                .AddValue(TagNames.SpanId, span.SpanId)
                .AddValue(TagNames.BeginTimestampUtc, EpochHelper.ToUnixTimeUtcTicks(span.BeginTimestamp.UtcDateTime))
                .AddValue(TagNames.BeginTimestampUtcOffset, span.BeginTimestamp.Offset.Ticks);

            if (span.ParentSpanId.HasValue)
            {
                builder.AddValue(TagNames.ParentSpanId, span.ParentSpanId.Value);
            }

            if (span.EndTimestamp.HasValue)
            {
                builder.AddValue(TagNames.EndTimestampUtc, EpochHelper.ToUnixTimeUtcTicks(span.EndTimestamp.Value.UtcDateTime));
                builder.AddValue(TagNames.EndTimestampUtcOffset, span.EndTimestamp.Value.Offset.Ticks);
            }

            builder.AddContainer(TagNames.Annotations, tagBuilder => BuildAnnotationsContainer(tagBuilder, span));
        }

        private void BuildAnnotationsContainer(IHerculesTagsBuilder builder, ISpan span)
        {
            foreach (var pair in span.Annotations)
            {
                if (builder.TryAddObject(pair.Key, pair.Value))
                    continue;

                builder.AddValue(pair.Key, ObjectValueFormatter.Format(pair.Value, formatProvider: config.FormatProvider));
            }
        }
    }
}
