using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal class HerculesHttpClientSpanAnnotationsReader : HerculesHttpTargettedSpanAnnotationsReader
    {
        public HerculesHttpClientSpanAnnotationsReader(HerculesHttpTargettedSpan span)
            : base(span)
        {
        }
    }
}