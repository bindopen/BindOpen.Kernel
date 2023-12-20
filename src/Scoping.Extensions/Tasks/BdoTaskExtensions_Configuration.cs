using BindOpen.Data.Meta;
using BindOpen.Scoping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents an application 
    /// </summary>
    public static class BdoTaskExtensions_Configuration
    {
        // Get

        public static ITBdoSet<IBdoMetaData> Properties(this IBdoMetaSet set)
            => BdoData.NewSet(set?.Where(q => q.IsProperty())?.ToArray());

        public static ITBdoSet<IBdoMetaData> Inputs(this IBdoMetaSet set)
            => BdoData.NewSet(set?.Where(q => q.IsInput())?.ToArray());

        public static ITBdoSet<IBdoMetaData> Outputs(this IBdoMetaSet set)
            => BdoData.NewSet(set?.Where(q => q.IsOutput())?.ToArray());

        // Is

        public static bool IsProperty(this IBdoMetaData meta)
            => meta.OfGroup(null);

        public static bool IsInput(this IBdoMetaData meta)
            => meta.OfGroup(IBdoTaskExtensions.__Token_Input);

        public static bool IsOutput(this IBdoMetaData meta)
            => meta.OfGroup(IBdoTaskExtensions.__Token_Output);

        // As

        public static IBdoMetaData AsProperty(
            this IBdoMetaData meta)
        {
            meta?.WithGroupId(null);

            return meta;
        }

        public static IBdoMetaData AsInput(
            this IBdoMetaData meta)
        {
            meta?.WithGroupId(IBdoTaskExtensions.__Token_Input);

            return meta;
        }

        public static IBdoMetaData AsOutput(
            this IBdoMetaData meta)
        {
            meta?.WithGroupId(IBdoTaskExtensions.__Token_Output);

            return meta;
        }

        // Properties

        public static T WithProperties<T>(
            this T obj,
            params IBdoMetaData[] props)
            where T : IBdoMetaObject
        {
            if (obj != null)
            {
                obj.Remove(q => q.OfGroup(null));
                obj.AddProperties(props);
            }

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T WithProperties<T>(
            this T obj,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoMetaObject
        {
            obj?.WithProperties(pairs?.Select(q => BdoData.NewMeta(q.Key, q.Value)).ToArray());

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithProperties<T>(
            this T obj,
            params (string Name, object Value)[] pairs)
            where T : IBdoMetaObject
        {
            obj?.WithProperties(pairs?.Select(q => BdoData.NewMeta(q.Name, q.Value)).ToArray());

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithProperties<T>(
            this T obj,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoMetaObject
        {
            obj?.WithProperties(pairs?.Select(q => BdoData.NewMeta(q.Name, q.ValueType, q.Value)).ToArray());

            return obj;
        }

        public static T AddProperties<T>(
            this T obj,
            params IBdoMetaData[] props)
            where T : IBdoMetaObject
        {
            if (obj != null)
            {

                Array.ForEach(props, q => { q.AsProperty(); });
                obj.Add(props);
            }

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T AddProperties<T>(
            this T obj,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoMetaObject
        {
            obj?.AddProperties(pairs?.Select(q => BdoData.NewMeta(q.Key, q.Value)).ToArray());

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddProperties<T>(
            this T obj,
            params (string Name, object Value)[] pairs)
            where T : IBdoMetaObject
        {
            obj?.AddProperties(pairs?.Select(q => BdoData.NewMeta(q.Name, q.Value)).ToArray());

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddProperties<T>(
            this T obj,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoMetaObject
        {
            obj?.AddProperties(pairs?.Select(q => BdoData.NewMeta(q.Name, q.ValueType, q.Value)).ToArray());

            return obj;
        }

        // Inputs

        public static T WithInputs<T>(
            this T obj,
            params IBdoMetaData[] inputs)
            where T : IBdoMetaObject
        {
            if (obj != null)
            {
                obj.Remove(q => q.OfGroup(IBdoTaskExtensions.__Token_Input));
                obj.AddInputs(inputs);
            }

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T WithInputs<T>(
            this T obj,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoMetaObject
        {
            obj?.WithInputs(pairs?.Select(q => BdoData.NewMeta(q.Key, q.Value)).ToArray());

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithInputs<T>(
            this T obj,
            params (string Name, object Value)[] pairs)
            where T : IBdoMetaObject
        {
            obj?.WithInputs(pairs?.Select(q => BdoData.NewMeta(q.Name, q.Value)).ToArray());

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithInputs<T>(
            this T obj,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoMetaObject
        {
            obj?.WithInputs(pairs?.Select(q => BdoData.NewMeta(q.Name, q.ValueType, q.Value)).ToArray());

            return obj;
        }

        public static T AddInputs<T>(
            this T obj,
            params IBdoMetaData[] inputs)
            where T : IBdoMetaObject
        {
            if (obj != null)
            {

                Array.ForEach(inputs, q => { q.AsInput(); });
                obj.Add(inputs);
            }

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T AddInputs<T>(
            this T obj,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoMetaObject
        {
            obj?.AddInputs(pairs?.Select(q => BdoData.NewMeta(q.Key, q.Value)).ToArray());

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddInputs<T>(
            this T obj,
            params (string Name, object Value)[] pairs)
            where T : IBdoMetaObject
        {
            obj?.AddInputs(pairs?.Select(q => BdoData.NewMeta(q.Name, q.Value)).ToArray());

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddInputs<T>(
            this T obj,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoMetaObject
        {
            obj?.AddInputs(pairs?.Select(q => BdoData.NewMeta(q.Name, q.ValueType, q.Value)).ToArray());

            return obj;
        }

        // Outputs

        public static T WithOutputs<T>(
            this T obj,
            params IBdoMetaData[] outputs)
            where T : IBdoMetaObject
        {
            if (obj != null)
            {
                obj.Remove(q => q.OfGroup(IBdoTaskExtensions.__Token_Output));
                obj.AddOutputs(outputs);
            }

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T WithOutputs<T>(
            this T obj,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoMetaObject
        {
            obj?.WithOutputs(pairs?.Select(q => BdoData.NewMeta(q.Key, q.Value)).ToArray());

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithOutputs<T>(
            this T obj,
            params (string Name, object Value)[] pairs)
            where T : IBdoMetaObject
        {
            obj?.WithOutputs(pairs?.Select(q => BdoData.NewMeta(q.Name, q.Value)).ToArray());

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithOutputs<T>(
            this T obj,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoMetaObject
        {
            obj?.WithOutputs(pairs?.Select(q => BdoData.NewMeta(q.Name, q.ValueType, q.Value)).ToArray());

            return obj;
        }

        public static T AddOutputs<T>(
            this T obj,
            params IBdoMetaData[] outputs)
            where T : IBdoMetaObject
        {
            if (obj != null)
            {
                Array.ForEach(outputs, q => { q.AsOutput(); });
                obj.Add(outputs);
            }

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T AddOutputs<T>(
            this T obj,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoMetaObject
        {
            obj?.AddOutputs(pairs?.Select(q => BdoData.NewMeta(q.Key, q.Value)).ToArray());

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddOutputs<T>(
            this T obj,
            params (string Name, object Value)[] pairs)
            where T : IBdoMetaObject
        {
            obj?.AddOutputs(pairs?.Select(q => BdoData.NewMeta(q.Name, q.Value)).ToArray());

            return obj;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddOutputs<T>(
            this T obj,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoMetaObject
        {
            obj?.AddOutputs(pairs?.Select(q => BdoData.NewMeta(q.Name, q.ValueType, q.Value)).ToArray());

            return obj;
        }
    }
}
