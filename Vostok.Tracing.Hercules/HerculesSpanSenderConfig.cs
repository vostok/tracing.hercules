using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions;

namespace Vostok.Tracing.Hercules
{
    [PublicAPI]
    public class HerculesSpanSenderConfig
    {
        public HerculesSpanSenderConfig([NotNull] IHerculesSink sink, [NotNull] string stream)
        {
            Sink = sink ?? throw new ArgumentNullException(nameof(sink));
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        /// <summary>
        /// Hercules sink used to emit events.
        /// </summary>
        [NotNull]
        public IHerculesSink Sink { get; }

        /// <summary>
        /// Name of the Hercules stream to use.
        /// </summary>
        [NotNull]
        public string Stream { get; }

        /// <summary>
        /// If specified, this <see cref="IFormatProvider"/> will be used when formatting annotation values to strings.
        /// </summary>
        [CanBeNull]
        public IFormatProvider FormatProvider { get; set; }
    }
}
