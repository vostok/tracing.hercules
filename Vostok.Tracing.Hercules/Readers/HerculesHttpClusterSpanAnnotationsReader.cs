﻿using Vostok.Commons.Helpers.Url;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Models;

namespace Vostok.Tracing.Hercules.Readers
{
    internal class HerculesHttpClusterSpanAnnotationsReader : DummyHerculesTagsBuilder, IHerculesTagsBuilder
    {
        private readonly HerculesHttpClusterSpan span;

        public HerculesHttpClusterSpanAnnotationsReader(HerculesHttpClusterSpan span)
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
                    span.RequestUrl = UrlParser.Parse(value);
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
                case WellKnownAnnotations.Http.Cluster.Status:
                    span.Status = value;
                    break;
                case WellKnownAnnotations.Http.Cluster.Strategy:
                    span.Strategy = value;
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
    }
}