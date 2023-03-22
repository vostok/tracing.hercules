using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public class HerculesHttpServerSpan : HerculesHttpSpan
    {
        internal string? RequestUrlPath { get; set; }
        internal string? RequestUrlHost { get; set; }
        internal string? RequestUrlScheme { get; set; }
        internal int? RequestUrlPort { get; set; }
        
        public override string ToString() =>
            $"{base.ToString()}";
    }
}