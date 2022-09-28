using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Hercules.Helpers;
using Vostok.Tracing.Hercules.Models;
using Vostok.Tracing.Hercules.Readers.AnnotationReaders;

namespace Vostok.Tracing.Hercules.Readers
{
    [PublicAPI]
    public class HerculesCustomClusterSpanReader : HerculesCommonSpanReader, IHerculesEventBuilder<HerculesCustomClusterSpan>
    {
        private readonly IBinaryEventsReader reader;
        private static readonly DummyHerculesTagsBuilder DummyBuilder = new DummyHerculesTagsBuilder();
        private readonly HerculesCustomClusterSpan span;

        public HerculesCustomClusterSpanReader(IBinaryEventsReader reader)
            : this(new HerculesCustomClusterSpan())
        {
            this.reader = reader;
        }

        private HerculesCustomClusterSpanReader(HerculesCustomClusterSpan span)
            : base(span) =>
            this.span = span;

        public new IHerculesEventBuilder<HerculesCustomClusterSpan> SetTimestamp(DateTimeOffset timestamp) => this;

        public new HerculesCustomClusterSpan BuildEvent() => span;

        public new IHerculesTagsBuilder AddContainer(string key, Action<IHerculesTagsBuilder> valueBuilder)
        {
            if (key == TagNames.Annotations)
            {
                span.Reader = reader;
                valueBuilder(new HerculesCustomClusterSpanAnnotationsReader(span));
            }
            else
                valueBuilder(DummyBuilder);

            return this;
        }
    }
}