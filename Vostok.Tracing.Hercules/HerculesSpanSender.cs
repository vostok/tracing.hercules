using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Hercules
{
    [PublicAPI]
    public class HerculesSpanSender : ISpanSender
    {
        private readonly HerculesSpanSenderConfig config;

        public HerculesSpanSender([NotNull] HerculesSpanSenderConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void Send(ISpan span)
        {
            throw new NotImplementedException();
        }
    }
}
