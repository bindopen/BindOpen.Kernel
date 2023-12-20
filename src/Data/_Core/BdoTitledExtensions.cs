using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class BdoTitledExtensions
    {
        public static T AddTitle<T>(
            this T obj,
            KeyValuePair<string, string> item)
            where T : IBdoTitled
        {
            if (obj != null)
            {
                obj.Title ??= BdoData.NewDictionary<string>();
                obj.Title.Add(item);
            }

            return obj;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param key="values">The values to consider.</param>
        public static T WithTitle<T>(
            this T obj,
            string st)
            where T : IBdoTitled
        {
            if (obj != null)
            {
                obj.Title = BdoData.NewDictionary<string>(st);
            }

            return obj;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param key="values">The values to consider.</param>
        public static T WithTitle<T>(
            this T obj,
            params KeyValuePair<string, string>[] pairs)
            where T : IBdoTitled
        {
            if (obj != null)
            {
                obj.Title = BdoData.NewDictionary<string>(pairs);
            }

            return obj;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param key="values">The values to consider.</param>
        public static T WithTitle<T>(
            this T obj,
            params (string Key, string Value)[] pairs)
            where T : IBdoTitled
        {
            if (obj != null)
            {
                obj.Title = BdoData.NewDictionary<string>(pairs);
            }

            return obj;
        }
    }
}
