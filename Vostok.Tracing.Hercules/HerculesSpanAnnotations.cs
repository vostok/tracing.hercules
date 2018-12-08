using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vostok.Hercules.Client.Abstractions.Events;

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

            value = herculesValue.Value;
            return true;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
            => annotations.Select(pair => new KeyValuePair<string, object>(pair.Key, pair.Value.Value)).GetEnumerator();

        public object this[string key]
            => annotations.GetValue(key).Value;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
