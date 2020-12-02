using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Hercules.Helpers;
using Vostok.Tracing.Hercules.Models;
using Vostok.Tracing.Hercules.Readers.AnnotationReaders;

namespace Vostok.Tracing.Hercules.Readers
{
    [PublicAPI]
    public class HerculesOperationSpanReader : HerculesCommonSpanReader, IHerculesEventBuilder<HerculesOperationSpan>
    {
        private static readonly DummyHerculesTagsBuilder DummyBuilder = new DummyHerculesTagsBuilder();
        private readonly HerculesOperationSpan span;
        
        public HerculesOperationSpanReader(): this(new HerculesOperationSpan()) {}
        
        private HerculesOperationSpanReader(HerculesOperationSpan span): base(span) =>
            this.span = span;
        
        public new IHerculesEventBuilder<HerculesOperationSpan> SetTimestamp(DateTimeOffset timestamp) => this;

        public new HerculesOperationSpan BuildEvent() => span;
        
        public new IHerculesTagsBuilder AddContainer(string key, Action<IHerculesTagsBuilder> valueBuilder)
        {
            valueBuilder(
                key == TagNames.Annotations
                    ? new HerculesOperationSpanAnnotationsReader(span)
                    : DummyBuilder);

            return this;
        }
    }
}