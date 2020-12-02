using System;
using Vostok.Clusterclient.Core.Model;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal abstract class HerculesHttpSpanAnnotationsReader : HerculesCommonSpanAnnotationsReader, IHerculesTagsBuilder
    {
        private readonly HerculesHttpSpan span;

        protected HerculesHttpSpanAnnotationsReader(HerculesHttpSpan span) : base(span) =>
            this.span = span;

        public new IHerculesTagsBuilder AddValue(string key, string value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Http.Request.Url:
                    span.RequestUrl = !Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out var parsed) ? null : parsed;
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
                default:
                    base.AddValue(key, value);
                    break;
            }

            return this;
        }

        public new IHerculesTagsBuilder AddValue(string key, long value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Http.Request.Size:
                    span.RequestSize = value;
                    break;
                case WellKnownAnnotations.Http.Response.Size:
                    span.ResponseSize = value;
                    break;
            }

            return this;
        }

        public new IHerculesTagsBuilder AddValue(string key, int value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Http.Response.Code:
                    span.ResponseCode = (ResponseCode)value;
                    break;
            }

            return this;
        }
    }
}