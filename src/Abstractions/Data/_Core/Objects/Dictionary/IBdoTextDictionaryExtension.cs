using BindOpen.System.Data.Helpers;
using System.Collections.Generic;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a dico data item extension.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static class IBdoTextDictionaryExtension
    {
        // Add -----------------------------

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T Add<T>(
            this T dico,
            params KeyValuePair<string, string>[] pairs)
            where T : IBdoTextDictionary
        {
            if (dico != null)
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
        public static T Add<T>(
            this T dico,
            string text)
            where T : IBdoTextDictionary
        {
            dico.Add(StringHelper.__Star, text);

            return dico;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T With<T>(
            this T dico,
            params KeyValuePair<string, string>[] pairs)
            where T : IBdoTextDictionary
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
        public static T With<T>(
            this T dico,
            string text)
            where T : IBdoTextDictionary
        {
            dico.With(StringHelper.__Star, text);

            return dico;
        }

        /// <summary>
        /// Sets the text of the default value.
        /// </summary>
        /// <param key="key">The key of the value to add.</param>
        /// <param key="text">The text of the value to add.</param>
        public static T With<T>(
            this T dico,
            string key,
            string text)
            where T : IBdoTextDictionary
        {
            if (dico != null)
            {
                dico.Clear();
                dico.Add(text);
            }

            return dico;
        }
    }
}
