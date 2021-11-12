using System;
using JetBrains.Annotations;
using Vostok.Clusterclient.Core.Model;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public class HerculesHttpBaseSpan : HerculesCommonSpan
    {
        public string RequestMethod { get; set; }

        public long? RequestSize { get; set; }

        public long? ResponseSize { get; set; }

        public ResponseCode ResponseCode { get; set; }

        public Uri RequestUrl { get; set; }

        public override string ToString() =>
            $"{nameof(TraceId)}: {TraceId}, {nameof(BeginTimestamp)}: {BeginTimestamp}, {nameof(EndTimestamp)}: {EndTimestamp}, {nameof(Latency)}: {Latency}, {nameof(Application)}: {Application}, {nameof(Environment)}: {Environment}, {nameof(Host)}: {Host}, {nameof(RequestMethod)}: {RequestMethod}, {nameof(RequestSize)}: {RequestSize}, {nameof(ResponseSize)}: {ResponseSize}, {nameof(ResponseCode)}: {ResponseCode}, {nameof(RequestUrl)}: {RequestUrl}";
    }
}