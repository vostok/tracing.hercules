using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public class HerculesHttpClusterSpan : HerculesHttpTargettedSpan
    {
        public string Status { get; set; }

        public string Strategy { get; set; }

        public override string ToString() =>
            $"{base.ToString()}, {nameof(Status)}: {Status}, {nameof(Strategy)}: {Strategy}";
    }
}