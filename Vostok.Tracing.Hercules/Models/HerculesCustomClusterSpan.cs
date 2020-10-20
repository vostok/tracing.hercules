using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public class HerculesCustomClusterSpan: HerculesCustomSpan
    {
        public override string ToString() =>
            $"{base.ToString()}";
    }
}