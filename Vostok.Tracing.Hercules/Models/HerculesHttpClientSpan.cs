using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public class HerculesHttpClientSpan : HerculesHttpTargetedSpan
    {
        public override string ToString() =>
            $"{base.ToString()}";
    }
}