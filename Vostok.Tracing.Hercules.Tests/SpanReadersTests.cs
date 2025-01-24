using System;
using System.Net;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Helpers;
using Vostok.Tracing.Hercules.OpenTelemetry;
using Vostok.Tracing.Hercules.Readers;

namespace Vostok.Tracing.Hercules.Tests
{
    [TestFixture]
    internal class SpanReadersTests
    {
        private readonly IBinaryEventsReader fakeReader = Substitute.For<IBinaryEventsReader>(); 
        
        [Test]
        public void Should_fill_http_cluster_annotations()
        {
            var reader = new HerculesHttpClusterSpanReader(fakeReader);
            var traceId = Guid.Parse("1DE90442-FC2D-4F43-829E-B0CC1A75C426");

            reader.AddValue(TagNames.TraceId, traceId);
            reader.AddContainer(
                TagNames.Annotations,
                builder => builder
                    .AddValue(WellKnownAnnotations.Http.Request.TargetEnvironment, "foo")
                    .AddValue(WellKnownAnnotations.Http.Response.Code, 200)
                    .AddValue(WellKnownAnnotations.Http.Request.TargetService, "baz")
                    .AddValue(WellKnownAnnotations.Http.Cluster.Strategy, "bar")
                    .AddValue(WellKnownAnnotations.Http.Request.Size, 10L)
                    .AddValue(WellKnownAnnotations.Http.Response.Size, 20L));

            var span = reader.BuildEvent();

            span.TargetEnvironment.Should().Be("foo");
            span.TraceId.Should().Be(traceId);
            span.ResponseCode.Should().Be(200);
            span.TargetService.Should().Be("baz");
            span.Strategy.Should().Be("bar");
            span.RequestSize.Should().Be(10L);
            span.ResponseSize.Should().Be(20L);
        }

        [Test]
        public void Should_fill_http_client_annotations()
        {
            var reader = new HerculesHttpClientSpanReader(fakeReader);
            var traceId = Guid.Parse("1DE90442-FC2D-4F43-829E-B0CC1A75C426");

            reader.AddValue(TagNames.TraceId, traceId);
            reader.AddContainer(
                TagNames.Annotations,
                builder => builder
                    .AddValue(WellKnownAnnotations.Http.Request.TargetEnvironment, "foo")
                    .AddValue(WellKnownAnnotations.Http.Response.Code, 200)
                    .AddValue(WellKnownAnnotations.Http.Request.TargetService, "baz")
                    .AddValue(WellKnownAnnotations.Http.Request.Size, 10L)
                    .AddValue(WellKnownAnnotations.Http.Response.Size, 20L));

            var span = reader.BuildEvent();

            span.TargetEnvironment.Should().Be("foo");
            span.TraceId.Should().Be(traceId);
            span.ResponseCode.Should().Be(200);
            span.TargetService.Should().Be("baz");
            span.RequestSize.Should().Be(10L);
            span.ResponseSize.Should().Be(20L);
        }

        [Test]
        public void Should_fill_custom_client_annotations()
        {
            var reader = new HerculesCustomClientSpanReader(fakeReader);
            var traceId = Guid.Parse("1DE90442-FC2D-4F43-829E-B0CC1A75C426");

            reader.AddValue(TagNames.TraceId, traceId);
            reader.AddContainer(
                TagNames.Annotations,
                builder => builder
                    .AddValue(WellKnownAnnotations.Custom.Request.TargetEnvironment, "foo")
                    .AddValue(WellKnownAnnotations.Custom.Request.Replica, "bar")
                    .AddValue(WellKnownAnnotations.Custom.Request.TargetService, "baz")
                    .AddValue(WellKnownAnnotations.Custom.Request.Size, 10L)
                    .AddValue(WellKnownAnnotations.Custom.Response.Size, 20L));

            var span = reader.BuildEvent();

            span.TargetEnvironment.Should().Be("foo");
            span.TraceId.Should().Be(traceId);
            span.Replica.Should().Be("bar");
            span.TargetService.Should().Be("baz");
            span.RequestSize.Should().Be(10L);
            span.ResponseSize.Should().Be(20L);
        }

