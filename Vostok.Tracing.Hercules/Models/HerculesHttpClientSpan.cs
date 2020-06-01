using JetBrains.Annotations;
using Vostok.Clusterclient.Core.Model;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public class HerculesHttpClientSpan : HerculesHttpSpan
    {
        public ResponseCode ResponseCode { get; set; }

        public override string ToString() =>
            $"{base.ToString()}, {nameof(ResponseCode)}: {ResponseCode}";
    }
}