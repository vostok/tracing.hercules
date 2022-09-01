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
        private readonly IBinaryEventsReader reader;
        private static readonly DummyHerculesTagsBuilder DummyBuilder = new DummyHerculesTagsBuilder();
        private readonly HerculesHttpClusterSpan span;

        public HerculesHttpClusterSpanReader(IBinaryEventsReader reader)
            : this(new HerculesHttpClusterSpan())
        {
            this.reader = reader;
        }

        private HerculesHttpClusterSpanReader(HerculesHttpClusterSpan span)
            : base(span) =>
            this.span = span;

        public new IHerculesEventBuilder<HerculesHttpClusterSpan> SetTimestamp(DateTimeOffset timestamp) => this;

        public new HerculesHttpClusterSpan BuildEvent() => span;

        public new IHerculesTagsBuilder AddContainer(string key, Action<IHerculesTagsBuilder> valueBuilder)
        {
            if (key == TagNames.Annotations)
            {
                span.Reader = reader;
                valueBuilder(new HerculesHttpClusterSpanAnnotationsReader(span));
            }
            else
                valueBuilder(DummyBuilder);

            return this;
        }
    }
}