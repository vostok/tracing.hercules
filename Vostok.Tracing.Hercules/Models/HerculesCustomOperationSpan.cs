using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public class HerculesCustomOperationSpan : HerculesCustomSpan
    {
        public long? Size { get; set; }

        public override string ToString() =>
            $"{base.ToString()}, {nameof(Size)}: {Size}";
    }
}