using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Hercules.Helpers;
using Vostok.Tracing.Hercules.Models;
using Vostok.Tracing.Hercules.Readers.AnnotationReaders;

namespace Vostok.Tracing.Hercules.Readers
{
    [PublicAPI]
    public class HerculesHttpClientSpanReader : HerculesCommonSpanReader, IHerculesEventBuilder<HerculesHttpClientSpan>
    {
        private readonly IBinaryEventsReader reader;
        private static readonly DummyHerculesTagsBuilder DummyBuilder = new DummyHerculesTagsBuilder();
        private readonly HerculesHttpClientSpan span;

        public HerculesHttpClientSpanReader(IBinaryEventsReader reader)
            : this(new HerculesHttpClientSpan())
        {
            this.reader = reader;
        }

        private HerculesHttpClientSpanReader(HerculesHttpClientSpan span)
            : base(span) =>
            this.span = span;

        public new IHerculesEventBuilder<HerculesHttpClientSpan> SetTimestamp(DateTimeOffset timestamp) => this;

        public new HerculesHttpClientSpan BuildEvent() => span;

        public new IHerculesTagsBuilder AddContainer(string key, Action<IHerculesTagsBuilder> valueBuilder)
        {
            if (key == TagNames.Annotations)
            {
                span.Reader = reader;
                valueBuilder(new HerculesHttpClientSpanAnnotationsReader(span));
            }
            else
                valueBuilder(DummyBuilder);
            
            

            return this;
        }
    }
}