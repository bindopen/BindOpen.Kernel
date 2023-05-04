using BindOpen.Data;
using BindOpen.Data.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BindOpen.Extensions.Tasks
{
    /// <summary>
    /// This class represents an application 
    /// </summary>
    public static class BdoTaskConfigurationExtensions
    {
        // Inputs

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T AddInputs<T>(
            this T list,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoTaskConfiguration
        {
            if (list != null)
            {
                foreach (var pair in pairs)
                {
                    list.AddInputs(BdoMeta.New(pair.Key, pair.Value));
                }
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddInputs<T>(
            this T list,
            params (string Name, object Value)[] pairs)
            where T : IBdoTaskConfiguration
        {
            if (list != null)
            {
                foreach (var (Name, Value) in pairs)
                {
                    list.AddInputs(BdoMeta.New(Name, Value));
                }
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddInputs<T>(
            this T list,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoTaskConfiguration
        {
            if (list != null)
            {
                foreach (var (Name, ValueType, Value) in pairs)
                {
                    list.AddInputs(BdoMeta.New(Name, ValueType, Value));
                }
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T WithInputs<T>(
            this T list,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoTaskConfiguration
        {
            list?.WithInputs(pairs?.Select(q => BdoMeta.New(q.Key, q.Value)).ToArray());

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithInputs<T>(
            this T list,
            params (string Name, object Value)[] pairs)
            where T : IBdoTaskConfiguration
        {
            list.WithInputs(pairs?.Select(q => BdoMeta.New(q.Name, q.Value)).ToArray());

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithInputs<T>(
            this T list,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoTaskConfiguration
        {
            list.WithInputs(pairs?.Select(q=> BdoMeta.New(q.Name, q.ValueType, q.Value)).ToArray());

            return list;
        }

        // Outputs

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T AddOutputs<T>(
            this T list,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoTaskConfiguration
        {
            if (list != null)
            {
                foreach (var pair in pairs)
                {
                    list.AddOutputs(BdoMeta.New(pair.Key, pair.Value));
                }
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddOutputs<T>(
            this T list,
            params (string Name, object Value)[] pairs)
            where T : IBdoTaskConfiguration
        {
            if (list != null)
            {
                foreach (var (Name, Value) in pairs)
                {
                    list.AddOutputs(BdoMeta.New(Name, Value));
                }
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T AddOutputs<T>(
            this T list,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoTaskConfiguration
        {
            if (list != null)
            {
                foreach (var (Name, ValueType, Value) in pairs)
                {
                    list.AddOutputs(BdoMeta.New(Name, ValueType, Value));
                }
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T WithOutputs<T>(
            this T list,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoTaskConfiguration
        {
            list?.WithOutputs(pairs?.Select(q => BdoMeta.New(q.Key, q.Value)).ToArray());

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithOutputs<T>(
            this T list,
            params (string Name, object Value)[] pairs)
            where T : IBdoTaskConfiguration
        {
            list.WithOutputs(pairs?.Select(q => BdoMeta.New(q.Name, q.Value)).ToArray());

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T WithOutputs<T>(
            this T list,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoTaskConfiguration
        {
            list.WithOutputs(pairs?.Select(q => BdoMeta.New(q.Name, q.ValueType, q.Value)).ToArray());

            return list;
        }
    }
}
