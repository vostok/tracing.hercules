using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public class HerculesHttpServerSpan : HerculesHttpSpan
    {
        public override string ToString() =>
            $"{base.ToString()}";
    }
}