        [Test]
        public void Should_fill_custom_cluster_annotations()
        {
            var reader = new HerculesCustomClusterSpanReader(fakeReader);
            var traceId = Guid.Parse("1DE90442-FC2D-4F43-829E-B0CC1A75C426");

            reader.AddValue(TagNames.TraceId, traceId);
            reader.AddContainer(
                TagNames.Annotations,
                builder => builder
                    .AddValue(WellKnownAnnotations.Custom.Request.TargetEnvironment, "foo")
                    .AddValue(WellKnownAnnotations.Custom.Request.TargetService, "baz")
                    .AddValue(WellKnownAnnotations.Custom.Request.Size, 10L)
                    .AddValue(WellKnownAnnotations.Custom.Response.Size, 20L));

            var span = reader.BuildEvent();
            
            span.TargetEnvironment.Should().Be("foo");
            span.TraceId.Should().Be(traceId);
            span.TargetService.Should().Be("baz");
            span.RequestSize.Should().Be(10L);
            span.ResponseSize.Should().Be(20L);
        }

        [Test]
        public void Should_fill_custom_operation_annotations()
        {
            var reader = new HerculesCustomOperationSpanReader(fakeReader);
            var traceId = Guid.Parse("1DE90442-FC2D-4F43-829E-B0CC1A75C426");

            reader.AddValue(TagNames.TraceId, traceId);
            reader.AddContainer(
                TagNames.Annotations,
                builder => builder
                    .AddValue(WellKnownAnnotations.Custom.Operation.TargetEnvironment, "foo")
                    .AddValue(WellKnownAnnotations.Custom.Operation.TargetService, "baz")
                    .AddValue(WellKnownAnnotations.Custom.Operation.Status, "bar")
                    .AddValue(WellKnownAnnotations.Custom.Operation.Size, 10L));

            var span = reader.BuildEvent();

            span.TargetEnvironment.Should().Be("foo");
            span.TraceId.Should().Be(traceId);
            span.TargetService.Should().Be("baz");
            span.CustomStatus.Should().Be("bar");
            span.Size.Should().Be(10L);
        }

        [Test]
        public void Should_fill_http_server_annotations()
        {
            var reader = new HerculesHttpServerSpanReader(fakeReader);
            var traceId = Guid.Parse("1DE90442-FC2D-4F43-829E-B0CC1A75C426");
            var ipAddress = IPAddress.Loopback;
            var clientName = "zapad";

            reader.AddValue(TagNames.TraceId, traceId);
            reader.AddContainer(
                TagNames.Annotations,
                builder => builder
                   .AddValue(WellKnownAnnotations.Common.Environment, "foo")
                   .AddValue(WellKnownAnnotations.Http.Response.Code, 200)
                   .AddValue(WellKnownAnnotations.Common.Application, "baz")
                   .AddValue(WellKnownAnnotations.Http.Request.Size, 10L)
                   .AddValue(WellKnownAnnotations.Http.Response.Size, 20L)
                   .AddValue(WellKnownAnnotations.Http.Client.Address, ipAddress.ToString())
                   .AddValue(WellKnownAnnotations.Http.Client.Name, clientName)
                );

            var span = reader.BuildEvent();

            span.Environment.Should().Be("foo");
            span.TraceId.Should().Be(traceId);
            span.ResponseCode.Should().Be(200);
            span.Application.Should().Be("baz");
            span.RequestSize.Should().Be(10L);
            span.ResponseSize.Should().Be(20L);
            span.ClientAddress.Should().Be(ipAddress);
            span.ClientName.Should().Be(clientName);
        }
        
