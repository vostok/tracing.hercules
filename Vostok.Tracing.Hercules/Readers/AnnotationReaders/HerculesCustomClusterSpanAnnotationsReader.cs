using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal class HerculesCustomClusterSpanAnnotationsReader : HerculesCustomSpanAnnotationsReader
    {
        private readonly HerculesCustomClusterSpan span;

        public HerculesCustomClusterSpanAnnotationsReader(HerculesCustomClusterSpan span)
            : base(span)
        {
            this.span = span;
        }
        
        public new IHerculesTagsBuilder AddValue(string key, long value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Custom.Request.Size:
                    span.RequestSize = value;
                    break;
                case WellKnownAnnotations.Custom.Response.Size:
                    span.ResponseSize = value;
                    break;
                default:
                    base.AddValue(key, value);
                    break;
            }

            return this;
        }
    }
}