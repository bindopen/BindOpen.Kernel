using System.Collections.Generic;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data item factory.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static partial class BdoItems
    {
        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        public static BdoDictionary NewDictionary()
        {
            var dictionary = new BdoDictionary();
            return dictionary;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public static BdoDictionary NewDictionary(params KeyValuePair<string, string>[] pairs)
        {
            var dictionary = new BdoDictionary();
            dictionary.Set(pairs);

            return dictionary;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public static BdoDictionary NewDictionary(params (string Key, string Value)[] pairs)
        {
            var dictionary = new BdoDictionary();
            foreach (var pair in pairs)
            {
                dictionary.Add(pair.Key, pair.Value);
            }

            return dictionary;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public static BdoDictionary NewDictionary(string text)
        {
            var dictionary = new BdoDictionary();
            dictionary.Set(text);

            return dictionary;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public static KeyValuePair<string, string> NewKeyPair(string name, string value)
        {
            var pair = new KeyValuePair<string, string>(name, value);

            return pair;
        }
    }
}
