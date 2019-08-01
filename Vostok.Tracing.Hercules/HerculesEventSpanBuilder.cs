using System;
using JetBrains.Annotations;
using Vostok.Commons.Formatting;
using Vostok.Commons.Time;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Hercules
{
    /// <summary>
    /// Converts <see cref="ISpan"/>s to <see cref="HerculesEvent"/>.
    /// </summary>
    [PublicAPI]
    public class HerculesEventSpanBuilder
    {
        public static HerculesEvent Build([NotNull] ISpan span, IFormatProvider formatProvider)
        {
            var builder = new HerculesEventBuilder();
            Build(span, builder, formatProvider);
            return builder.BuildEvent();
        }

        public static void Build([NotNull] ISpan span, [NotNull] IHerculesEventBuilder builder, IFormatProvider formatProvider)
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

            builder.AddContainer(TagNames.Annotations, tagBuilder => BuildAnnotationsContainer(span, tagBuilder, formatProvider));
        }

        private static void BuildAnnotationsContainer(ISpan span, IHerculesTagsBuilder builder, IFormatProvider formatProvider)
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