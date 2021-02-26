using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal class HerculesCustomClientSpanAnnotationsReader : HerculesCustomSpanAnnotationsReader, IHerculesTagsBuilder
    {
        private readonly HerculesCustomClientSpan span;

        public HerculesCustomClientSpanAnnotationsReader(HerculesCustomClientSpan span)
            : base(span)
        {
            this.span = span;
        }

        public new IHerculesTagsBuilder AddValue(string key, string value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Custom.Request.Replica:
                    span.Replica = value;
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