// ReSharper disable once CheckNamespace
namespace OpenTelemetry.Resources;

// note (ponomaryovigor, 05.09.2024): Temporarily copied from legacy OpenTelemetry.SemanticConventions package
// because it was deprecated and unlisted
internal static class ResourceSemanticConventions
{
    public const string AttributeDeploymentEnvironment = "deployment.environment";
    public const string AttributeHostName = "host.name";
    public const string AttributeServiceName = "service.name";
}