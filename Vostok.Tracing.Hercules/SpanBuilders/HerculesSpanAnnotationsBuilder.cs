using System;
using System.Collections.Generic;
using Vostok.Hercules.Client.Abstractions.Events;

namespace Vostok.Tracing.Hercules.SpanBuilders
{
    internal partial class HerculesSpanAnnotationsBuilder : IHerculesTagsBuilder
    {
        private static readonly DummyHerculesTagsBuilder DummyBuilder = new DummyHerculesTagsBuilder();

        public Dictionary<string, object> Dictionary = new Dictionary<string, object>();

        public IHerculesTagsBuilder AddContainer(string key, Action<IHerculesTagsBuilder> valueBuilder)
        {
            valueBuilder(DummyBuilder);
            return this;
        }

        public IHerculesTagsBuilder AddVectorOfContainers(string key, IReadOnlyList<Action<IHerculesTagsBuilder>> valueBuilders)
        {
            foreach (var valueBuilder in valueBuilders)
            {
                valueBuilder(DummyBuilder);
            }

            return this;
        }

        public IHerculesTagsBuilder AddNull(string key)
        {
            Dictionary[key] = null;
            return this;
        }

        public IHerculesTagsBuilder RemoveTags(string key)
        {
            if (Dictionary.ContainsKey(key))
                Dictionary.Remove(key);
            return this;
        }
    }
}