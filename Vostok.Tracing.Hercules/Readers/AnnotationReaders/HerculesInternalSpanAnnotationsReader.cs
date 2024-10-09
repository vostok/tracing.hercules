#nullable enable
using Vostok.Clusterclient.Core.Model;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Helpers;
using Vostok.Tracing.Hercules.OpenTelemetry;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal class HerculesInternalSpanAnnotationsReader : DummyHerculesTagsBuilder, IHerculesTagsBuilder
    {
        public string? Kind;
        public string? Url;
        public string? UrlPath;
        public string? UrlHost;
        public string? UrlScheme;
        public int? UrlPort;
        public string? Replica;
        public string? Method;
        public string? TargetEnvironment;
        public string? TargetService;
        public string? Operation;
        public string? CustomStatus;
        public string? WellKnownStatus;
        public string? SourceApplication;
        public string? SourceEnvironment;
        public ResponseCode? Code;

        public void FillUrl()
        {
            if (Url == null && UrlHost != null && UrlPath != null)
                Url = UrlHelper.FromPieces(UrlScheme, UrlHost, UrlPort, UrlPath).ToString();
        }
        
        public new IHerculesTagsBuilder AddValue(string key, string value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Common.Kind:
                    Kind = value;
                    break;
                
                case WellKnownAnnotations.Http.Request.TargetEnvironment:
                case WellKnownAnnotations.Custom.Request.TargetEnvironment:
                case WellKnownAnnotations.Custom.Operation.TargetEnvironment:
                    TargetEnvironment = value;
                    break;
                
                case WellKnownAnnotations.Http.Request.TargetService:
                case WellKnownAnnotations.Custom.Operation.TargetService:
                case WellKnownAnnotations.Custom.Request.TargetService:
                    TargetService = value;
                    break;
                
                case WellKnownAnnotations.Custom.Response.Status:
                case WellKnownAnnotations.Custom.Operation.Status:
                    CustomStatus = value;
                    break;

                case WellKnownAnnotations.Http.Request.Url:
                case SemanticConventions.AttributeHttpUrlLegacy:
                case SemanticConventions.AttributeHttpUrlFull:
                    Url = value;
                    break;
                // case WellKnownAnnotations.Http.Request.Method: same as OTel attribute
                case SemanticConventions.AttributeHttpMethodLegacy:
                case SemanticConventions.AttributeHttpRequestMethod:
                    Method = value;
                    break;
                case WellKnownAnnotations.Custom.Request.Replica:
                    Replica = value;
                    break;
                case SemanticConventions.AttributeHttpTargetLegacy:
                case SemanticConventions.AttributeUrlPath:
                    UrlPath = value;
                    break;
                case SemanticConventions.AttributeNetHostNameLegacy:
                case SemanticConventions.AttributeServerAddress:
                    UrlHost = value;
                    break;
                case SemanticConventions.AttributeHttpSchemeLegacy:
                case SemanticConventions.AttributeUrlScheme:
                    UrlScheme = value;
                    break;

                case WellKnownAnnotations.Common.Operation:
                    Operation = value;
                    break;
                case WellKnownAnnotations.Common.Status:
                    WellKnownStatus = value;
                    break;
                case WellKnownAnnotations.Common.Application:
                case SemanticConventions.AttributeServiceName:
                    SourceApplication = value;
                    break;
                case WellKnownAnnotations.Common.Environment:
                case SemanticConventions.AttributeDeploymentEnvironment:
                    SourceEnvironment = value;
                    break;
            }

            return this;
        }

        // note (kungurtsev, 22.03.2023): grpc protocol has no int values:
        public new IHerculesTagsBuilder AddValue(string key, long value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Http.Response.Code:
                case SemanticConventions.AttributeHttpStatusCodeLegacy:
                case SemanticConventions.AttributeHttpResponseStatusCode:
                    Code = (ResponseCode)value;
                    break;
                case SemanticConventions.AttributeNetHostPortLegacy:
                case SemanticConventions.AttributeServerPort:
                    UrlPort = (int)value;
                    break;
            }

            return this;
        }
        
        public new IHerculesTagsBuilder AddValue(string key, int value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Http.Response.Code:
                case SemanticConventions.AttributeHttpStatusCodeLegacy:
                case SemanticConventions.AttributeHttpResponseStatusCode:
                    Code = (ResponseCode)value;
                    break;
                case SemanticConventions.AttributeNetHostPortLegacy:
                case SemanticConventions.AttributeServerPort:
                    UrlPort = value;
                    break;
            }

            return this;
        }
    }
}