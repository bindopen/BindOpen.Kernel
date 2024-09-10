using BindOpen.Data.Helpers;
using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a dico data item extension.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static class ITBdoDictionaryExtension
    {
        // Add -----------------------------

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T Add<T, TItem>(
            this T dico,
            params KeyValuePair<string, TItem>[] pairs)
            where T : ITBdoDictionary<TItem>
        {
            if (dico != null && pairs != null)
            {
                foreach (var value in pairs)
                {
                    dico.Add(value.Key, value.Value);
                }
            }

            return dico;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T Add<T, TItem>(
            this T dico,
            TItem item)
            where T : ITBdoDictionary<TItem>
        {
            dico.Add(StringHelper.__Star, item);

            return dico;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T With<T, TItem>(
            this T dico,
            params KeyValuePair<string, TItem>[] pairs)
            where T : ITBdoDictionary<TItem>
        {
            if (dico != null)
            {
                dico.Clear();
                dico.Add(pairs);
            }

            return dico;
        }

        /// <summary>
        /// Sets the text of the default value.
        /// </summary>
        /// <param key="text">The text of the value to add.</param>
        public static T With<T, TItem>(
            this T dico,
            TItem item)
            where T : ITBdoDictionary<TItem>
        {
            dico.With(StringHelper.__Star, item);

            return dico;
        }

        /// <summary>
        /// Sets the text of the default value.
        /// </summary>
        /// <param key="key">The key of the value to add.</param>
        /// <param key="text">The text of the value to add.</param>
        public static T With<T, TItem>(
            this T dico,
            string key,
            TItem item)
            where T : ITBdoDictionary<TItem>
        {
            if (dico != null)
            {
                dico.Clear();
                dico.Add(item);
            }

            return dico;
        }
    }
}
