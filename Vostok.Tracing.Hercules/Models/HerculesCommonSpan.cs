using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Hercules.Readers.AnnotationReaders;

namespace Vostok.Tracing.Hercules.Models
{
    [PublicAPI]
    public abstract class HerculesCommonSpan
    {
        private IBinaryEventsReader reader;
        private long readerPosition;

        public Guid TraceId { get; set; }

        public DateTimeOffset BeginTimestamp { get; set; }

        public DateTimeOffset EndTimestamp { get; set; }

        public TimeSpan Latency => EndTimestamp - BeginTimestamp;

        public string Operation { get; set; }

        public string WellKnownStatus { get; set; }

        public string Application { get; set; }

        public string Environment { get; set; }

        public string Host { get; set; }

        public string Component { get; set; }

        public void ReadSpecificAnnotations(Dictionary<string, object> annotations)
        {
            var annotationsReader = new HerculesSpecificAnnotationsReader(annotations);

            using var _ = reader.JumpTo(readerPosition);
            
            reader.ReadContainer(annotationsReader);
        }

        public override string ToString() =>
            $"{nameof(TraceId)}: {TraceId}, {nameof(BeginTimestamp)}: {BeginTimestamp}, {nameof(EndTimestamp)}: {EndTimestamp}, {nameof(Latency)}: {Latency}, {nameof(Operation)}: {Operation}, {nameof(WellKnownStatus)}: {WellKnownStatus}, {nameof(Application)}: {Application}, {nameof(Environment)}: {Environment}, {nameof(Host)}: {Host}, {nameof(Component)}: {Component}";

        internal IBinaryEventsReader Reader
        {
            set
            {
                reader = value;
                readerPosition = reader.Position;
            }
        }
    }
}