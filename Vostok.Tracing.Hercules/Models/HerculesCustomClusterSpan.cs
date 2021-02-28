using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public class HerculesCustomClusterSpan : HerculesCustomSpan
    {
        public long? RequestSize { get; set; }

        public long? ResponseSize { get; set; }

        public override string ToString() =>
            $"{base.ToString()}, {nameof(RequestSize)}: {RequestSize}, {nameof(ResponseSize)}: {ResponseSize}";
    }
}