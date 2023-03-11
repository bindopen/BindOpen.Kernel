using System;
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
        /// Get the key values from the specified string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        /// <returns>Returns the added string items.</returns>
        public static List<KeyValuePair<string, string>> ToKeyValues(this string st)
        {
            List<KeyValuePair<string, string>> pairs = new();
            foreach (var subSt in st.Split('|'))
            {
                if (subSt.Contains('='))
                {
                    int i = subSt.IndexOf("=");
                    pairs.Add(
                        new KeyValuePair<string, string>(subSt[..i].Trim(), subSt[(i + 1)..].Trim()));
                }
            }

            return pairs;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="namePreffix"></param>
        /// <param key="asClone"></param>
        /// <returns></returns>
        public static string NewName(
            string name,
            string namePreffix = null,
            bool asClone = false)
        {
            return string.IsNullOrEmpty(name) ?
                (namePreffix ?? string.Empty) + DateTime.Now.Ticks.ToString() :
                (asClone ? "copy_" : "") + name;
        }

    }
}
