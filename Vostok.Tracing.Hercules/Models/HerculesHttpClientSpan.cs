using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public class HerculesHttpClientSpan : HerculesHttpSpan
    {
        public override string ToString() =>
            $"{base.ToString()}";
    }
}