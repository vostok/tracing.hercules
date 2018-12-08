namespace Vostok.Tracing.Hercules
{
    internal static class TagNames
    {
        public const string TraceId = nameof(TraceId);

        public const string SpanId = nameof(SpanId);

        public const string ParentSpanId = nameof(ParentSpanId);

        public const string BeginTimestampUtc = nameof(BeginTimestampUtc);

        public const string BeginTimestampUtcOffset = nameof(BeginTimestampUtcOffset);

        public const string EndTimestampUtc = nameof(EndTimestampUtc);

        public const string EndTimestampUtcOffset = nameof(EndTimestampUtcOffset);

        public const string Annotations = nameof(Annotations);
    }
}
