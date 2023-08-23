using System;

namespace BindOpen.System.Data.Helpers
{
    /// <summary>
    /// This class represents a helper for objects.
    /// </summary>
    public static partial class StringHelper
    {
        /// <summary>
        /// Gets the quoted string.
        /// </summary>
        /// <param key="st">The string to normalize.</param>
        /// <returns>Returns the quoted string.</returns>
        public static string ToQuoted(this string st, char quote = '\'')
        {
            string quotes = string.Concat(quote, quote);
            return quote + (st ?? string.Empty).Replace(quote.ToString(), quotes) + quote;
        }

        /// <summary>
        /// Gets the unquoted string.
        /// </summary>
        /// <param key="st">The string to normalize.</param>
        /// <param key="quoteChar">The quote character to consider.</param>
        /// <returns>Returns the quoted string.</returns>
        public static string ToUnquoted(this string st, char quote = '\'')
        {
            if (st == null) return null;

            if (st.Length > 2 && st.StartsWith(quote) && st.EndsWith(quote))
            {
                string quotes = string.Concat(quote, quote);
                st = st[1..^1].Replace(quotes, quote.ToString());
            }
            return st;
        }

        /// <summary>
        /// Gets the specified sub string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        /// <param key="startIndex">The start index to consider.</param>
        /// <param key="endIndex">The end index to consider.</param>
        /// <returns>The formated path.</returns>
        public static string ToSubstring(this string st, int startIndex, int endIndex = -1)
        {
            if (st == null) return null;

            if (startIndex < 0) startIndex = 0;
            if (endIndex == -1 || endIndex >= st.Length) endIndex = st.Length - 1;
            if (st?.Length == 0 || startIndex >= st.Length || endIndex < startIndex) return string.Empty;

            return st.Substring(startIndex, endIndex - startIndex + 1);
        }

        /// <summary>
        /// Gets the string with the specified starting string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        /// <param key="startingString">The starting string to consider.</param>
        /// <returns>The formated path.</returns>
        public static string StartingWith(this string st, string startingString)
        {
            return string.IsNullOrEmpty(st) ? string.Empty :
                startingString != null && st.StartsWith(startingString) ? st : startingString + st;
        }

        /// <summary>
        /// Gets the string with the specified ending string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        /// <param key="endingString">The ending string to consider.</param>
        /// <returns>The formated path.</returns>
        public static string EndingWith(this string st, string endingString)
        {
            return string.IsNullOrEmpty(st) ? string.Empty :
                endingString != null && st.EndsWith(endingString) ? st : st + endingString;
        }

        /// <summary>
        /// Gets the index of the last sub string in the specified string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        /// <param key="stv">The string to search.</param>
        /// <param key="startIndex">The start index to consider.</param>
        /// <returns>The formated string.</returns>
        public static void IndexOfLastString(this string st, string stv, ref int startIndex)
        {
            startIndex = st.IndexOfLastString(stv, startIndex);
        }

        /// <summary>
        /// Gets the index of the last sub string in the specified string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        /// <param key="stv">The string to search.</param>
        /// <param key="startIndex">The start index to consider.</param>
        /// <returns>The formated string.</returns>
        public static int IndexOfLastString(this string st, string stv, int startIndex)
        {
            int index = startIndex;
            bool b = false;
            while (index >= 0 && !b)
            {
                if (st.Substring(index, 1) == "\"" && stv != "\"")
                {
                    index = st.IndexOfLastString("\"", index - 1) - 1;
                }
                if (st.Substring(index, 1) == "'" && stv != "'")
                {
                    index = st.IndexOfLastString("'", index - 1) - 1;
                }
                else if (st.Substring(index, 1) == ")" && stv == "(")
                {
                    index = st.IndexOfLastString("(", index - 1) - 1;
                }
                else if (st.Substring(index, 1) == "}" && stv == "{")
                {
                    index = st.IndexOfLastString("{", index - 1) - 1;
                }
                else if (st.Substring(index, 1) == stv)
                {
                    b = true;
                }
                else
                {
                    index--;
                }
            }

            return index;
        }

        /// <summary>
        /// Gets the index of the next sub string in the specified string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        /// <param key="stv">The string to search.</param>
        /// <param key="startIndex">The start index to consider.</param>
        /// <returns>The formated string.</returns>
        public static void IndexOfNextString(this string st, string stv, ref int startIndex)
        {
            startIndex = st.IndexOfNextString(stv, startIndex);
        }

        /// <summary>
        /// Gets the index of the next sub string in the specified string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        /// <param key="stv">The string to search.</param>
        /// <param key="startIndex">The start index to consider.</param>
        /// <param key="stringComparison">The string comparison to consider.</param>
        /// <returns>The formated string.</returns>
        public static int IndexOfNextString(
            this string st,
            string stv,
            int startIndex = 0,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase,
            char quote = '\0')
        {
            if (string.IsNullOrEmpty(st) || string.IsNullOrEmpty(stv))
            {
                return -1;
            }

            int index = startIndex;
            bool b = false;
            int st_l = st.Length;
            int stv_l = stv.Length;
            while (index < st_l && index > -1 && !b)
            {
                if (st[index] == quote && (stv[0] != quote && st[index] == quote))
                {
                    if (index < stv_l - 1 && st[index..(index + 2)] == string.Concat(quote, quote))
                    {
                        index += 2;
                    }
                    else
                    {
                        index = st.IndexOfNextString(quote.ToString(), index + 1);
                        index = index == -1 ? -1 : (index + 1);
                    }
                }
                else if (st[index] == '(' && stv == ")")
                {
                    index = st.IndexOfNextString(")", index + 1, quote: quote);
                    index = index == -1 ? -1 : (index + 1);
                }
                else if (stv == "}}" && index < stv_l - 1 && st[index..(index + 2)] == "{{")
                {
                    index = st.IndexOfNextString("}}", index + 2, quote: quote);
                    index = index == -1 ? -1 : (index + 2);
                }
                else if (stv == "}" && st[index] == '{')
                {
                    index = st.IndexOfNextString("}", index + 1, quote: quote);
                    index = index == -1 ? -1 : (index + 1);
                }
                else if (index <= st_l - stv_l && string.Equals(st.Substring(index, stv_l), stv, stringComparison))
                {
                    b = true;
                }
                else
                {
                    index++;
                }
            }
            return index >= st_l ? -1 : index;
        }
    }
}
