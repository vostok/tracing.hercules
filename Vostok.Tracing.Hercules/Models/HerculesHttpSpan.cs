using System;
using JetBrains.Annotations;
using Vostok.Clusterclient.Core.Model;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public abstract class HerculesHttpSpan: HerculesCommonSpan
    {
        public string TargetEnvironment { get; set; }

        public string TargetService { get; set; }

        public string RequestMethod { get; set; }

        public long? RequestSize { get; set; }

        public long? ResponseSize { get; set; }

        public ResponseCode ResponseCode { get; set; }
        
        public Uri RequestUrl { get; set; }

        public override string ToString() =>
            $"{nameof(TraceId)}: {TraceId}, {nameof(BeginTimestamp)}: {BeginTimestamp}, {nameof(EndTimestamp)}: {EndTimestamp}, {nameof(Latency)}: {Latency}, {nameof(Application)}: {Application}, {nameof(Environment)}: {Environment}, {nameof(Host)}: {Host}, {nameof(TargetEnvironment)}: {TargetEnvironment}, {nameof(TargetService)}: {TargetService}, {nameof(RequestMethod)}: {RequestMethod}, {nameof(RequestSize)}: {RequestSize}, {nameof(ResponseSize)}: {ResponseSize}, {nameof(ResponseCode)}: {ResponseCode}, {nameof(RequestUrl)}: {RequestUrl}";
    }
}