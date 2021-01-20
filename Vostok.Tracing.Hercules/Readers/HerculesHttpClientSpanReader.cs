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
        private static readonly DummyHerculesTagsBuilder DummyBuilder = new DummyHerculesTagsBuilder();
        private readonly HerculesHttpClientSpan span;

        public HerculesHttpClientSpanReader()
            : this(new HerculesHttpClientSpan())
        {
        }

        private HerculesHttpClientSpanReader(HerculesHttpClientSpan span)
            : base(span) =>
            this.span = span;

        public new IHerculesEventBuilder<HerculesHttpClientSpan> SetTimestamp(DateTimeOffset timestamp) => this;

        public new HerculesHttpClientSpan BuildEvent() => span;

        public new IHerculesTagsBuilder AddContainer(string key, Action<IHerculesTagsBuilder> valueBuilder)
        {
            valueBuilder(
                key == TagNames.Annotations
                    ? new HerculesHttpClientSpanAnnotationsReader(span)
                    : DummyBuilder);

            return this;
        }
    }
}