using System;
using System.Net;
using Vostok.Clusterclient.Core.Model;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Models;
using Vostok.Tracing.Hercules.OpenTelemetry;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders
{
    internal abstract class HerculesHttpSpanAnnotationsReader : HerculesCommonSpanAnnotationsReader, IHerculesTagsBuilder
    {
        private readonly HerculesHttpSpan span;

        protected HerculesHttpSpanAnnotationsReader(HerculesHttpSpan span)
            : base(span) =>
            this.span = span;

        public new IHerculesTagsBuilder AddValue(string key, string value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Http.Client.Name:
                    span.ClientName = value;
                    break;
                case WellKnownAnnotations.Http.Client.Address:
                case SemanticConventions.AttributeHttpClientIpLegacy:
                case SemanticConventions.AttributeClientAddress:
                    span.ClientAddress = IPAddress.TryParse(value, out var ip) ? ip : null;
                    break;
                case WellKnownAnnotations.Http.Request.Url:
                case SemanticConventions.AttributeHttpUrlLegacy:
                case SemanticConventions.AttributeHttpUrlFull:
                    span.RequestUrl = !Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out var url) ? null : url;
                    break;
                // case WellKnownAnnotations.Http.Request.Method: same as OTel attribute
                case SemanticConventions.AttributeHttpMethodLegacy:
                case SemanticConventions.AttributeHttpRequestMethod:
                    span.RequestMethod = value;
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
                case SemanticConventions.AttributeHttpRequestContentLengthLegacy:
                case SemanticConventions.AttributeHttpRequestContentLength:
                    span.RequestSize = value;
                    break;
                case WellKnownAnnotations.Http.Response.Size:
                case SemanticConventions.AttributeHttpResponseContentLengthLegacy:
                case SemanticConventions.AttributeHttpResponseContentLength:
                    span.ResponseSize = value;
                    break;
                // note (kungurtsev, 22.03.2023): grpc protocol has no int values:
                case WellKnownAnnotations.Http.Response.Code:
                case SemanticConventions.AttributeHttpStatusCodeLegacy:
                case SemanticConventions.AttributeHttpResponseStatusCode:
                    span.ResponseCode = (ResponseCode)value;
                    break;
                default:
                    base.AddValue(key, value);
                    break;
            }

            return this;
        }

        public new IHerculesTagsBuilder AddValue(string key, int value)
        {
            switch (key)
            {
                case WellKnownAnnotations.Http.Response.Code:
                case SemanticConventions.AttributeHttpStatusCodeLegacy:
                case SemanticConventions.AttributeHttpResponseStatusCode:
                    span.ResponseCode = (ResponseCode)value;
                    break;
                default:
                    base.AddValue(key, value);
                    break;
            }

            return this;
        }
    }
}