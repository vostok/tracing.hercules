using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public abstract class HerculesCustomSpan : HerculesCommonSpan
    {
        public string TargetEnvironment { get; set; }

        public string TargetService { get; set; }

        public string CustomStatus { get; set; }

        public long? RequestSize { get; set; }

        public long? ResponseSize { get; set; }

        public override string ToString() =>
            $"{nameof(TraceId)}: {TraceId},{nameof(BeginTimestamp)}: {BeginTimestamp}, {nameof(EndTimestamp)}: {EndTimestamp}, {nameof(Latency)}: {Latency}, {nameof(Application)}: {Application}, {nameof(Environment)}: {Environment}, {nameof(Host)}: {Host}, {nameof(TargetEnvironment)}: {TargetEnvironment}, {nameof(TargetService)}: {TargetService}, {nameof(Component)}: {Component}, {nameof(RequestSize)}: {RequestSize}, {nameof(ResponseSize)}: {ResponseSize}, {nameof(CustomStatus)}: {CustomStatus}, {nameof(WellKnownStatus)}: {WellKnownStatus}";
    }
}