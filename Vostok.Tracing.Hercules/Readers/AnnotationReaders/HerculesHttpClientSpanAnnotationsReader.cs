using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal class HerculesHttpClientSpanAnnotationsReader : HerculesHttpSpanAnnotationsReader
    {
        public HerculesHttpClientSpanAnnotationsReader(HerculesHttpSpan span) : base(span) {}
    }
}