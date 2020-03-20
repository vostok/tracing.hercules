using System;
using JetBrains.Annotations;
using Vostok.Clusterclient.Core.Model;

namespace Vostok.Tracing.Hercules
{
    [PublicAPI]
    public class HerculesHttpClientSpan
    {
        public Guid TraceId { get; set; }

        public DateTimeOffset BeginTimestamp { get; set; }

        public DateTimeOffset EndTimestamp { get; set; }

        public TimeSpan Latency => EndTimestamp - BeginTimestamp;

        public string Application { get; set; }

        public string Host { get; set; }

        public string TargetEnvironment { get; set; }

        public string TargetService { get; set; }

        public string RequestMethod { get; set; }

        public int? RequestSize { get; set; }

        public ResponseCode ResponseCode { get; set; }

        public int? ResponseSize { get; set; }

        public Uri Url { get; set; }
    }
}