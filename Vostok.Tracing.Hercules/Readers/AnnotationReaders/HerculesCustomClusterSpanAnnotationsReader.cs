using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal class HerculesCustomClusterSpanAnnotationsReader : HerculesCustomSpanAnnotationsReader
    {
        public HerculesCustomClusterSpanAnnotationsReader(HerculesCustomClusterSpan span)
            : base(span)
        {
        }
    }
}