        [Test]
        public void Should_fill_http_server_annotations_with_OpenTelemetryLegacyStyle()
        {
            var reader = new HerculesHttpServerSpanReader(fakeReader);
            var traceId = Guid.Parse("1DE90442-FC2D-4F43-829E-B0CC1A75C426");
            var ipAddress = IPAddress.Loopback;
            var clientName = "zapad";

            reader.AddValue(TagNames.TraceId, traceId);
            reader.AddContainer(
                TagNames.Annotations,
                builder => builder
                    .AddValue(SemanticConventions.AttributeHostName, "host_name")
                    .AddValue(SemanticConventions.AttributeDeploymentEnvironmentName, "foo")
                    .AddValue(SemanticConventions.AttributeHttpStatusCodeLegacy, 200)
                    .AddValue(SemanticConventions.AttributeServiceName, "baz")
                    .AddValue(SemanticConventions.AttributeHttpRequestContentLengthLegacy, 10L)
                    .AddValue(SemanticConventions.AttributeHttpResponseContentLengthLegacy, 20L)
                    .AddValue(SemanticConventions.AttributeHttpClientIpLegacy, ipAddress.ToString())
                    .AddValue(WellKnownAnnotations.Http.Client.Name, clientName)
                    .AddValue(SemanticConventions.AttributeHttpTargetLegacy, "/webshop/articles/4?s=1")
                    .AddValue(SemanticConventions.AttributeNetHostNameLegacy, "example.com")
                    .AddValue(SemanticConventions.AttributeHttpSchemeLegacy, "https")
                    .AddValue(SemanticConventions.AttributeNetHostPortLegacy, 8080)
            );

            var span = reader.BuildEvent();

            span.Environment.Should().Be("foo");
            span.TraceId.Should().Be(traceId);
            span.ResponseCode.Should().Be(200);
            span.Application.Should().Be("baz");
            span.RequestSize.Should().Be(10L);
            span.ResponseSize.Should().Be(20L);
            span.ClientAddress.Should().Be(ipAddress);
            span.ClientName.Should().Be(clientName);
            span.RequestUrl.Should().Be(new Uri("https://example.com:8080/webshop/articles/4?s=1"));
        }

        [Test]
        public void Should_fill_http_server_annotations_with_OpenTelemetryStyle()
        {
            var reader = new HerculesHttpServerSpanReader(fakeReader);
            var traceId = Guid.Parse("1DE90442-FC2D-4F43-829E-B0CC1A75C426");
            var ipAddress = IPAddress.Loopback;
            var clientName = "zapad";

            reader.AddValue(TagNames.TraceId, traceId);
            reader.AddContainer(
                TagNames.Annotations,
                builder => builder
                    .AddValue(SemanticConventions.AttributeHostName, "host_name")
                    .AddValue(SemanticConventions.AttributeDeploymentEnvironmentName, "foo")
                    .AddValue(SemanticConventions.AttributeHttpResponseStatusCode, 200)
                    .AddValue(SemanticConventions.AttributeServiceName, "baz")
                    .AddValue(SemanticConventions.AttributeHttpRequestContentLength, 10L)
                    .AddValue(SemanticConventions.AttributeHttpResponseContentLength, 20L)
                    .AddValue(SemanticConventions.AttributeClientAddress, ipAddress.ToString())
                    .AddValue(WellKnownAnnotations.Http.Client.Name, clientName)
                    .AddValue(SemanticConventions.AttributeUrlPath, "/webshop/articles/4?s=1")
                    .AddValue(SemanticConventions.AttributeServerAddress, "example.com")
                    .AddValue(SemanticConventions.AttributeUrlScheme, "https")
                    .AddValue(SemanticConventions.AttributeServerPort, 8080)
            );

            var span = reader.BuildEvent();

            span.Environment.Should().Be("foo");
            span.TraceId.Should().Be(traceId);
            span.ResponseCode.Should().Be(200);
            span.Application.Should().Be("baz");
            span.RequestSize.Should().Be(10L);
            span.ResponseSize.Should().Be(20L);
            span.ClientAddress.Should().Be(ipAddress);
            span.ClientName.Should().Be(clientName);
            span.RequestUrl.Should().Be(new Uri("https://example.com:8080/webshop/articles/4?s=1"));
        }
    }
}