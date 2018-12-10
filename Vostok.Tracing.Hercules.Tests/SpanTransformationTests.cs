using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using FluentAssertions.Extensions;
using NSubstitute;
using NUnit.Framework;
using Vostok.Hercules.Client.Abstractions;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Hercules.Tests
{
    [TestFixture]
    internal class SpanTransformationTests
    {
        private ISpan originalSpan;
        private ISpan herculesSpan;

        private IHerculesSink sink;
        private HerculesEventBuilder builder;
        private HerculesEvent @event;
        private HerculesSpanSender sender;

        [SetUp]
        public void TestSetup()
        {
            originalSpan = Substitute.For<ISpan>();
            originalSpan.TraceId.Returns(Guid.NewGuid());
            originalSpan.SpanId.Returns(Guid.NewGuid());

            builder = new HerculesEventBuilder();

            @event = null;

            sink = Substitute.For<IHerculesSink>();
            sink
                .When(s => s.Put(Arg.Any<string>(), Arg.Any<Action<IHerculesEventBuilder>>()))
                .Do(info => info.Arg<Action<IHerculesEventBuilder>>().Invoke(builder));

            sender = new HerculesSpanSender(new HerculesSpanSenderConfig(sink, Guid.NewGuid().ToString()));
        }

        [Test]
        public void Should_correctly_transform_trace_id()
        {
            TransformSpan();

            herculesSpan.TraceId.Should().Be(originalSpan.TraceId);
        }

        [Test]
        public void Should_correctly_transform_span_id()
        {
            TransformSpan();

            herculesSpan.SpanId.Should().Be(originalSpan.SpanId);
        }

        [Test]
        public void Should_correctly_transform_existing_parent_span_id()
        {
            originalSpan.ParentSpanId.Returns(Guid.NewGuid());

            TransformSpan();

            herculesSpan.ParentSpanId.Should().Be(originalSpan.ParentSpanId);
        }

        [Test]
        public void Should_correctly_transform_missing_parent_span_id()
        {
            originalSpan.ParentSpanId.Returns(null as Guid?);

            TransformSpan();

            herculesSpan.ParentSpanId.Should().BeNull();
        }

        [Test]
        public void Should_correctly_transform_begin_timestamp_of_local_kind()
        {
            originalSpan.BeginTimestamp.Returns(DateTimeOffset.Now);

            TransformSpan();

            herculesSpan.BeginTimestamp.Should().Be(originalSpan.BeginTimestamp);
        }

        [Test]
        public void Should_correctly_transform_begin_timestamp_of_utc_kind()
        {
            originalSpan.BeginTimestamp.Returns(DateTimeOffset.UtcNow);

            TransformSpan();

            herculesSpan.BeginTimestamp.Should().Be(originalSpan.BeginTimestamp);
        }

        [Test]
        public void Should_correctly_transform_existing_end_timestamp_of_local_kind()
        {
            originalSpan.EndTimestamp.Returns(DateTimeOffset.Now);

            TransformSpan();

            herculesSpan.EndTimestamp.Should().Be(originalSpan.EndTimestamp);
        }

        [Test]
        public void Should_correctly_transform_existing_end_timestamp_of_utc_kind()
        {
            originalSpan.EndTimestamp.Returns(DateTimeOffset.UtcNow);

            TransformSpan();

            herculesSpan.EndTimestamp.Should().Be(originalSpan.EndTimestamp);
        }

        [Test]
        public void Should_correctly_transform_missing_end_timestamp()
        {
            originalSpan.EndTimestamp.Returns(null as DateTimeOffset?);

            TransformSpan();

            herculesSpan.EndTimestamp.Should().BeNull();
        }

        [Test]
        public void Should_choose_end_timestamp_as_primary_Hercules_timestamp_if_available()
        {
            originalSpan.BeginTimestamp.Returns(DateTimeOffset.UtcNow);
            originalSpan.EndTimestamp.Returns(DateTimeOffset.UtcNow + 2.Hours());

            TransformSpan();

            @event.Timestamp.Should().Be(originalSpan.EndTimestamp.Value);
        }

        [Test]
        public void Should_choose_begin_timestamp_as_primary_Hercules_timestamp_if_end_timestamp_is_not_available()
        {
            originalSpan.BeginTimestamp.Returns(DateTimeOffset.UtcNow);
            originalSpan.EndTimestamp.Returns(null as DateTimeOffset?);

            TransformSpan();

            @event.Timestamp.Should().Be(originalSpan.BeginTimestamp);
        }

        [Test]
        public void Should_correctly_transform_annotations_of_primitive_scalar_types()
        {
            originalSpan.Annotations.Returns(
                new Dictionary<string, object>
                {
                    ["key1"] = byte.MinValue,
                    ["key2"] = true,
                    ["key3"] = short.MaxValue,
                    ["key4"] = int.MaxValue,
                    ["key5"] = long.MaxValue,
                    ["key6"] = float.MaxValue,
                    ["key7"] = double.MaxValue,
                    ["key8"] = Guid.NewGuid(),
                    ["key9"] = Guid.NewGuid().ToString()
                });

            TransformSpan();

            herculesSpan.Annotations.Should().BeEquivalentTo(originalSpan.Annotations);
        }

        [Test]
        public void Should_correctly_transform_annotations_of_primitive_vector_types()
        {
            originalSpan.Annotations.Returns(
                new Dictionary<string, object>
                {
                    ["key1"] = new [] {byte.MinValue},
                    ["key2"] = new [] {true},
                    ["key3"] = new [] {short.MaxValue},
                    ["key4"] = new [] {int.MaxValue},
                    ["key5"] = new [] {long.MaxValue},
                    ["key6"] = new [] {float.MaxValue},
                    ["key7"] = new [] {double.MaxValue},
                    ["key8"] = new [] {Guid.NewGuid()},
                    ["key9"] = new[] {Guid.NewGuid().ToString()}
                });
        
            TransformSpan();
        
            herculesSpan.Annotations.Should().BeEquivalentTo(originalSpan.Annotations);
        }

        [Test]
        public void Should_stringify_or_serialize_complex_annotation_values()
        {
            originalSpan.Annotations.Returns(
                new Dictionary<string, object>
                {
                    ["key1"] = new { A = 1, B = 2},
                    ["key2"] = IPAddress.Parse("154.31.215.11")
                });

            TransformSpan();

            herculesSpan.Annotations.Count.Should().Be(2);
            herculesSpan.Annotations["key1"].Should().Be("{\"A\": \"1\", \"B\": \"2\"}");
            herculesSpan.Annotations["key2"].Should().Be("154.31.215.11");
        }


        [Test]
        public void Should_correctly_transform_annotations_with_null_values()
        {
            originalSpan.Annotations.Returns(
                new Dictionary<string, object>
                {
                    ["key1"] = null,
                    ["key2"] = null
                });

            TransformSpan();

            herculesSpan.Annotations.Should().BeEquivalentTo(originalSpan.Annotations);
        }

        private void TransformSpan()
        {
            sender.Send(originalSpan);

            herculesSpan = new HerculesSpan(@event = builder.BuildEvent());
        }
    }
}
