using System;
using System.Net;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Helpers;
using Vostok.Tracing.Hercules.Readers;

namespace Vostok.Tracing.Hercules.Tests
{
    [TestFixture]
    internal class SpanReadersTests
    {
        [Test]
        public void Should_fill_http_cluster_annotations()
        {
            var reader = new HerculesHttpClusterSpanReader();
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
            var reader = new HerculesHttpClientSpanReader();
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
            var reader = new HerculesCustomClientSpanReader();
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
            var reader = new HerculesCustomClusterSpanReader();
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
            var reader = new HerculesCustomOperationSpanReader();
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
            var reader = new HerculesHttpServerSpanReader();
            var traceId = Guid.Parse("1DE90442-FC2D-4F43-829E-B0CC1A75C426");
            var ipAddress = IPAddress.Loopback;
            var clientName = "zapad";

            reader.AddValue(TagNames.TraceId, traceId);
            reader.AddContainer(
                TagNames.Annotations,
                builder => builder
                   .AddValue(WellKnownAnnotations.Http.Request.TargetEnvironment, "foo")
                   .AddValue(WellKnownAnnotations.Http.Response.Code, 200)
                   .AddValue(WellKnownAnnotations.Http.Request.TargetService, "baz")
                   .AddValue(WellKnownAnnotations.Http.Request.Size, 10L)
                   .AddValue(WellKnownAnnotations.Http.Response.Size, 20L)
                   .AddValue(WellKnownAnnotations.Http.Client.Address, ipAddress.ToString())
                   .AddValue(WellKnownAnnotations.Http.Client.Name, clientName)
                );

            var span = reader.BuildEvent();

            span.TargetEnvironment.Should().Be("foo");
            span.TraceId.Should().Be(traceId);
            span.ResponseCode.Should().Be(200);
            span.TargetService.Should().Be("baz");
            span.RequestSize.Should().Be(10L);
            span.ResponseSize.Should().Be(20L);
            span.ClientAddress.Should().Be(ipAddress);
            span.ClientName.Should().Be(clientName);
        }
    }
}