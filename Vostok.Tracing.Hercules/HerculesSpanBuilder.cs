using System;
using JetBrains.Annotations;
using Vostok.Commons.Formatting;
using Vostok.Commons.Time;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Helpers;

namespace Vostok.Tracing.Hercules
{
    /// <summary>
    /// Converts <see cref="ISpan"/>s to <see cref="HerculesEvent"/>.
    /// </summary>
    [PublicAPI]
    public class HerculesSpanBuilder
    {
        public static HerculesEvent Build([NotNull] ISpan span, [CanBeNull] IFormatProvider formatProvider = null)
        {
            var builder = new HerculesEventBuilder();
            Build(span, builder, formatProvider);
            return builder.BuildEvent();
        }

        public static void Build([NotNull] ISpan span, [NotNull] IHerculesEventBuilder builder, [CanBeNull] IFormatProvider formatProvider = null)
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

        private static void BuildAnnotationsContainer(IHerculesTagsBuilder builder, ISpan span, IFormatProvider formatProvider)
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