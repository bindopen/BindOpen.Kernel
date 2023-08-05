using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// This class represents an application 
    /// </summary>
    public static partial class BdoTaskExtensions
    {
        // Properties

        public static T WithProperties<T>(
            this T obj,
            params IBdoMetaData[] inputs)
            where T : IBdoMetaObject
        {
            if (obj != null)
            {
                obj.Remove(q => q.OfGroup(null));
                Array.ForEach(inputs, q => { q.AsProperty(); });
                obj.Add(inputs);
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
            params IBdoMetaData[] inputs)
            where T : IBdoMetaObject
        {
            if (obj != null)
            {

                Array.ForEach(inputs, q => { q.AsProperty(); });
                obj.Add(inputs);
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
            if (obj != null)
            {
                foreach (var pair in pairs)
                {
                    obj.AddProperties(BdoData.NewMeta(pair.Key, pair.Value));
                }
            }

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
            if (obj != null)
            {
                foreach (var (Name, Value) in pairs)
                {
                    obj.AddProperties(BdoData.NewMeta(Name, Value));
                }
            }

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
            if (obj != null)
            {
                foreach (var (Name, ValueType, Value) in pairs)
                {
                    obj.AddProperties(BdoData.NewMeta(Name, ValueType, Value));
                }
            }

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
                Array.ForEach(inputs, q => { q.AsInput(); });
                obj.Add(inputs);
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
            if (obj != null)
            {
                foreach (var pair in pairs)
                {
                    obj.AddInputs(BdoData.NewMeta(pair.Key, pair.Value));
                }
            }

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
            if (obj != null)
            {
                foreach (var (Name, Value) in pairs)
                {
                    obj.AddInputs(BdoData.NewMeta(Name, Value));
                }
            }

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
            if (obj != null)
            {
                foreach (var (Name, ValueType, Value) in pairs)
                {
                    obj.AddInputs(BdoData.NewMeta(Name, ValueType, Value));
                }
            }

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
                Array.ForEach(outputs, q => { q.AsOutput(); });
                obj.Add(outputs);
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
            if (obj != null)
            {
                foreach (var pair in pairs)
                {
                    obj.AddOutputs(BdoData.NewMeta(pair.Key, pair.Value));
                }
            }

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
            if (obj != null)
            {
                foreach (var (Name, Value) in pairs)
                {
                    obj.AddOutputs(BdoData.NewMeta(Name, Value));
                }
            }

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
            if (obj != null)
            {
                foreach (var (Name, ValueType, Value) in pairs)
                {
                    obj.AddOutputs(BdoData.NewMeta(Name, ValueType, Value));
                }
            }

            return obj;
        }
    }
}
