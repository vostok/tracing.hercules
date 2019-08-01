using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Commons.Binary;
using Vostok.Commons.Collections;
using Vostok.Commons.Time;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Hercules.SpanAnnotations;
using Annotations = System.Collections.Generic.IReadOnlyDictionary<string, object>;

namespace Vostok.Tracing.Hercules
{
    [PublicAPI]
    public class HerculesSpanBuilder : DummyHerculesTagsBuilder, IHerculesEventBuilder<HerculesSpan>
    {
        private static readonly CachingTransform<IBinaryBufferReader, Dictionary<ByteArrayKey, Annotations>> CacheTransform = new CachingTransform<IBinaryBufferReader, Dictionary<ByteArrayKey, Annotations>>(
            _ => new Dictionary<ByteArrayKey, Annotations>());
        private static readonly DummyHerculesTagsBuilder DummyBuilder = new DummyHerculesTagsBuilder();

        private readonly IBinaryBufferReader reader;
        private readonly Dictionary<ByteArrayKey, Annotations> cache;

        private Guid? traceId;
        private Guid? spanId;
        private Guid? parentSpanId;
        private DateTime? beginTimestamp, endTimestamp;
        private TimeSpan beginOffset, endOffset;
        private IReadOnlyDictionary<string, object> annotations;

        public HerculesSpanBuilder(IBinaryBufferReader reader)
        {
            this.reader = reader;

            // Note(kungurtsev): deleting old cache with byte array buffer.
            cache = CacheTransform.Get(reader);
        }

        public IHerculesEventBuilder<HerculesSpan> SetTimestamp(DateTimeOffset timestamp)
        {
            return this;
        }

        public HerculesSpan BuildEvent()
        {
            return new HerculesSpan(
                traceId ?? throw new ArgumentOutOfRangeException(nameof(traceId), "Unexpected null traceId."),
                spanId ?? throw new ArgumentOutOfRangeException(nameof(spanId), "Unexpected null spanId."),
                parentSpanId,
                // ReSharper disable once NotResolvedInText
                BuildDateTimeOffset(beginTimestamp, beginOffset) ?? throw new ArgumentOutOfRangeException("Unexpected null beginTimestamp."),
                BuildDateTimeOffset(endTimestamp, endOffset),
                annotations);
        }

        public new IHerculesTagsBuilder AddValue(string key, Guid value)
        {
            switch (key)
            {
                case TagNames.TraceId:
                    traceId = value;
                    break;
                case TagNames.SpanId:
                    spanId = value;
                    break;
                case TagNames.ParentSpanId:
                    parentSpanId = value;
                    break;
            }

            return this;
        }

        public new IHerculesTagsBuilder AddValue(string key, long value)
        {
            switch (key)
            {
                case TagNames.BeginTimestampUtc:
                    beginTimestamp = EpochHelper.FromUnixTimeUtcTicks(value);
                    break;
                case TagNames.EndTimestampUtc:
                    endTimestamp = EpochHelper.FromUnixTimeUtcTicks(value);
                    break;
                case TagNames.BeginTimestampUtcOffset:
                    beginOffset = TimeSpan.FromTicks(value);
                    break;
                case TagNames.EndTimestampUtcOffset:
                    endOffset = TimeSpan.FromTicks(value);
                    break;
            }

            return this;
        }

        public new IHerculesTagsBuilder AddContainer(string key, Action<IHerculesTagsBuilder> valueBuilder)
        {
            if (key == TagNames.Annotations)
            {
                AddAnnotations(valueBuilder);
            }
            else
            {
                valueBuilder(DummyBuilder);
            }

            return this;
        }

        private void AddAnnotations(Action<IHerculesTagsBuilder> valueBuilder)
        {
            var startPosition = reader.Position;
            reader.SkipMode = true;
            valueBuilder(DummyBuilder);
            reader.SkipMode = false;
            var endPosition = reader.Position;

            var byteKey = new ByteArrayKey(reader.Buffer, startPosition, endPosition - startPosition);

            if (cache.TryGetValue(byteKey, out annotations))
                return;

            reader.Position = startPosition;

            var builder = new HerculesSpanAnnotationsBuilder();
            valueBuilder(builder);
            cache[byteKey] = annotations = builder.Dictionary;
        }

        private DateTimeOffset? BuildDateTimeOffset(DateTime? utcTimestamp, TimeSpan? utcOffset)
        {
            if (utcTimestamp == null || utcOffset == null)
                return null;
            return new DateTimeOffset(DateTime.SpecifyKind(utcTimestamp.Value + utcOffset.Value, DateTimeKind.Unspecified), utcOffset.Value);
        }
    }
}