using System.Collections.Generic;

namespace BindOpen.System.Data
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
        public static BdoTextDictionary NewDictionary()
        {
            var dico = new BdoTextDictionary();
            return dico;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param key="values">The values to consider.</param>
        public static BdoTextDictionary NewDictionary(params KeyValuePair<string, string>[] pairs)
        {
            var dico = new BdoTextDictionary();
            dico.With(pairs);

            return dico;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param key="values">The values to consider.</param>
        public static BdoTextDictionary NewDictionary(params (string Key, string Value)[] pairs)
        {
            var dico = new BdoTextDictionary();
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
        public static BdoTextDictionary NewDictionary(string text)
        {
            var dico = new BdoTextDictionary();
            dico.With(text);

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
