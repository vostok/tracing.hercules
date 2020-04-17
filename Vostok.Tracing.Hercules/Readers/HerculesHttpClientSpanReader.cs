using System;
using JetBrains.Annotations;
using Vostok.Commons.Time;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Hercules.Helpers;

namespace Vostok.Tracing.Hercules.Readers
{
    [PublicAPI]
    public class HerculesHttpClientSpanReader : DummyHerculesTagsBuilder, IHerculesEventBuilder<HerculesHttpClientSpan>
    {
        private static readonly DummyHerculesTagsBuilder DummyBuilder = new DummyHerculesTagsBuilder();
        private readonly HerculesHttpClientSpan span = new HerculesHttpClientSpan();

        public IHerculesEventBuilder<HerculesHttpClientSpan> SetTimestamp(DateTimeOffset timestamp) => this;

        public HerculesHttpClientSpan BuildEvent() => span;

        public new IHerculesTagsBuilder AddValue(string key, Guid value)
        {
            switch (key)
            {
                case TagNames.TraceId:
                    span.TraceId = value;
                    break;
            }

            return this;
        }

        public new IHerculesTagsBuilder AddValue(string key, long value)
        {
            switch (key)
            {
                case TagNames.BeginTimestampUtc:
                    span.BeginTimestamp = EpochHelper.FromUnixTimeUtcTicks(value);
                    break;
                case TagNames.EndTimestampUtc:
                    span.EndTimestamp = EpochHelper.FromUnixTimeUtcTicks(value);
                    break;
                case TagNames.BeginTimestampUtcOffset:
                    span.BeginTimestamp = Timestamp(span.BeginTimestamp.DateTime, value);
                    break;
                case TagNames.EndTimestampUtcOffset:
                    span.EndTimestamp = Timestamp(span.EndTimestamp.DateTime, value);
                    break;
            }

            return this;
        }

        public new IHerculesTagsBuilder AddContainer(string key, Action<IHerculesTagsBuilder> valueBuilder)
        {
            valueBuilder(
                key == TagNames.Annotations
                    ? new HerculesHttpClientSpanAnnotationsReader(span)
                    : DummyBuilder);

            return this;
        }

        private DateTimeOffset Timestamp(DateTime utcTimestamp, long utcOffset)
        {
            var offset = TimeSpan.FromTicks(utcOffset);
            return new DateTimeOffset(DateTime.SpecifyKind(utcTimestamp + offset, DateTimeKind.Unspecified), offset);
        }
    }
}