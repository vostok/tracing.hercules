using System;
using JetBrains.Annotations;
using Vostok.Commons.Time;
using Vostok.Hercules.Client.Abstractions.Events;

namespace Vostok.Tracing.Hercules.SpanBuilders
{
    [PublicAPI]
    public abstract class HerculesSpanBuilderBase : DummyHerculesTagsBuilder
    {
        protected static readonly DummyHerculesTagsBuilder DummyBuilder = new DummyHerculesTagsBuilder();
        protected readonly IBinaryBufferReader Reader;

        protected Guid? TraceId;
        protected Guid? SpanId;
        protected Guid? ParentSpanId;
        // ReSharper disable once NotResolvedInText
        protected DateTimeOffset BeginTimestamp => BuildDateTimeOffset(beginTimestamp, beginOffset) ?? throw new ArgumentNullException("Unexpected null beginTimestamp.");
        protected DateTimeOffset? EndTimestamp => BuildDateTimeOffset(endTimestamp, endOffset);

        private DateTime? beginTimestamp, endTimestamp;
        private TimeSpan beginOffset, endOffset;

        protected HerculesSpanBuilderBase(IBinaryBufferReader reader)
        {
            Reader = reader;
        }

        public new IHerculesTagsBuilder AddValue(string key, Guid value)
        {
            switch (key)
            {
                case TagNames.TraceId:
                    TraceId = value;
                    break;
                case TagNames.SpanId:
                    SpanId = value;
                    break;
                case TagNames.ParentSpanId:
                    ParentSpanId = value;
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

        protected abstract void AddAnnotations(Action<IHerculesTagsBuilder> valueBuilder);

        private DateTimeOffset? BuildDateTimeOffset(DateTime? utcTimestamp, TimeSpan? utcOffset)
        {
            if (utcTimestamp == null || utcOffset == null)
                return null;
            return new DateTimeOffset(DateTime.SpecifyKind(utcTimestamp.Value + utcOffset.Value, DateTimeKind.Unspecified), utcOffset.Value);
        }
    }
}