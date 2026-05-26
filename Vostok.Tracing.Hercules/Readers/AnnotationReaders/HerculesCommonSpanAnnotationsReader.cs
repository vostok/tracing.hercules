using System;
using System.Collections.Generic;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Models;
using Vostok.Tracing.Hercules.OpenTelemetry;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal abstract class HerculesCommonSpanAnnotationsReader : DummyHerculesTagsBuilder, IHerculesTagsBuilder
    {
        private static readonly DummyHerculesTagsBuilder DummyBuilder = new();

        private readonly HerculesCommonSpan span;

        protected HerculesCommonSpanAnnotationsReader(HerculesCommonSpan span) =>
            this.span = span;

        public new IHerculesTagsBuilder AddValue(string key, string value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Common.Application:
                case SemanticConventions.AttributeServiceName:
                    span.Application = value;
                    break;
                case WellKnownAnnotations.Common.Environment:
                case SemanticConventions.AttributeDeploymentEnvironmentName:
                case SemanticConventions.AttributeDeploymentEnvironment:
                    span.Environment = value;
                    break;
                case WellKnownAnnotations.Common.Host:
                case SemanticConventions.AttributeHostName:
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

        public new IHerculesTagsBuilder AddContainer(string key, Action<IHerculesTagsBuilder> valueBuilder)
        {
            valueBuilder(DummyBuilder);
            return this;
        }

        public new IHerculesTagsBuilder AddVectorOfContainers(string key, IReadOnlyList<Action<IHerculesTagsBuilder>> valueBuilders)
        {
            foreach (var valueBuilder in valueBuilders)
                valueBuilder(DummyBuilder);

            return this;
        }

        public new IHerculesTagsBuilder AddVectorOfContainers<TValue>(string key, IReadOnlyList<TValue> values, Action<IHerculesTagsBuilder, TValue> valueBuilder)
        {
            foreach (var value in values)
                valueBuilder(DummyBuilder, value);

            return this;
        }
    }
}