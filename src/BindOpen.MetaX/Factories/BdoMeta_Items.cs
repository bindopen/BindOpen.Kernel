using System;
using System.Collections.Generic;

namespace BindOpen.Meta
{
    /// <summary>
    /// This class represents a data item factory.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Get the key values from the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
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
        /// <param name="name"></param>
        /// <param name="namePreffix"></param>
        /// <param name="asClone"></param>
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
