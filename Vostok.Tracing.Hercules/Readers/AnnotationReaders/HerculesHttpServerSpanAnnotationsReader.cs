using System.Net;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal class HerculesHttpServerSpanAnnotationsReader : HerculesHttpBaseSpanAnnotationsReader, IHerculesTagsBuilder
    {
        private readonly HerculesHttpServerSpan span;

        public HerculesHttpServerSpanAnnotationsReader(HerculesHttpServerSpan span)
            : base(span)
        {
            this.span = span;
        }

        public new IHerculesTagsBuilder AddValue(string key, string value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Http.Client.Name:
                    span.ClientName = value;
                    break;
                case WellKnownAnnotations.Http.Client.Address:
                    span.ClientAddress = string.IsNullOrEmpty(value) ? null : IPAddress.Parse(value);
                    break;
                default:
                    base.AddValue(key, value);
                    break;
            }

            return this;
        }
    }
}