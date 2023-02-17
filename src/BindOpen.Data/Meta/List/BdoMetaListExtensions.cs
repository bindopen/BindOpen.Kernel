using BindOpen.Data.Meta;
using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class BdoMetaListExtensions
    {
        // Add

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param name="pairs">The value to add.</param>
        public static T Add<T>(
            this T list,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoMetaList
        {
            if (list != null)
            {
                foreach (var pair in pairs)
                {
                    list.Add(pair.Key, pair.Value);
                }
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T Add<T>(
            this T list,
            object value)
            where T : IBdoMetaList
        {
            list?.Add(null, value);

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        /// <param name="availableKeys">The available keys to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T Add<T>(
            this T list,
            string key, object value)
            where T : IBdoMetaList
        {
            list?.Add(BdoMeta.New(key, value));

            return list;
        }

        // With

        /// <summary>
        /// Withs a new value to this instance.
        /// </summary>
        /// <param name="pairs">The value to add.</param>
        public static T With<T>(
            this T list,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoMetaList
        {
            if (list != null)
            {
                foreach (var pair in pairs)
                {
                    list.With(pair.Key, pair.Value);
                }
            }

            return list;
        }

        /// <summary>
        /// Withs a new value to this instance with the specified key and text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T With<T>(
            this T list,
            object value)
            where T : IBdoMetaList
        {
            list?.With(null, value);

            return list;
        }

        /// <summary>
        /// Withs a new value to this instance with the specified key and text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        /// <param name="availableKeys">The available keys to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T With<T>(
            this T list,
            string key, object value)
            where T : IBdoMetaList
        {
            list?.With(BdoMeta.New(key, value));

            return list;
        }
    }
}