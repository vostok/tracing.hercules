using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal class HerculesHttpClusterSpanAnnotationsReader : HerculesHttpSpanAnnotationsReader, IHerculesTagsBuilder
    {
        private readonly HerculesHttpClusterSpan span;

        public HerculesHttpClusterSpanAnnotationsReader(HerculesHttpClusterSpan span)
            : base(span)
        {
            this.span = span;
        }

        public new IHerculesTagsBuilder AddValue(string key, string value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Http.Cluster.Status:
                    span.Status = value;
                    break;
                case WellKnownAnnotations.Http.Cluster.Strategy:
                    span.Strategy = value;
                    break;
                default:
                    base.AddValue(key, value);
                    break;
            }

            return this;
        }
    }
}