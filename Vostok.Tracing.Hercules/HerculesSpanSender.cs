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
        private readonly Func<HerculesSpanSenderSettings> settingsProvider;

        public HerculesSpanSender([NotNull] Func<HerculesSpanSenderSettings> settingsProvider)
        {
            this.settingsProvider = settingsProvider ?? throw new ArgumentNullException(nameof(settingsProvider));
        }

        public HerculesSpanSender([NotNull] HerculesSpanSenderSettings settings)
            : this(() => settings)
        {
        }

        public void Send(ISpan span)
        {
            var settings = settingsProvider();

            settings.Sink.Put(settings.Stream, builder => BuildHerculesEvent(builder, span, settings.FormatProvider));
        }

        private void BuildHerculesEvent(IHerculesEventBuilder builder, ISpan span, IFormatProvider formatProvider)
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

            builder.AddContainer(TagNames.Annotations, tagBuilder => BuildAnnotationsContainer(tagBuilder, span, formatProvider));
        }

        private void BuildAnnotationsContainer(IHerculesTagsBuilder builder, ISpan span, IFormatProvider formatProvider)
        {
            foreach (var pair in span.Annotations)
            {
                if (builder.TryAddObject(pair.Key, pair.Value))
                    continue;

                builder.AddValue(pair.Key, ObjectValueFormatter.Format(pair.Value, formatProvider: formatProvider));
            }
        }
    }
}