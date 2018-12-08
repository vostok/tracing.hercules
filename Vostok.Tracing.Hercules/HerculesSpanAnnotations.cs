using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.Hercules.Client.Abstractions.Values;

namespace Vostok.Tracing.Hercules
{
    internal class HerculesSpanAnnotations : IReadOnlyDictionary<string, object>
    {
        private readonly HerculesTags annotations;

        public HerculesSpanAnnotations(HerculesTags annotations)
        {
            this.annotations = annotations;
        }

        public int Count
            => annotations.Count;

        public IEnumerable<string> Keys
            => this.Select(pair => pair.Key);

        public IEnumerable<object> Values
            => this.Select(pair => pair.Value);

        public bool ContainsKey(string key)
            => annotations.ContainsKey(key);

        public bool TryGetValue(string key, out object value)
        {
            if (!annotations.TryGetValue(key, out var herculesValue))
            {
                value = null;
                return false;
            }

            value = UnwrapValue(herculesValue);
            return true;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
            => annotations.Select(pair => new KeyValuePair<string, object>(pair.Key, UnwrapValue(pair.Value))).GetEnumerator();

        public object this[string key]
            => UnwrapValue(annotations.GetValue(key));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static object UnwrapValue(HerculesValue value)
            => value.IsVector ? value.AsVector.Elements.Select(element => element.Value) : value.Value;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
