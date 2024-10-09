namespace Vostok.Tracing.Hercules.OpenTelemetry;

// note (ponomaryovigor, 09.10.2024): Temporarily copied from legacy OpenTelemetry.SemanticConventions package
// because it was deprecated and unlisted
internal static class SemanticConventions
{
    // Legacy
    public const string AttributeNetHostNameLegacy = "net.host.name";
    public const string AttributeNetHostPortLegacy = "net.host.port";
    public const string AttributeHttpMethodLegacy = "http.method";
    public const string AttributeHttpStatusCodeLegacy = "http.status_code";
    public const string AttributeHttpRequestContentLengthLegacy = "http.request_content_length";
    public const string AttributeHttpResponseContentLengthLegacy = "http.response_content_length";
    public const string AttributeHttpUrlLegacy = "http.url";
    public const string AttributeHttpSchemeLegacy = "http.scheme";
    public const string AttributeHttpTargetLegacy = "http.target";
    public const string AttributeHttpClientIpLegacy = "http.client_ip";

    // New
    public const string AttributeServiceName = "service.name";
    public const string AttributeDeploymentEnvironment = "deployment.environment";
    public const string AttributeHostName = "host.name";
}