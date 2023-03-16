#nullable enable
using Vostok.Clusterclient.Core.Model;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal class HerculesInternalSpanAnnotationsReader : DummyHerculesTagsBuilder, IHerculesTagsBuilder
    {
        public string? Kind;
        public string? Url;
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
                case OpenTelemetrySemanticConventions.HttpUrl:
                    Url = value;
                    break;
                case WellKnownAnnotations.Http.Request.Method:
                case OpenTelemetrySemanticConventions.HttpMethod:
                    Method = value;
                    break;
                case WellKnownAnnotations.Custom.Request.Replica:
                    Replica = value;
                    break;
                
                case WellKnownAnnotations.Common.Operation:
                    Operation = value;
                    break;
                case WellKnownAnnotations.Common.Status:
                    WellKnownStatus = value;
                    break;
                case WellKnownAnnotations.Common.Application:
                case OpenTelemetrySemanticConventions.ServiceName:
                    SourceApplication = value;
                    break;
                case WellKnownAnnotations.Common.Environment:
                case OpenTelemetrySemanticConventions.DeploymentEnvironment:
                    SourceEnvironment = value;
                    break;
            }

            return this;
        }

        public new IHerculesTagsBuilder AddValue(string key, int value)
        {
            if (key is WellKnownAnnotations.Http.Response.Code or OpenTelemetrySemanticConventions.HttpStatusCode)
                Code = (ResponseCode)value;

            return this;
        }
    }
}