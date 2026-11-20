using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines an described data item.
    /// </summary>
    public static class BdoDescribedExtensions
    {
        public static T AddDescription<T>(
            this T obj,
            KeyValuePair<string, string> item)
            where T : IBdoDescribed
        {
            if (obj != null)
            {
                obj.Description ??= BdoData.NewDictionary<string>();
                obj.Description.Add(item);
            }

            return obj;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param key="values">The values to consider.</param>
        public static T WithDescription<T>(
            this T obj,
            string st)
            where T : IBdoDescribed
        {
            if (obj != null)
            {
                obj.Description = BdoData.NewDictionary<string>(st);
            }

            return obj;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param key="values">The values to consider.</param>
        public static T WithDescription<T>(
            this T obj,
            params KeyValuePair<string, string>[] pairs)
            where T : IBdoDescribed
        {
            if (obj != null)
            {
                obj.Description = BdoData.NewDictionary<string>(pairs);
            }

            return obj;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param key="values">The values to consider.</param>
        public static T WithDescription<T>(
            this T obj,
            params (string Key, string Value)[] pairs)
            where T : IBdoDescribed
        {
            if (obj != null)
            {
                obj.Description = BdoData.NewDictionary<string>(pairs);
            }

            return obj;
        }
    }
}
