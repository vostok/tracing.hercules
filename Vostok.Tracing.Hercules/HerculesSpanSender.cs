using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Hercules
{
    /// <summary>
    /// An implementation of <see cref="ISpanSender"/> based on <see cref="IHerculesSink"/> from Hercules client library.
    /// </summary>
    [PublicAPI]
    public class HerculesSpanSender : ISpanSender
    {
        private readonly Func<HerculesSpanSenderSettings> settingsProvider;

        public HerculesSpanSender([NotNull] Func<HerculesSpanSenderSettings> settingsProvider)
        {
            this.settingsProvider = settingsProvider ?? throw new ArgumentNullException(nameof(settingsProvider));
        }

        public HerculesSpanSender([NotNull] HerculesSpanSenderSettings settings)
            : this(() => settings)
        {
        }

        public void Send(ISpan span)
        {
            var settings = settingsProvider();

            settings.Sink.Put(settings.Stream, builder => HerculesSpanBuilder.Build(span, builder, settings.FormatProvider));
        }
    }
}