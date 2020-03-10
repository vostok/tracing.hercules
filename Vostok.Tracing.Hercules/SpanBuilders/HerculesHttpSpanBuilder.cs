using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Events;

namespace Vostok.Tracing.Hercules.SpanBuilders
{
    [PublicAPI]
    public class HerculesHttpSpanBuilder : HerculesSpanBuilderBase, IHerculesEventBuilder<HerculesHttpSpan>
    {
        public HerculesHttpSpanBuilder(IBinaryBufferReader reader)
            : base(reader)
        {
        }

        private readonly HerculesHttpSpan span = new HerculesHttpSpan();
        
        public IHerculesEventBuilder<HerculesHttpSpan> SetTimestamp(DateTimeOffset timestamp) => this;

        public HerculesHttpSpan BuildEvent()
        {
            span.TraceId = TraceId ?? throw new ArgumentOutOfRangeException(nameof(TraceId), "Unexpected null traceId.");
            span.UtcTimestamp = BeginTimestamp.UtcDateTime;
            span.DurationTicks = (EndTimestamp - BeginTimestamp)?.Ticks;


            return span;
        }

        protected override void AddAnnotations(Action<IHerculesTagsBuilder> valueBuilder)
        {
            valueBuilder(new HerculesHttpSpanAnnotationsBuilder(span));
        }
    }
}