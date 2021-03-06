using System;
using JetBrains.Annotations;
using Vostok.Commons.Time;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Hercules.Helpers;
using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers
{
    [PublicAPI]
    public abstract class HerculesCommonSpanReader : DummyHerculesTagsBuilder, IHerculesEventBuilder<HerculesCommonSpan>
    {
        private static readonly DummyHerculesTagsBuilder DummyBuilder = new DummyHerculesTagsBuilder();
        private readonly HerculesCommonSpan span;
        protected HerculesCommonSpanReader(HerculesCommonSpan span) => this.span = span;

        public IHerculesEventBuilder<HerculesCommonSpan> SetTimestamp(DateTimeOffset timestamp) => this;

        public HerculesCommonSpan BuildEvent() => span;

        public new IHerculesTagsBuilder AddValue(string key, Guid value)
        {
            switch (key)
            {
                case TagNames.TraceId:
                    span.TraceId = value;
                    break;
                default:
                    base.AddValue(key, value);
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
                    span.BeginTimestamp = DatetimeHelper.Timestamp(span.BeginTimestamp.DateTime, value);
                    break;
                case TagNames.EndTimestampUtcOffset:
                    span.EndTimestamp = DatetimeHelper.Timestamp(span.EndTimestamp.DateTime, value);
                    break;
                default:
                    base.AddValue(key, value);
                    break;
            }

            return this;
        }
    }
}