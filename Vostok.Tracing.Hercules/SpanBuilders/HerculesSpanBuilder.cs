using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Commons.Binary;
using Vostok.Commons.Collections;
using Vostok.Hercules.Client.Abstractions.Events;
using Annotations = System.Collections.Generic.IReadOnlyDictionary<string, object>;

namespace Vostok.Tracing.Hercules.SpanBuilders
{
    [PublicAPI]
    public class HerculesSpanBuilder : HerculesSpanBuilderBase, IHerculesEventBuilder<HerculesSpan>
    {
        private static readonly CachingTransform<IBinaryBufferReader, Dictionary<ByteArrayKey, Annotations>> CacheTransform = new CachingTransform<IBinaryBufferReader, Dictionary<ByteArrayKey, Annotations>>(
            _ => new Dictionary<ByteArrayKey, Annotations>());

        private readonly Dictionary<ByteArrayKey, Annotations> cache;

        private IReadOnlyDictionary<string, object> annotations;

        public HerculesSpanBuilder(IBinaryBufferReader reader)
            : base(reader)
        {
            // Note(kungurtsev): deleting old cache with byte array buffer.
            cache = CacheTransform.Get(reader);
        }

        public IHerculesEventBuilder<HerculesSpan> SetTimestamp(DateTimeOffset timestamp) => this;

        public HerculesSpan BuildEvent()
        {
            return new HerculesSpan(
                TraceId ?? throw new ArgumentOutOfRangeException(nameof(TraceId), "Unexpected null traceId."),
                SpanId ?? throw new ArgumentOutOfRangeException(nameof(SpanId), "Unexpected null spanId."),
                ParentSpanId,
                BeginTimestamp,
                EndTimestamp,
                annotations);
        }

        protected override void AddAnnotations(Action<IHerculesTagsBuilder> valueBuilder)
        {
            var startPosition = Reader.Position;
            Reader.SkipMode = true;
            valueBuilder(DummyBuilder);
            Reader.SkipMode = false;
            var endPosition = Reader.Position;

            var byteKey = new ByteArrayKey(Reader.Buffer, startPosition, endPosition - startPosition);

            if (cache.TryGetValue(byteKey, out annotations))
                return;

            Reader.Position = startPosition;

            var builder = new HerculesSpanAnnotationsBuilder();
            valueBuilder(builder);
            cache[byteKey] = annotations = builder.Dictionary;
        }
    }
}