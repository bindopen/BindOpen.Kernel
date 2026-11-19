using System;

namespace BindOpen.Data.Helpers;

/// <summary>
/// This class represents a helper for objects.
/// </summary>
public static partial class StringHelper
{
    /// <summary>
    /// Indicates whether the specified string is quoted.
    /// </summary>
    /// <param key="st">The string to normalize.</param>
    /// <returns>Returns the quoted string.</returns>
    public static bool IsQuoted(this string st, char quote = '\'')
    {
        return !string.IsNullOrEmpty(st) && st.StartsWith(quote.ToString()) && st.EndsWith(quote.ToString());
    }

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
        if (st.Length >= 2 && st.StartsWith(quote.ToString()) && st.EndsWith(quote.ToString()))
        {
            string quotes = string.Concat(quote, quote);
            var inner = st.Length > 2 ? st.Substring(1, st.Length - 2) : string.Empty;
            st = inner.Replace(quotes, quote.ToString());
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
        if (string.IsNullOrEmpty(st) || string.IsNullOrEmpty(stv)) return -1;

        int index = Math.Min(startIndex, st.Length - 1);

        while (index >= 0)
        {
            char c = st[index];

            // Skip double-quoted sections when searching for other tokens
            if (c == '"' && stv != "\"")
            {
                int prev = st.LastIndexOf('"', index - 1);
                if (prev < 0) return -1;
                index = prev - 1;
                continue;
            }

            // Skip single-quoted sections when searching for other tokens
            if (c == '\'' && stv != "'")
            {
                int prev = st.LastIndexOf('\'', index - 1);
                if (prev < 0) return -1;
                index = prev - 1;
                continue;
            }

            // Handle simple bracket/parenthesis backward search
            if (c == ')' && stv == "(")
            {
                int prev = st.LastIndexOf('(', index - 1);
                if (prev < 0) return -1;
                index = prev - 1;
                continue;
            }

            if (c == '}' && stv == "{")
            {
                int prev = st.LastIndexOf('{', index - 1);
                if (prev < 0) return -1;
                index = prev - 1;
                continue;
            }

            // Match single-char token
            if (stv.Length == 1)
            {
                if (c == stv[0]) return index;
            }
            else
            {
                int start = index - stv.Length + 1;
                if (start >= 0)
                {
                    if (string.Equals(st.Substring(start, stv.Length), stv, StringComparison.Ordinal))
                        return start;
                }
            }

            index--;
        }

        return -1;
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
        int index = Math.Max(0, startIndex);
        int st_l = st.Length;
        int stv_l = stv.Length;

        while (index < st_l)
        {
            char c = st[index];

            // Handle quoted sections
            if (quote != '\0' && c == quote && stv.Length > 0 && stv[0] != quote)
            {
                // handle doubled quotes ""
                if (index + 1 < st_l && st[index + 1] == quote)
                {
                    index += 2;
                    continue;
                }

                int next = st.IndexOf(quote, index + 1);
                if (next == -1) return -1;
                index = next + 1;
                continue;
            }

            // Parenthesis and braces handling
            if (c == '(' && stv == ")")
            {
                int next = st.IndexOf(')', index + 1);
                if (next == -1) return -1;
                index = next + 1;
                continue;
            }

            if (stv == "}}" && index + 1 < st_l && st[index] == '{' && st[index + 1] == '{')
            {
                int next = st.IndexOf("}}", index + 2, StringComparison.Ordinal);
                if (next == -1) return -1;
                index = next + 2;
                continue;
            }

            if (stv == "}" && c == '{')
            {
                int next = st.IndexOf('}', index + 1);
                if (next == -1) return -1;
                index = next + 1;
                continue;
            }

            // Check for match at current position
            if (index <= st_l - stv_l && string.Equals(st.Substring(index, stv_l), stv, stringComparison))
            {
                return index;
            }

            index++;
        }

        return -1;
    }
}
