using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Hercules.Helpers;
using Vostok.Tracing.Hercules.Models;
using Vostok.Tracing.Hercules.Readers.AnnotationReaders;

namespace Vostok.Tracing.Hercules.Readers
{
    [PublicAPI]
    public class HerculesCustomOperationSpanReader : HerculesCommonSpanReader, IHerculesEventBuilder<HerculesCustomOperationSpan>
    {
        private static readonly DummyHerculesTagsBuilder DummyBuilder = new DummyHerculesTagsBuilder();
        private readonly HerculesCustomOperationSpan span;
        
        public HerculesCustomOperationSpanReader(): this(new HerculesCustomOperationSpan()) {}
        
        private HerculesCustomOperationSpanReader(HerculesCustomOperationSpan span): base(span) =>
            this.span = span;
        
        public new IHerculesEventBuilder<HerculesCustomOperationSpan> SetTimestamp(DateTimeOffset timestamp) => this;

        public new HerculesCustomOperationSpan BuildEvent() => span;
        
        public new IHerculesTagsBuilder AddContainer(string key, Action<IHerculesTagsBuilder> valueBuilder)
        {
            valueBuilder(
                key == TagNames.Annotations
                    ? new HerculesCustomOperationSpanAnnotationsReader(span)
                    : DummyBuilder);

            return this;
        }
    }
}