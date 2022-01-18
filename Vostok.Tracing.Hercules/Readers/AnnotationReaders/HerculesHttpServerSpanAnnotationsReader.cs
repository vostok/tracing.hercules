using System.Net;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal class HerculesHttpServerSpanAnnotationsReader : HerculesHttpSpanAnnotationsReader, IHerculesTagsBuilder
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
                    span.ClientAddress = IPAddress.TryParse(value, out var parsed) && parsed.IsIPv4MappedToIPv6 ? parsed.MapToIPv4() : null;
                    break;
                default:
                    base.AddValue(key, value);
                    break;
            }

            return this;
        }
    }
}