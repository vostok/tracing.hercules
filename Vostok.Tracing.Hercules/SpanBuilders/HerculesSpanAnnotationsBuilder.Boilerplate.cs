

using System;
using System.Collections.Generic;
using Vostok.Hercules.Client.Abstractions.Events;

namespace Vostok.Tracing.Hercules.SpanBuilders
{
    internal partial class HerculesSpanAnnotationsBuilder
    {

        #region bool

        public IHerculesTagsBuilder AddValue(string key, bool value)
        {
            Dictionary[key] = value;
            return this;
        }

        public IHerculesTagsBuilder AddVector(string key, IReadOnlyList<bool> values)
        {
            Dictionary[key] = values;
            return this;
        }

        #endregion


        #region byte

        public IHerculesTagsBuilder AddValue(string key, byte value)
        {
            Dictionary[key] = value;
            return this;
        }

        public IHerculesTagsBuilder AddVector(string key, IReadOnlyList<byte> values)
        {
            Dictionary[key] = values;
            return this;
        }

        #endregion


        #region short

        public IHerculesTagsBuilder AddValue(string key, short value)
        {
            Dictionary[key] = value;
            return this;
        }

        public IHerculesTagsBuilder AddVector(string key, IReadOnlyList<short> values)
        {
            Dictionary[key] = values;
            return this;
        }

        #endregion


        #region int

        public IHerculesTagsBuilder AddValue(string key, int value)
        {
            Dictionary[key] = value;
            return this;
        }

        public IHerculesTagsBuilder AddVector(string key, IReadOnlyList<int> values)
        {
            Dictionary[key] = values;
            return this;
        }

        #endregion


        #region long

        public IHerculesTagsBuilder AddValue(string key, long value)
        {
            Dictionary[key] = value;
            return this;
        }

        public IHerculesTagsBuilder AddVector(string key, IReadOnlyList<long> values)
        {
            Dictionary[key] = values;
            return this;
        }

        #endregion


        #region float

        public IHerculesTagsBuilder AddValue(string key, float value)
        {
            Dictionary[key] = value;
            return this;
        }

        public IHerculesTagsBuilder AddVector(string key, IReadOnlyList<float> values)
        {
            Dictionary[key] = values;
            return this;
        }

        #endregion


        #region double

        public IHerculesTagsBuilder AddValue(string key, double value)
        {
            Dictionary[key] = value;
            return this;
        }

        public IHerculesTagsBuilder AddVector(string key, IReadOnlyList<double> values)
        {
            Dictionary[key] = values;
            return this;
        }

        #endregion


        #region Guid

        public IHerculesTagsBuilder AddValue(string key, Guid value)
        {
            Dictionary[key] = value;
            return this;
        }

        public IHerculesTagsBuilder AddVector(string key, IReadOnlyList<Guid> values)
        {
            Dictionary[key] = values;
            return this;
        }

        #endregion


        #region string

        public IHerculesTagsBuilder AddValue(string key, string value)
        {
            Dictionary[key] = value;
            return this;
        }

        public IHerculesTagsBuilder AddVector(string key, IReadOnlyList<string> values)
        {
            Dictionary[key] = values;
            return this;
        }

        #endregion


    }
}