using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Hercules.Helpers;
using Vostok.Tracing.Hercules.Models;
using Vostok.Tracing.Hercules.OpenTelemetry;

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

        public void FillUrl()
        {
            if (span.RequestUrl == null && span.RequestUrlPath != null)
                span.RequestUrl = UrlHelper.FromPieces(span.RequestUrlScheme, span.RequestUrlHost, span.RequestUrlPort, span.RequestUrlPath);
        }
        
        public new IHerculesTagsBuilder AddValue(string key, string value)
        {
            switch (key)
            {
                case SemanticConventions.AttributeHttpTargetLegacy:
                case SemanticConventions.AttributeUrlPath:
                    span.RequestUrlPath = value;
                    break;
                case SemanticConventions.AttributeNetHostNameLegacy:
                case SemanticConventions.AttributeServerAddress:
                    span.RequestUrlHost = value;
                    break;
                case SemanticConventions.AttributeHttpSchemeLegacy:
                case SemanticConventions.AttributeUrlScheme:
                    span.RequestUrlScheme = value;
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
                case SemanticConventions.AttributeNetHostPortLegacy:
                case SemanticConventions.AttributeServerPort:
                    span.RequestUrlPort = (int)value;
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
                case SemanticConventions.AttributeNetHostPortLegacy:
                case SemanticConventions.AttributeServerPort:
                    span.RequestUrlPort = value;
                    break;
                default:
                    base.AddValue(key, value);
                    break;
            }

            return this;
        }
    }
}