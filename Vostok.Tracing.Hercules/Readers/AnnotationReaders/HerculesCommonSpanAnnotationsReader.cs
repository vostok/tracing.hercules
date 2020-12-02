using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal abstract class HerculesCommonSpanAnnotationsReader: DummyHerculesTagsBuilder, IHerculesTagsBuilder
    {
        private readonly HerculesCommonSpan span;

        protected HerculesCommonSpanAnnotationsReader(HerculesCommonSpan span) =>
            this.span = span;

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
                case WellKnownAnnotations.Common.Operation:
                    span.Operation = value;
                    break;
                case WellKnownAnnotations.Common.Status:
                    span.WellKnownStatus = value;
                    break;
                case WellKnownAnnotations.Common.Component:
                    span.Component = value;
                    break;
            }

            return this;
        }

    }
}