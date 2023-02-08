using System;

namespace BindOpen.Data.Helpers
{
    /// <summary>
    /// This class represents a helper for objects.
    /// </summary>
    public static partial class StringHelper
    {

        /// <summary>
        /// Gets the quoted string.
        /// </summary>
        /// <param name="st">The string to normalize.</param>
        /// <returns>Returns the quoted string.</returns>
        public static string ToQuoted(this string st)
        {
            return "'" + (st ?? string.Empty).Replace("'", "''") + "'";
        }

        /// <summary>
        /// Gets the unquoted string.
        /// </summary>
        /// <param name="st">The string to normalize.</param>
        /// <param name="quoteChar">The quote character to consider.</param>
        /// <returns>Returns the quoted string.</returns>
        public static string ToUnquoted(this string st, char quoteChar = '\'')
        {
            if (st == null) return null;

            if (st.Length > 2 && st.StartsWith(quoteChar) && st.EndsWith(quoteChar))
                st = st[1..^1];
            return st;
        }

        /// <summary>
        /// Formats the specified string replacing the specified index by the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <param name="replaceString">The replacement string to consider.</param>
        /// <param name="wholeReplaceString">The whole replacement string to consider.</param>
        /// <example>The string should be formated this way: {0} {1} or { .. {0} .. } { .. {1} .. } and so on.</example>
        /// <returns>The formated string.</returns>
        public static string ToFormatedString(this string st, int index, string replaceString, string wholeReplaceString = null)
        {
            string indexString = "{" + index.ToString() + "}";
            int indexStringIndex = st.IndexOf(indexString);
            if (indexStringIndex > -1)
            {
                string stringToReplace = indexString;
                int startIndex = st.IndexOfLastString("{", indexStringIndex - 1);
                string newString;
                if (startIndex > -1)
                {
                    int endIndex = st.IndexOfNextString("}", indexStringIndex + indexString.Length);
                    if (endIndex > -1)
                    {
                        stringToReplace = st.Substring(startIndex, endIndex - startIndex + 1);
                    }

                    newString = wholeReplaceString ?? stringToReplace[1..^1].Replace(indexString, replaceString)
;
                }
                else
                {
                    newString = replaceString;
                }

                st = st.Replace(stringToReplace, newString);
            }

            return st;
        }

        /// <summary>
        /// Gets the specified sub string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="startIndex">The start index to consider.</param>
        /// <param name="endIndex">The end index to consider.</param>
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
        /// <param name="st">The string to consider.</param>
        /// <param name="startingString">The starting string to consider.</param>
        /// <returns>The formated path.</returns>
        public static string StartingWith(this string st, string startingString)
        {
            return string.IsNullOrEmpty(st) ? string.Empty :
                startingString != null && st.StartsWith(startingString) ? st : startingString + st;
        }

        /// <summary>
        /// Gets the string with the specified ending string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="endingString">The ending string to consider.</param>
        /// <returns>The formated path.</returns>
        public static string EndingWith(this string st, string endingString)
        {
            return string.IsNullOrEmpty(st) ? string.Empty :
                endingString != null && st.EndsWith(endingString) ? st : st + endingString;
        }

        /// <summary>
        /// Gets the index of the last sub string in the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="stv">The string to search.</param>
        /// <param name="startIndex">The start index to consider.</param>
        /// <returns>The formated string.</returns>
        public static void IndexOfLastString(this string st, string stv, ref int startIndex)
        {
            startIndex = st.IndexOfLastString(stv, startIndex);
        }

        /// <summary>
        /// Gets the index of the last sub string in the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="stv">The string to search.</param>
        /// <param name="startIndex">The start index to consider.</param>
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
        /// <param name="st">The string to consider.</param>
        /// <param name="stv">The string to search.</param>
        /// <param name="startIndex">The start index to consider.</param>
        /// <returns>The formated string.</returns>
        public static void IndexOfNextString(this string st, string stv, ref int startIndex)
        {
            startIndex = st.IndexOfNextString(stv, startIndex);
        }

        /// <summary>
        /// Gets the index of the next sub string in the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="stv">The string to search.</param>
        /// <param name="startIndex">The start index to consider.</param>
        /// <param name="stringComparison">The string comparison to consider.</param>
        /// <returns>The formated string.</returns>
        public static int IndexOfNextString(this string st, string stv, int startIndex = 0, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            if (st == null || stv == null)
            {
                return -1;
            }

            int index = startIndex;
            bool b = false;
            int st_l = st.Length;
            int stv_l = stv.Length;
            while (index < st_l && index > -1 && !b)
            {
                if (st.Substring(index, 1) == "\"" && stv != "\"")
                {
                    index = st.IndexOfNextString("\"", index + 1) + 1;
                }
                else if (st.Substring(index, 1) == "'" && stv == "'")
                {
                    index = st.IndexOfNextString("'", index + 1) + 1;
                }
                else if (st.Substring(index, 1) == "(" && stv == ")")
                {
                    index = st.IndexOfNextString(")", index + 1) + 1;
                }
                else if (st.ToSubstring(index, index + 1) == "{{" && stv == "}}")
                {
                    index = st.IndexOfNextString("}}", index + 1) + 2;
                }
                else if (st.Substring(index, 1) == "{" && stv == "}")
                {
                    index = st.IndexOfNextString("}", index + 1) + 1;
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
            return index;
        }
    }
}
