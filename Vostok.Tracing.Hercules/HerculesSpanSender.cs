using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Hercules
{
    [PublicAPI]
    public class HerculesSpanSender : ISpanSender
    {
        private readonly IHerculesSink sink;
        private readonly string stream;

        public HerculesSpanSender([NotNull] IHerculesSink sink, [NotNull] string stream)
        {
            this.sink = sink ?? throw new ArgumentNullException(nameof(sink));
            this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        public void Send(ISpan span)
        {
            throw new NotImplementedException();
        }
    }
}
