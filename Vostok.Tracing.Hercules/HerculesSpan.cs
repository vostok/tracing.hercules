using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Hercules
{
    [PublicAPI]
    public class HerculesSpan : ISpan
    {
        private readonly HerculesEvent @event;

        public HerculesSpan([NotNull] HerculesEvent @event)
        {
            this.@event = @event ?? throw new ArgumentNullException(nameof(@event));
        }

        public Guid TraceId => throw new NotImplementedException();

        public Guid SpanId => throw new NotImplementedException();

        public Guid? ParentSpanId => throw new NotImplementedException();

        public DateTimeOffset BeginTimestamp => throw new NotImplementedException();

        public DateTimeOffset? EndTimestamp => throw new NotImplementedException();

        public IReadOnlyDictionary<string, object> Annotations => throw new NotImplementedException();
    }
}