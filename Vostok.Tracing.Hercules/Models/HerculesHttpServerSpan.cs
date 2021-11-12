using System.Net;
using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public class HerculesHttpServerSpan : HerculesHttpBaseSpan
    {
        public string ClientName { get; set; }

        public IPAddress ClientAddress { get; set; }

        public override string ToString() =>
            $"{base.ToString()}, {nameof(ClientName)}: {ClientName}, {nameof(ClientAddress)}: {ClientAddress}";
    }
}