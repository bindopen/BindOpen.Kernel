using BindOpen.Data.Meta;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class BdoMetaSetExtensions
    {
        // Add

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T AddData<T>(
            this T list,
            object obj)
            where T : IBdoMetaSet
        {
            list?.Add(BdoMeta.New(obj));

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T Add<T>(
            this T list,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoMetaSet
        {
            if (list != null)
            {
                foreach (var pair in pairs)
                {
                    list.Add(BdoMeta.New(pair.Key, pair.Value));
                }
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T Add<T>(
            this T list,
            params (string Name, object Value)[] pairs)
            where T : IBdoMetaSet
        {
            if (list != null)
            {
                foreach (var (Name, Value) in pairs)
                {
                    list.Add(BdoMeta.New(Name, Value));
                }
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T Add<T>(
            this T list,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoMetaSet
        {
            if (list != null)
            {
                foreach (var (Name, ValueType, Value) in pairs)
                {
                    list.Add(BdoMeta.New(Name, ValueType, Value));
                }
            }

            return list;
        }

        // With

        /// <summary>
        /// Withs a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T With<T>(
            this T list,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoMetaSet
        {
            if (list != null)
            {
                var items = pairs.Select(q => BdoMeta.New(q.Key, q.Value)).ToArray();
                list.With(items);
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T With<T>(
            this T list,
            params (string Name, object Value)[] pairs)
            where T : IBdoMetaSet
        {
            if (list != null)
            {
                var items = pairs.Select(q => BdoMeta.New(q.Name, q.Value)).ToArray();
                list.With(items);
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T With<T>(
            this T list,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoMetaSet
        {
            if (list != null)
            {
                var items = pairs.Select(q => BdoMeta.New(q.Name, q.ValueType, q.Value)).ToArray();
                list.With(items);
            }

            return list;
        }
    }
}