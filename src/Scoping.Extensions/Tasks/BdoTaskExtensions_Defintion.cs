using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Scoping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents an application 
    /// </summary>
    public static class BdoTaskExtensions_Definition
    {
        // Get

        public static ITBdoSet<IBdoSpec> Properties(this IBdoSpecSet set)
            => BdoData.NewSpecSet(set?.Where(q => q.IsProperty())?.ToArray());

        public static ITBdoSet<IBdoSpec> Inputs(this IBdoSpecSet set)
            => BdoData.NewSpecSet(set?.Where(q => q.IsInput())?.ToArray());

        public static ITBdoSet<IBdoSpec> Outputs(this IBdoSpecSet set)
            => BdoData.NewSpecSet(set?.Where(q => q.IsOutput())?.ToArray());

        // Is

        public static bool IsProperty(this IBdoSpec spec)
            => spec.OfGroup(null);

        public static bool IsInput(this IBdoSpec spec)
            => spec.OfGroup(IBdoTaskExtensions.__Token_Input);

        public static bool IsOutput(this IBdoSpec spec)
            => spec.OfGroup(IBdoTaskExtensions.__Token_Output);

        // As

        public static IBdoSpec AsProperty(
            this IBdoSpec spec)
        {
            spec?.WithGroupId(null);

            return spec;
        }

        public static IBdoSpec AsInput(
            this IBdoSpec spec)
        {
            spec?.WithGroupId(IBdoTaskExtensions.__Token_Input);

            return spec;
        }

        public static IBdoSpec AsOutput(
            this IBdoSpec spec)
        {
            spec?.WithGroupId(IBdoTaskExtensions.__Token_Output);

            return spec;
        }

        // Properties

        public static T WithProperties<T>(
            this T set,
            params IBdoSpec[] props)
            where T : IBdoSpec
        {
            if (set != null)
            {
                set.RemoveChildren(q => q.OfGroup(null));
                set.AddProperties(props);
            }

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithProperties<T>(
            this T set,
            params (string Name, DataValueTypes ValueType)[] pairs)
            where T : IBdoSpec
        {
            set?.WithProperties(pairs?.Select(q => BdoData.NewSpec(q.Name, q.ValueType)).ToArray());

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T WithProperties<T>(
            this T set,
            params KeyValuePair<string, DataValueTypes>[] pairs)
            where T : IBdoSpec
        {
            set?.WithProperties(pairs?.Select(q => BdoData.NewSpec(q.Key, q.Value)).ToArray());

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithProperties<T>(
            this T set,
            params (string Name, DataValueTypes ValueType, object DefaultData)[] pairs)
            where T : IBdoSpec
        {
            set?.WithProperties(pairs?.Select(q => BdoData.NewSpec(q.Name, q.ValueType, q.DefaultData)).ToArray());

            return set;
        }

        public static T AddProperties<T>(
            this T set,
            params IBdoSpec[] props)
            where T : IBdoSpec
        {
            if (set != null)
            {
                Array.ForEach(props, q => { q.AsProperty(); });
                set.AddChildren(props);
            }

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddProperties<T>(
            this T set,
            params (string Name, DataValueTypes ValueType)[] pairs)
            where T : IBdoSpec
        {
            set?.AddProperties(pairs?.Select(q => BdoData.NewSpec(q.Name, q.ValueType)).ToArray());

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T AddProperties<T>(
            this T set,
            params KeyValuePair<string, DataValueTypes>[] pairs)
            where T : IBdoSpec
        {
            set?.AddProperties(pairs?.Select(q => BdoData.NewSpec(q.Key, q.Value)).ToArray());

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddProperties<T>(
            this T set,
            params (string Name, DataValueTypes ValueType, object DefaultData)[] pairs)
            where T : IBdoSpec
        {
            set?.AddProperties(pairs?.Select(q => BdoData.NewSpec(q.Name, q.ValueType, q.DefaultData)).ToArray());

            return set;
        }

        // Inputs

        public static T WithInputs<T>(
            this T set,
            params IBdoSpec[] inputs)
            where T : IBdoSpec
        {
            if (set != null)
            {
                set.RemoveChildren(q => q.OfGroup(IBdoTaskExtensions.__Token_Input));
                set.AddInputs(inputs);
            }

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithInputs<T>(
            this T set,
            params (string Name, DataValueTypes ValueType)[] pairs)
            where T : IBdoSpec
        {
            set?.WithInputs(pairs?.Select(q => BdoData.NewSpec(q.Name, q.ValueType)).ToArray());

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T WithInputs<T>(
            this T set,
            params KeyValuePair<string, DataValueTypes>[] pairs)
            where T : IBdoSpec
        {
            set?.WithInputs(pairs?.Select(q => BdoData.NewSpec(q.Key, q.Value)).ToArray());

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithInputs<T>(
            this T set,
            params (string Name, DataValueTypes ValueType, object DefaultData)[] pairs)
            where T : IBdoSpec
        {
            set?.WithInputs(pairs?.Select(q => BdoData.NewSpec(q.Name, q.ValueType, q.DefaultData)).ToArray());

            return set;
        }

        public static T AddInputs<T>(
            this T set,
            params IBdoSpec[] inputs)
            where T : IBdoSpec
        {
            if (set != null)
            {

                Array.ForEach(inputs, q => { q.AsInput(); });
                set.AddChildren(inputs);
            }

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddInputs<T>(
            this T set,
            params (string Name, DataValueTypes ValueType)[] pairs)
            where T : IBdoSpec
        {
            set?.AddInputs(pairs?.Select(q => BdoData.NewSpec(q.Name, q.ValueType)).ToArray());

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T AddInputs<T>(
            this T set,
            params KeyValuePair<string, DataValueTypes>[] pairs)
            where T : IBdoSpec
        {
            set?.AddInputs(pairs?.Select(q => BdoData.NewSpec(q.Key, q.Value)).ToArray());

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddInputs<T>(
            this T set,
            params (string Name, DataValueTypes ValueType, object DefaultData)[] pairs)
            where T : IBdoSpec
        {
            set?.AddInputs(pairs?.Select(q => BdoData.NewSpec(q.Name, q.ValueType, q.DefaultData)).ToArray());

            return set;
        }

        // Outputs

        public static T WithOutputs<T>(
            this T set,
            params IBdoSpec[] outputs)
            where T : IBdoSpec
        {
            if (set != null)
            {
                set.RemoveChildren(q => q.OfGroup(IBdoTaskExtensions.__Token_Output));
                set.AddOutputs(outputs);
            }

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithOutputs<T>(
            this T set,
            params (string Name, DataValueTypes ValueType)[] pairs)
            where T : IBdoSpec
        {
            set?.WithOutputs(pairs?.Select(q => BdoData.NewSpec(q.Name, q.ValueType)).ToArray());

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T WithOutputs<T>(
            this T set,
            params KeyValuePair<string, DataValueTypes>[] pairs)
            where T : IBdoSpec
        {
            set?.WithOutputs(pairs?.Select(q => BdoData.NewSpec(q.Key, q.Value)).ToArray());

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithOutputs<T>(
            this T set,
            params (string Name, DataValueTypes ValueType, object DefaultData)[] pairs)
            where T : IBdoSpec
        {
            set?.WithOutputs(pairs?.Select(q => BdoData.NewSpec(q.Name, q.ValueType, q.DefaultData)).ToArray());

            return set;
        }

        public static T AddOutputs<T>(
            this T set,
            params IBdoSpec[] outputs)
            where T : IBdoSpec
        {
            if (set != null)
            {

                Array.ForEach(outputs, q => { q.AsOutput(); });
                set.AddChildren(outputs);
            }

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddOutputs<T>(
            this T set,
            params (string Name, DataValueTypes ValueType)[] pairs)
            where T : IBdoSpec
        {
            set?.AddOutputs(pairs?.Select(q => BdoData.NewSpec(q.Name, q.ValueType)).ToArray());

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T AddOutputs<T>(
            this T set,
            params KeyValuePair<string, DataValueTypes>[] pairs)
            where T : IBdoSpec
        {
            set?.AddOutputs(pairs?.Select(q => BdoData.NewSpec(q.Key, q.Value)).ToArray());

            return set;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddOutputs<T>(
            this T set,
            params (string Name, DataValueTypes ValueType, object DefaultData)[] pairs)
            where T : IBdoSpec
        {
            set?.AddOutputs(pairs?.Select(q => BdoData.NewSpec(q.Name, q.ValueType, q.DefaultData)).ToArray());

            return set;
        }
    }
}
