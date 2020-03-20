using Vostok.Clusterclient.Core.Model;
using Vostok.Commons.Helpers.Url;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Hercules.SpanBuilders
{
    internal class HerculesHttpClientSpanAnnotationsBuilder : DummyHerculesTagsBuilder, IHerculesTagsBuilder
    {
        private readonly HerculesHttpClientSpan span;

        public HerculesHttpClientSpanAnnotationsBuilder(HerculesHttpClientSpan span)
        {
            this.span = span;
        }

        public new IHerculesTagsBuilder AddValue(string key, string value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Common.Application:
                    span.Application = value;
                    break;
                case WellKnownAnnotations.Common.Host:
                    span.Host = value;
                    break;
                case WellKnownAnnotations.Http.Request.Url:
                    span.Url = UrlParser.Parse(value);
                    break;
                case WellKnownAnnotations.Http.Request.Method:
                    span.RequestMethod = value;
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
                    span.RequestSize = value;
                    break;
                case WellKnownAnnotations.Http.Response.Code:
                    span.ResponseCode = (ResponseCode)value;
                    break;
                case WellKnownAnnotations.Http.Response.Size:
                    span.ResponseSize = value;
                    break;
            }

            return this;
        }
    }
}