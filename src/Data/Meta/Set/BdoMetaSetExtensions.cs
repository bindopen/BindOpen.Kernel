using BindOpen.System.Data.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class BdoMetaSetExtensions
    {
        // Add

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T InsertData<T>(
            this T list,
            object obj)
            where T : IBdoMetaSet
        {
            IBdoMetaData meta = obj is IBdoMetaData data ? data : BdoData.NewMeta(obj);
            list?.Add(meta);

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T Add<T>(
            this T list,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoMetaSet
        {
            if (list != null)
            {
                foreach (var pair in pairs)
                {
                    list.Add(BdoData.NewMeta(pair.Key, pair.Value));
                }
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T Add<T>(
            this T list,
            params (string Name, object Value)[] pairs)
            where T : IBdoMetaSet
        {
            if (list != null)
            {
                foreach (var (Name, Value) in pairs)
                {
                    list.Add(BdoData.NewMeta(Name, Value));
                }
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T Add<T>(
            this T list,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoMetaSet
        {
            if (list != null)
            {
                foreach (var (Name, ValueType, Value) in pairs)
                {
                    list.Add(BdoData.NewMeta(Name, ValueType, Value));
                }
            }

            return list;
        }

        // With

        /// <summary>
        /// Withs a new value to this instance.
        /// </summary>
        /// <param key="pairs">The value to add.</param>
        public static T With<T>(
            this T list,
            params KeyValuePair<string, object>[] pairs)
            where T : IBdoMetaSet
        {
            if (list != null)
            {
                list.Clear();
                list.Add(pairs);
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T With<T>(
            this T list,
            params (string Name, object Value)[] pairs)
            where T : IBdoMetaSet
        {
            if (list != null)
            {
                list.Clear();
                list.Add(pairs);
            }

            return list;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public static T With<T>(
            this T list,
            params (string Name, DataValueTypes ValueType, object Value)[] pairs)
            where T : IBdoMetaSet
        {
            if (list != null)
            {
                var items = pairs.Select(q => BdoData.NewMeta(q.Name, q.ValueType, q.Value)).ToArray();
                list.With(items);
            }

            return list;
        }

        /// <summary>
        /// Gets the quoted string.
        /// </summary>
        /// <param key="st">The string to normalize.</param>
        /// <returns>Returns the quoted string.</returns>
        public static IBdoMetaSet ExtractTokens(this string st, string pattern, char quote = '\'')
        {
            var set = BdoData.NewMetaSet();

            if (!string.IsNullOrEmpty(st))
            {
                int i = 0;
                int ii = -1;
                int ji = 0;
                string tokenName = null;

                while (i > -1)
                {
                    i = pattern.IndexOfNextString("{{", i);

                    if (ii == -1)
                    {
                        ji = i;
                    }

                    if (ii > -1)
                    {
                        var word = pattern.ToSubstring(ii, i == -1 ? pattern.Length - 1 : i - 1);
                        var wordst = word.ToUnquoted(quote);
                        var jj = string.IsNullOrEmpty(wordst) ? st.Length : st.IndexOfNextString(wordst, ji, quote: quote);
                        var tokenValue = st.ToSubstring(ji, jj - 1).ToUnquoted(quote);
                        set.Add((tokenName, tokenValue));
                        ji = jj + word.Length;
                    }

                    if (i > -1)
                    {
                        int j = pattern.IndexOfNextString("}}", i + 2);

                        if (j > -1)
                        {
                            tokenName = pattern.ToSubstring(i + 2, j - 1);
                            ii = j + 2;
                        }
                        i++;
                    }
                }
            }

            return set;
        }
    }
}