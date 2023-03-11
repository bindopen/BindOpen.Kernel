using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data item factory.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static partial class BdoData
    {
        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        public static BdoDictionary NewDictionary()
        {
            var dico = new BdoDictionary();
            return dico;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param key="values">The values to consider.</param>
        public static BdoDictionary NewDictionary(params KeyValuePair<string, string>[] pairs)
        {
            var dico = new BdoDictionary();
            dico.Set(pairs);

            return dico;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param key="values">The values to consider.</param>
        public static BdoDictionary NewDictionary(params (string Key, string Value)[] pairs)
        {
            var dico = new BdoDictionary();
            foreach (var pair in pairs)
            {
                dico.Add(pair.Key, pair.Value);
            }

            return dico;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param key="values">The values to consider.</param>
        public static BdoDictionary NewDictionary(string text)
        {
            var dico = new BdoDictionary();
            dico.Set(text);

            return dico;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param key="values">The values to consider.</param>
        public static KeyValuePair<string, string> NewKeyPair(string name, string value)
        {
            var pair = new KeyValuePair<string, string>(name, value);

            return pair;
        }
    }
}
