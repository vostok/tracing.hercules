using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers
{
    public class HerculesCustomClientSpanAnnotationsReader: DummyHerculesTagsBuilder, IHerculesTagsBuilder
    {
        private readonly HerculesCustomClientSpan span;

        public HerculesCustomClientSpanAnnotationsReader(HerculesCustomClientSpan span)
        {
            this.span = span;
        }

        public new IHerculesTagsBuilder AddValue(string key, string value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Common.Application:
                    span.Application = value;
                    break;
                case WellKnownAnnotations.Common.Environment:
                    span.Environment = value;
                    break;
                case WellKnownAnnotations.Common.Host:
                    span.Host = value;
                    break;
                case WellKnownAnnotations.Custom.Response.Status:
                    span.ResponseStatus = value;
                    break;
                case WellKnownAnnotations.Common.Status:
                    span.WellKnownResponseStatus = value;
                    break;
                case WellKnownAnnotations.Custom.Request.Replica:
                    span.Replica = value;
                    break;
                case WellKnownAnnotations.Common.Component:
                    span.Component = value;
                    break;
                case WellKnownAnnotations.Custom.Request.TargetEnvironment:
                    span.TargetEnvironment = value;
                    break;
                case WellKnownAnnotations.Custom.Request.TargetService:
                    span.TargetService = value;
                    break;
            }

            return this;
        }

        public new IHerculesTagsBuilder AddValue(string key, long value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Custom.Request.Size:
                    span.RequestSize = value;
                    break;
                case WellKnownAnnotations.Custom.Response.Size:
                    span.ResponseSize = value;
                    break;
            }

            return this;
        }
    }
}