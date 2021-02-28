using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal class HerculesCustomSpanAnnotationsReader : HerculesCommonSpanAnnotationsReader, IHerculesTagsBuilder
    {
        private readonly HerculesCustomSpan span;

        public HerculesCustomSpanAnnotationsReader(HerculesCustomSpan span)
            : base(span)
        {
            this.span = span;
        }

        public new IHerculesTagsBuilder AddValue(string key, string value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Custom.Response.Status:
                    span.CustomStatus = value;
                    break;
                case WellKnownAnnotations.Custom.Request.TargetEnvironment:
                case WellKnownAnnotations.Custom.Operation.TargetEnvironment:
                    span.TargetEnvironment = value;
                    break;
                case WellKnownAnnotations.Custom.Request.TargetService:
                case WellKnownAnnotations.Custom.Operation.TargetService:
                    span.TargetService = value;
                    break;
                default:
                    base.AddValue(key, value);
                    break;
            }

            return this;
        }
    }
}