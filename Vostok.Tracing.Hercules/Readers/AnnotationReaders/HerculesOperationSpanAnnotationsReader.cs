using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal class HerculesOperationSpanAnnotationsReader : HerculesCommonSpanAnnotationsReader
    {
        private readonly HerculesCustomOperationSpan span;

        public HerculesOperationSpanAnnotationsReader(HerculesCustomOperationSpan span) : base(span)
        {
            this.span = span;
        }

        public new IHerculesTagsBuilder AddValue(string key, string value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Custom.Operation.Status:
                    span.CustomStatus = value;
                    break;
                case WellKnownAnnotations.Custom.Operation.TargetEnvironment:
                    span.TargetEnvironment = value;
                    break;
                case WellKnownAnnotations.Custom.Operation.TargetService:
                    span.TargetService = value;
                    break;
                default:
                    base.AddValue(key, value);
                    break;
            }

            return this;
        }

        public new IHerculesTagsBuilder AddValue(string key, long value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Custom.Operation.Size:
                    span.Size = value;
                    break;
            }

            return this;
        }
    }
}