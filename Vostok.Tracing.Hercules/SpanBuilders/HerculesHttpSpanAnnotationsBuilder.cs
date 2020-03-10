using System;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Hercules.SpanBuilders
{
    internal class HerculesHttpSpanAnnotationsBuilder : DummyHerculesTagsBuilder, IHerculesTagsBuilder
    {
        private readonly HerculesHttpSpan span;

        public HerculesHttpSpanAnnotationsBuilder(HerculesHttpSpan span)
        {
            this.span = span;
        }

        public new IHerculesTagsBuilder AddValue(string key, string value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Common.Kind:
                    span.IsClientSpan = value == WellKnownSpanKinds.HttpRequest.Client;
                    break;
                case WellKnownAnnotations.Common.Application:
                    span.OriginService = value;
                    break;
                case WellKnownAnnotations.Common.Host:
                    span.OriginHost = value;
                    break;
                case WellKnownAnnotations.Http.Request.Url:
                    AddUrl(value);
                    break;
                case WellKnownAnnotations.Http.Request.Method:
                    span.ClientRequestMethod = value;
                    break;
                case WellKnownAnnotations.Http.Request.TargetEnvironment:
                    span.TargetEnvironment = value;
                    break;
                case WellKnownAnnotations.Http.Request.TargetService:
                    span.TargetService = value;
                    break;
            }

            return this;
        }

        public new IHerculesTagsBuilder AddValue(string key, int value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Http.Request.Size:
                    span.ClientRequestBodySize = value;
                    break;
                case WellKnownAnnotations.Http.Response.Code:
                    span.ClientResponseCode = value;
                    break;
                case WellKnownAnnotations.Http.Response.Size:
                    span.ClientResponseBodySize = value;
                    break;
            }

            return this;
        }

        private void AddUrl(string value)
        {
            if (!Uri.TryCreate(value, UriKind.Absolute, out var url))
                return;
            span.Url = url;
        }
    }
}