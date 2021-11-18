using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal class HerculesHttpClientSpanAnnotationsReader : HerculesHttpTargetedSpanAnnotationsReader
    {
        public HerculesHttpClientSpanAnnotationsReader(HerculesHttpTargetedSpan span)
            : base(span)
        {
        }
    }
}