using JetBrains.Annotations;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public abstract class HerculesHttpTargettedSpan : HerculesHttpSpan
    {
        public string TargetEnvironment { get; set; }

        public string TargetService { get; set; }

        public override string ToString() =>
            $"{base.ToString()}, {nameof(TargetEnvironment)}: {TargetEnvironment}, {nameof(TargetService)}: {TargetService}";
    }
}