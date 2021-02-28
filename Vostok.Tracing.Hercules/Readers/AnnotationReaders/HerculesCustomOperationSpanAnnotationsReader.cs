using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal class HerculesCustomOperationSpanAnnotationsReader : HerculesCustomSpanAnnotationsReader
    {
        private readonly HerculesCustomOperationSpan span;

        public HerculesCustomOperationSpanAnnotationsReader(HerculesCustomOperationSpan span)
            : base(span)
        {
            this.span = span;
        }

        public new IHerculesTagsBuilder AddValue(string key, long value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Custom.Operation.Size:
                    span.Size = value;
                    break;
                default:
                    base.AddValue(key, value);
                    break;
            }

            return this;
        }
    }
}