using System;
using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public abstract class HerculesCustomSpan
    {
        public Guid TraceId { get; set; }

        public DateTimeOffset BeginTimestamp { get; set; }

        public DateTimeOffset EndTimestamp { get; set; }
        
        public TimeSpan Latency => EndTimestamp - BeginTimestamp;

        public string Component { get; set; }

        public string Application { get; set; }

        public string Environment { get; set; }

        public string Host { get; set; }

        public string TargetEnvironment { get; set; }

        public string TargetService { get; set; }

        public string ResponseStatus { get; set; }

        public string WellKnownResponseStatus { get; set; }

        public long? RequestSize { get; set; }

        public long? ResponseSize { get; set; }

        public override string ToString() =>
            $"{nameof(TraceId)}: {TraceId},{nameof(BeginTimestamp)}: {BeginTimestamp}, {nameof(EndTimestamp)}: {EndTimestamp}, {nameof(Latency)}: {Latency}, {nameof(Application)}: {Application}, {nameof(Environment)}: {Environment}, {nameof(Host)}: {Host}, {nameof(TargetEnvironment)}: {TargetEnvironment}, {nameof(TargetService)}: {TargetService}, {nameof(Component)}: {Component}, {nameof(RequestSize)}: {RequestSize}, {nameof(ResponseSize)}: {ResponseSize}, {nameof(ResponseStatus)}: {ResponseStatus}, {nameof(WellKnownResponseStatus)}: {WellKnownResponseStatus}";
    }
}