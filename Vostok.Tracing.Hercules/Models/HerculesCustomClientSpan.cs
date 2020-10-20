using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public class HerculesCustomClientSpan: HerculesCustomSpan
    {
        public string Replica;

        public override string ToString() =>
            $"{base.ToString()}, {nameof(Replica)}: {Replica}";
    }
}