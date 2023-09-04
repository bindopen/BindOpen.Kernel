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
        public static TBdoDictionary<T> NewDictionary<T>()
        {
            var dico = new TBdoDictionary<T>();
            return dico;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param key="values">The values to consider.</param>
        public static TBdoDictionary<T> NewDictionary<T>(params KeyValuePair<string, T>[] pairs)
        {
            var dico = new TBdoDictionary<T>();
            dico.With(pairs);

            return dico;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param key="values">The values to consider.</param>
        public static TBdoDictionary<T> NewDictionary<T>(params (string Key, T Value)[] pairs)
        {
            var dico = new TBdoDictionary<T>();
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
        public static TBdoDictionary<T> NewDictionary<T>(T item)
        {
            var dico = new TBdoDictionary<T>();
            dico.With(item);

            return dico;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param key="values">The values to consider.</param>
        public static KeyValuePair<string, T> NewKeyPair<T>(string name, T item)
        {
            var pair = new KeyValuePair<string, T>(name, item);

            return pair;
        }
    }
}
