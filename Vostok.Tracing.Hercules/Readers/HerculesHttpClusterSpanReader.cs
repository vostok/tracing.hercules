using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Hercules.Helpers;
using Vostok.Tracing.Hercules.Models;
using Vostok.Tracing.Hercules.Readers.AnnotationReaders;

namespace Vostok.Tracing.Hercules.Readers
{
    [PublicAPI]
    public class HerculesHttpClusterSpanReader : HerculesCommonSpanReader, IHerculesEventBuilder<HerculesHttpClusterSpan>
    {
        private static readonly DummyHerculesTagsBuilder DummyBuilder = new DummyHerculesTagsBuilder();
        private readonly HerculesHttpClusterSpan span;

        public HerculesHttpClusterSpanReader()
            : this(new HerculesHttpClusterSpan())
        {
        }

        private HerculesHttpClusterSpanReader(HerculesHttpClusterSpan span)
            : base(span) =>
            this.span = span;

        public new IHerculesEventBuilder<HerculesHttpClusterSpan> SetTimestamp(DateTimeOffset timestamp) => this;

        public new HerculesHttpClusterSpan BuildEvent() => span;

        public new IHerculesTagsBuilder AddContainer(string key, Action<IHerculesTagsBuilder> valueBuilder)
        {
            valueBuilder(
                key == TagNames.Annotations
                    ? new HerculesHttpClusterSpanAnnotationsReader(span)
                    : DummyBuilder);

            return this;
        }
    }
}