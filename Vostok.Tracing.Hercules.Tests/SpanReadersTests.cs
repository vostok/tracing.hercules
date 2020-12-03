using FluentAssertions;
using NUnit.Framework;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Hercules.Readers;

namespace Vostok.Tracing.Hercules.Tests
{
    [TestFixture]
    internal class SpanReadersTests
    {
        [Test]
        public void Should_fill_annotations()
        {
            var reader = new HerculesHttpClusterSpanReader();
            reader.AddValue(WellKnownAnnotations.Http.Request.TargetEnvironment, "a");
            
            var span = reader.BuildEvent();
            span.TargetEnvironment.Should().Be("a");
        }
    }
}