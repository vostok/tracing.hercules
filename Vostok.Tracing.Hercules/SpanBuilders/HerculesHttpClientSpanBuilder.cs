using System;
using JetBrains.Annotations;
using Vostok.Commons.Time;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Hercules.Helpers;

namespace Vostok.Tracing.Hercules.SpanBuilders
{
    [PublicAPI]
    public class HerculesHttpClientSpanBuilder : DummyHerculesTagsBuilder, IHerculesEventBuilder<HerculesHttpClientSpan>
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
                    span.BeginTimestamp = new DateTimeOffset(span.BeginTimestamp.DateTime, TimeSpan.FromTicks(value));
                    break;
                case TagNames.EndTimestampUtcOffset:
                    span.EndTimestamp = new DateTimeOffset(span.EndTimestamp.DateTime, TimeSpan.FromTicks(value));
                    break;
            }

            return this;
        }

        public new IHerculesTagsBuilder AddContainer(string key, Action<IHerculesTagsBuilder> valueBuilder)
        {
            valueBuilder(
                key == TagNames.Annotations
                    ? new HerculesHttpClientSpanAnnotationsBuilder(span)
                    : DummyBuilder);

            return this;
        }
    }
}