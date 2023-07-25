using System.Collections.Generic;
using Vostok.Hercules.Client.Abstractions.Events;

namespace Vostok.Tracing.Hercules.Readers.AnnotationReaders;

internal class HerculesSpecificAnnotationsReader : DummyHerculesTagsBuilder, IHerculesTagsBuilder
{
    private readonly Dictionary<string, object> annotations;

    public HerculesSpecificAnnotationsReader(Dictionary<string, object> annotations)
    {
        this.annotations = annotations;
    }

    public new IHerculesTagsBuilder AddValue(string key, int value)
    {
        if (annotations.ContainsKey(key))
            annotations[key] = value;
        return this;
    }

    public new IHerculesTagsBuilder AddValue(string key, string value)
    {
        if (annotations.ContainsKey(key))
            annotations[key] = value;
        return this;
    }

    public new IHerculesTagsBuilder AddValue(string key, long value)
    {
        if (annotations.ContainsKey(key))
            annotations[key] = value;
        return this;
    }

    public new IHerculesTagsBuilder AddValue(string key, double value)
    {
        if (annotations.ContainsKey(key))
            annotations[key] = value;
        return this;
    }
}