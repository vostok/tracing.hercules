using System;
using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules
{
    [PublicAPI]
    public class HerculesHttpSpan
    {
        public Guid TraceId { get; set; }

        public bool IsClientSpan { get; set; }

        public DateTime UtcTimestamp { get; set; }

        public string OriginService { get; set; }

        public string OriginHost { get; set; }

        public long? DurationTicks { get; set; }

        public string ClientRequestMethod { get; set; }

        public int? ClientRequestBodySize { get; set; }

        public int? ClientResponseCode { get; set; }

        public int? ClientResponseBodySize { get; set; }
        
        public string TargetEnvironment { get; set; }

        public string TargetService { get; set; }

        public Uri Url { get; set; }
    }
}