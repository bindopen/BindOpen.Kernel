using System;
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
        /// Get the key values from the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <returns>Returns the added string items.</returns>
        public static List<KeyValuePair<string, string>> ToKeyValues(this string st)
        {
            List<KeyValuePair<string, string>> pairs = new();
            foreach (var subString in st.Split('|'))
            {
                if (subString.Contains('='))
                {
                    int i = subString.IndexOf("=");
                    pairs.Add(
                        new KeyValuePair<string, string>(subString[..i].Trim(), subString[(i + 1)..].Trim()));
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
