using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public class HerculesHttpClientSpan : HerculesHttpTargettedSpan
    {
        public override string ToString() =>
            $"{base.ToString()}";
    }
}