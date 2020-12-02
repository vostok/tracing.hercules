using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Hercules.Helpers;
using Vostok.Tracing.Hercules.Models;
using Vostok.Tracing.Hercules.Readers.AnnotationReaders;

namespace Vostok.Tracing.Hercules.Readers
{
    [PublicAPI]
    public class HerculesCustomClientSpanReader: HerculesCommonSpanReader, IHerculesEventBuilder<HerculesCustomClientSpan>
    {
        private static readonly DummyHerculesTagsBuilder DummyBuilder = new DummyHerculesTagsBuilder();
        private readonly HerculesCustomClientSpan span;
        
        public HerculesCustomClientSpanReader(): this(new HerculesCustomClientSpan()) {}
        
        private HerculesCustomClientSpanReader(HerculesCustomClientSpan span) : base(span) =>
            this.span = span;

        public new IHerculesEventBuilder<HerculesCustomClientSpan> SetTimestamp(DateTimeOffset timestamp) => this;

        public new HerculesCustomClientSpan BuildEvent() => span;
        
        public new IHerculesTagsBuilder AddContainer(string key, Action<IHerculesTagsBuilder> valueBuilder)
        {
            valueBuilder(
                key == TagNames.Annotations
                    ? new HerculesCustomClientSpanAnnotationsReader(span)
                    : DummyBuilder);

            return this;
        }
    }
}