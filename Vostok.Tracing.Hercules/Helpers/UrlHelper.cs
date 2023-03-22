using System;

namespace Vostok.Tracing.Hercules.Helpers;

internal static class UrlHelper
{
    public static Uri FromPieces(string? scheme, string? host, int? port, string path)
    {
        var question = path.IndexOf('?');
        var uriBuilder = new UriBuilder
        {
            Scheme = scheme!,
            Host = host!,
            Path = question == -1 ? path : path.Substring(0, question),
            Query = (question == -1 ? null : path.Substring(question + 1))!
        };
        if (port.HasValue)
            uriBuilder.Port = port.Value;
        return uriBuilder.Uri;
    }
}