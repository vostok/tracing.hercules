using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public class HerculesCustomOperationSpan : HerculesCommonSpan
    {
        public string TargetEnvironment { get; set; }

        public string TargetService { get; set; }

        public string CustomStatus { get; set; }

        public long? Size { get; set; }

        public override string ToString() =>
            $"{base.ToString()}, {nameof(TargetEnvironment)}: {TargetEnvironment}, {nameof(TargetService)}: {TargetService}, {nameof(CustomStatus)}: {CustomStatus}, {nameof(Size)}: {Size}";
    }
}