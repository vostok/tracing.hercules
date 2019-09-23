using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Vostok.Commons.Time;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Helpers;

// ReSharper disable PossibleInvalidOperationException

namespace Vostok.Tracing.Hercules
{
    [PublicAPI]
    public class HerculesSpan : ISpan
    {
        private static readonly IReadOnlyDictionary<string, object> EmptyDictionary = new Dictionary<string, object>();

        public HerculesSpan(Guid traceId, Guid spanId, Guid? parentSpanId, DateTimeOffset beginTimestamp, DateTimeOffset? endTimestamp, IReadOnlyDictionary<string, object> annotations)
        {
            TraceId = traceId;
            SpanId = spanId;
            ParentSpanId = parentSpanId;
            BeginTimestamp = beginTimestamp;
            EndTimestamp = endTimestamp;
            Annotations = annotations ?? EmptyDictionary;
        }

        public HerculesSpan([NotNull] HerculesEvent @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            TraceId = @event.Tags.GetValue(TagNames.TraceId).AsGuid;
            SpanId = @event.Tags.GetValue(TagNames.SpanId).AsGuid;
            ParentSpanId = @event.Tags[TagNames.ParentSpanId]?.AsGuid;
            BeginTimestamp = ExtractTimestamp(@event, TagNames.BeginTimestampUtc, TagNames.BeginTimestampUtcOffset).Value;
            EndTimestamp = ExtractTimestamp(@event, TagNames.EndTimestampUtc, TagNames.EndTimestampUtcOffset);
            Annotations = @event.Tags[TagNames.Annotations]?.AsContainer.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Value) ?? EmptyDictionary;
        }

        public Guid TraceId { get; }
        public Guid SpanId { get; }
        public Guid? ParentSpanId { get; }
        public DateTimeOffset BeginTimestamp { get; }
        public DateTimeOffset? EndTimestamp { get; }
        public IReadOnlyDictionary<string, object> Annotations { get; }

        [CanBeNull]
        private static DateTimeOffset? ExtractTimestamp(HerculesEvent @event, string timestampTag, string offsetTag)
        {
            if (!@event.Tags.TryGetValue(timestampTag, out var timestampValue) ||
                !@event.Tags.TryGetValue(offsetTag, out var offsetValue))
            {
                return null;
            }

            var utcTimestamp = EpochHelper.FromUnixTimeUtcTicks(timestampValue.AsLong);
            var utcOffset = TimeSpan.FromTicks(offsetValue.AsLong);

            return new DateTimeOffset(DateTime.SpecifyKind(utcTimestamp + utcOffset, DateTimeKind.Unspecified), utcOffset);
        }
    }
}