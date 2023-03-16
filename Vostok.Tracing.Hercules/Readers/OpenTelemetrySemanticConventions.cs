using System.Diagnostics;

namespace Vostok.Tracing.Hercules.Readers;

internal static class OpenTelemetrySemanticConventions
{
    public const string ServiceName = "service.name";
    public const string DeploymentEnvironment = "deployment.environment";
    public const string HostName = "host.name";
    public const string HttpStatusCode = "http.status_code";
    public const string HttpUrl = "http.url";
    public const string HttpMethod = "http.method";
    public const string HttpClientIp = "http.client_ip";
    public const string HttpRequestContentLength = "http.request_content_length";
    public const string HttpResponseContentLength = "http.response_content_length";

    public const string ActivityKindServer = "Server"; // nameof(ActivityKind.Server);
    public const string ActivityKindClient = "Client"; // nameof(ActivityKind.Client);
    public const string ActivityStatusCodeOk = "Ok"; // nameof(ActivityStatusCode.Ok);
    public const string ActivityStatusCodeError = "Error"; // nameof(ActivityStatusCode.Error);
}