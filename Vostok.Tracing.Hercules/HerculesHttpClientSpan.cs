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

        public override string ToString() =>
            $"{nameof(TraceId)}: {TraceId}, {nameof(BeginTimestamp)}: {BeginTimestamp}, {nameof(EndTimestamp)}: {EndTimestamp}, {nameof(Latency)}: {Latency}, {nameof(Application)}: {Application}, {nameof(Host)}: {Host}, {nameof(TargetEnvironment)}: {TargetEnvironment}, {nameof(TargetService)}: {TargetService}, {nameof(RequestMethod)}: {RequestMethod}, {nameof(RequestSize)}: {RequestSize}, {nameof(ResponseCode)}: {ResponseCode}, {nameof(ResponseSize)}: {ResponseSize}, {nameof(Url)}: {Url}";
    }
}