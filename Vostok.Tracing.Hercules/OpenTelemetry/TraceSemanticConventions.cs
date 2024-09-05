// ReSharper disable once CheckNamespace
namespace OpenTelemetry.Trace;

// note (ponomaryovigor, 05.09.2024): Temporarily copied from legacy OpenTelemetry.SemanticConventions package
// because it was deprecated and unlisted
internal static class TraceSemanticConventions
{
    public const string AttributeNetHostName = "net.host.name";
    public const string AttributeNetHostPort = "net.host.port";
    public const string AttributeHttpMethod = "http.method";
    public const string AttributeHttpStatusCode = "http.status_code";
    public const string AttributeHttpRequestContentLength = "http.request_content_length";
    public const string AttributeHttpResponseContentLength = "http.response_content_length";
    public const string AttributeHttpUrl = "http.url";
    public const string AttributeHttpScheme = "http.scheme";
    public const string AttributeHttpTarget = "http.target";
    public const string AttributeHttpClientIp = "http.client_ip";
}