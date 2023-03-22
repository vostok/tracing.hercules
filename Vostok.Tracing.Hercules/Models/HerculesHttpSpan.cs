using System;
using System.Net;
using JetBrains.Annotations;
using Vostok.Clusterclient.Core.Model;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public abstract class HerculesHttpSpan : HerculesCommonSpan
    {
        public string RequestMethod { get; set; }

        public long? RequestSize { get; set; }

        public long? ResponseSize { get; set; }

        public ResponseCode ResponseCode { get; set; }

        public Uri RequestUrl { get; set; }

        public string ClientName { get; set; }

        public IPAddress ClientAddress { get; set; }

        public override string ToString() =>
            $"{base.ToString()}, {nameof(RequestMethod)}: {RequestMethod}, {nameof(RequestSize)}: {RequestSize}, {nameof(ResponseSize)}: {ResponseSize}, {nameof(ResponseCode)}: {ResponseCode}, {nameof(RequestUrl)}: {RequestUrl}, {nameof(ClientName)}: {ClientName}, {nameof(ClientAddress)}: {ClientAddress}";
    }
}