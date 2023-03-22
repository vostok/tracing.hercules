using System;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Hercules.Helpers;
using Vostok.Tracing.Hercules.Models;
using Vostok.Tracing.Hercules.Readers.AnnotationReaders;

namespace Vostok.Tracing.Hercules.Readers
{
    public class HerculesHttpServerSpanReader : HerculesCommonSpanReader, IHerculesEventBuilder<HerculesHttpServerSpan>
    {
        private readonly IBinaryEventsReader reader;
        private static readonly DummyHerculesTagsBuilder DummyBuilder = new DummyHerculesTagsBuilder();
        private readonly HerculesHttpServerSpan span;
        
        public HerculesHttpServerSpanReader(IBinaryEventsReader reader)
            : this(new HerculesHttpServerSpan())
        {
            this.reader = reader;
        }

        public HerculesHttpServerSpanReader(HerculesHttpServerSpan span)
            : base(span)
        {
            this.span = span;
        }

        public new IHerculesEventBuilder<HerculesHttpServerSpan> SetTimestamp(DateTimeOffset timestamp) => this;

        public new HerculesHttpServerSpan BuildEvent() => span;
        
        public new IHerculesTagsBuilder AddContainer(string key, Action<IHerculesTagsBuilder> valueBuilder)
        {
            if (key == TagNames.Annotations)
            {
                span.Reader = reader;
                var annotationsReader = new HerculesHttpServerSpanAnnotationsReader(span);
                valueBuilder(annotationsReader);
                annotationsReader.FillUrl();
            }
            else
                valueBuilder(DummyBuilder);

            return this;
        }
    }
}