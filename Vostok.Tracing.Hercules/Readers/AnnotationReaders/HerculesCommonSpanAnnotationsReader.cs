using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal abstract class HerculesCommonSpanAnnotationsReader : DummyHerculesTagsBuilder, IHerculesTagsBuilder
    {
        private readonly HerculesCommonSpan span;

        protected HerculesCommonSpanAnnotationsReader(HerculesCommonSpan span) =>
            this.span = span;

        public new IHerculesTagsBuilder AddValue(string key, string value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Common.Application:
                case ResourceSemanticConventions.AttributeServiceName:
                    span.Application = value;
                    break;
                case WellKnownAnnotations.Common.Environment:
                case ResourceSemanticConventions.AttributeDeploymentEnvironment:
                    span.Environment = value;
                    break;
                case WellKnownAnnotations.Common.Host:
                case ResourceSemanticConventions.AttributeHostName:
                    span.Host = value;
                    break;
                case WellKnownAnnotations.Common.Operation:
                case "name":
                    span.Operation = value;
                    break;
                case WellKnownAnnotations.Common.Status:
                    span.WellKnownStatus = value;
                    break;
                case WellKnownAnnotations.Common.Component:
                    span.Component = value;
                    break;
                default:
                    base.AddValue(key, value);
                    break;
            }

            return this;
        }
    }
}