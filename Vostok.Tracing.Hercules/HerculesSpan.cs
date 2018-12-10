using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Commons.Time;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;

// ReSharper disable PossibleInvalidOperationException

namespace Vostok.Tracing.Hercules
{
    [PublicAPI]
    public class HerculesSpan : ISpan
    {
        private readonly HerculesEvent @event;
        private volatile HerculesTags annotationTags;
        private volatile HerculesSpanAnnotations annotations;

        public HerculesSpan([NotNull] HerculesEvent @event)
        {
            this.@event = @event ?? throw new ArgumentNullException(nameof(@event));
        }

        public Guid TraceId
            => @event.Tags.GetValue(TagNames.TraceId).AsGuid;

        public Guid SpanId
            => @event.Tags.GetValue(TagNames.SpanId).AsGuid;

        public Guid? ParentSpanId
            => @event.Tags[TagNames.ParentSpanId]?.AsGuid;

        public DateTimeOffset BeginTimestamp
            => ExtractTimestamp(@event, TagNames.BeginTimestampUtc, TagNames.BeginTimestampUtcOffset).Value;

        public DateTimeOffset? EndTimestamp
            => ExtractTimestamp(@event, TagNames.EndTimestampUtc, TagNames.EndTimestampUtcOffset);

        public IReadOnlyDictionary<string, object> Annotations
            => annotations ?? (annotations = new HerculesSpanAnnotations(AnnotationTags));

        public HerculesTags AnnotationTags
            => annotationTags ?? (annotationTags = @event.Tags.GetValue(TagNames.Annotations).AsContainer);

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
