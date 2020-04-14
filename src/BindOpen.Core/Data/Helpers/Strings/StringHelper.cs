using BindOpen.Data.Common;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Items;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace BindOpen.Data.Helpers.Strings
{
    /// <summary>
    /// This structure represents a string helper.
    /// </summary>
    public static class StringHelper
    {
        // ------------------------------------------
        // CONSTANTS
        // ------------------------------------------

        #region Constants

        /// <summary>
        /// The unique token.
        /// </summary>
        public static string __UniqueToken = "|*|";

        /// <summary>
        /// The pattern empty value.
        /// </summary>
        public static string __PatternEmptyValue = "{{*}}";

        /// <summary>
        /// The string that is returned when the instance is not found.
        /// </summary>
        public static string __NoneString = "<!--NONE-->";

        /// <summary>
        /// The string that is returned when the instance is not found.
        /// </summary>
        public static string __DateFormat = "s";

        /// <summary>
        /// The string that is returned when the instance is not found.
        /// </summary>
        public static string __TimeFormat = "HH:mm:ss.fff";

        #endregion

        // --------------------------------------------------
        // METHODS
        // --------------------------------------------------

        #region Methods

        /// <summary>
        /// Replaces the specified text by the specified one in the specified text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <param name="textToFind">The text to find.</param>
        /// <param name="textToReplace">The text to replace.</param>
        /// <param name="isCaseMatched">Indicates whether the case must be considered.</param>
        /// <param name="isReplacedOnce">Indicates whether the text to find is to be replaced once.</param>
        /// <returns></returns>
        public static string Replace(this string text, string textToFind, string textToReplace, bool isCaseMatched, bool isReplacedOnce = false)
        {
            StringComparison sc = isCaseMatched ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            int pos;
            while ((pos = text.IndexOf(textToFind, sc)) > -1)
            {
                text = text.Remove(pos, textToFind.Length);
                text = text.Insert(pos, textToReplace);

                if (isReplacedOnce) break;
            }

            return text;
        }

        /// <summary>
        /// Generates a password.
        /// </summary>
        /// <param name="charNumber">The character number to consider.</param>
        /// <returns>Returns the generated password.</returns>
        public static string GeneratePassword(int charNumber)
        {
            return Guid.NewGuid().ToString().ToLower()
                .Replace("-", "").Replace("l", "").Replace("1", "").Replace("o", "").Replace("0", "")
                .Substring(0, charNumber);
        }

        /// <summary>
        /// Gets the number of occurences of a specfied character in the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="character">The character to consider.</param>
        /// <returns>The number of occurences of a specfied character in the specified string.</returns>
        public static int CountOccurences(this string st, char character)
        {
            int count = 0;
            int index = 0;
            if (!string.IsNullOrEmpty(st))
            {
                if (st[0] == character) count++;
                while (
                    (index >= 0
                    & index < st.Length)
                    && (index = st.IndexOf(character, index + 1)) > 0)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Gets the number of occurences of a specfied character in the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="subString">The sub string to consider.</param>
        /// <returns>The number of occurences of a specfied character in the specified string.</returns>
        public static int CountOccurences(this string st, string subString)
        {
            int count = 0;
            int i = 0;
            while ((i = st.IndexOf(subString, i)) != -1)
            {
                i += subString.Length;
                count++;
            }
            return count;
        }

        /// <summary>
        /// Returns the string value of the specified settings.
        /// </summary>
        /// <param name="stringValue">string value to consider.</param>
        /// <param name="limitSize">Limit string size to consider.</param>
        /// <returns>The string value of the specified settings.</returns>
        public static bool CheckNameFormat(this string stringValue, int limitSize)
        {
            bool aBool = true;

            // Valid special characters (which are configurable) are ?_!.*
            string aRegEx = "^[a-zA-Z0-9_]{0," + limitSize.ToString() + "}$";
            try
            {
                if (!Regex.IsMatch(stringValue, aRegEx))
                    aBool = false;
            }
            catch
            {
            }

            return aBool;
        }

        /// <summary>
        /// Gets the string shorten to the specified characters.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="charNumber">The number of characters to consider.</param>
        /// <param name="addedString">Indicates whether dots are added.</param>
        /// <returns>Returns the specified string shorten.</returns>
        public static string GetShortString(this string st, int charNumber, string addedString = "...")
        {
            string shortString = "";
            if (st != null)
                shortString = (st.Length > charNumber ? st.GetSubstring(0, charNumber - 4) + addedString : st);
            return shortString;
        }

        /// <summary>
        /// Gets the sub string contained between the specified characters in the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="character">The character to consider.</param>
        /// <returns>Returns the sub string.</returns>
        public static string GetStringBetween(this string st, Char character)
        {
            string subString = "";
            int index1 = st.IndexOf(character);
            int index2 = (index1 == -1 ? -1 : st.IndexOf(character, index1 + 1));

            if ((index1 > -1) & (index2 > -1))
                subString = st.Substring(index1 + 1, index2 - index1 - 1);

            return subString;
        }

        /// <summary>
        /// Hashes the string.
        /// </summary>
        /// <param name="st">The string to hash.</param>
        /// <param name="hashName">The name of the algorithm to consider.</param>
        /// <returns></returns>
        public static string HashString(this string st, string hashName)
        {
            HashAlgorithm aHashAlgorithm = HashAlgorithm.Create(hashName);
            if (aHashAlgorithm == null)
                throw new ArgumentException("Unrecognized hash name", "hashName");

            byte[] bytes = aHashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(st));

            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Converts the specified path according to the environment.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <returns></returns>
        public static string ToPath(this string st)
        {
            return st?.Replace('\\', Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// Converts the specified path according to the environment.
        /// </summary>
        /// <param name="path">The path to consider.</param>
        /// <param name="rootPath">The root path to consider.</param>
        /// <returns></returns>
        public static string GetConcatenatedPath(this string path, string rootPath)
        {
            if (path == null) return null;

            if ((path?.StartsWith(@".\") == true) || (path?.StartsWith(@"./") == true))
            {
                path = (rootPath.GetEndedString(@"\") + path.Substring(2)).ToPath();
            }
            else if ((path?.StartsWith(@"..\") == true) || (path?.StartsWith(@"../") == true))
            {
                path = (rootPath.GetEndedString(@"\") + path).ToPath();
            }

            return path?.Replace('\\', Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// Gets the specified sub string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="startIndex">The start index to consider.</param>
        /// <param name="endIndex">The end index to consider.</param>
        /// <returns>The formated path.</returns>
        public static string GetSubstring(this string st, int startIndex, int endIndex = -1)
        {
            if (st == null) return null;

            if (startIndex < 0) startIndex = 0;
            if (endIndex == -1 || endIndex >= st.Length) endIndex = st.Length - 1;
            if ((st?.Length == 0) || (startIndex >= st.Length) || (endIndex < startIndex)) return "";

            return st.Substring(startIndex, endIndex - startIndex + 1);
        }

        /// <summary>
        /// Gets the minimum index from the specified ones.
        /// </summary>
        /// <param name="indexes">The indexes to consider.</param>
        /// <returns>Returns the minimum index.</returns>
        public static int GetMinimumIndex(params int[] indexes)
        {
            int i = int.MaxValue;
            foreach (int index in indexes)
                if ((index > -1) && (index < i)) i = index;
            return i;
        }

        /// <summary>
        /// Gets the string with the specified starting string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="startingString">The starting string to consider.</param>
        /// <param name="containingString">The string con</param>
        /// <returns>The formated path.</returns>
        public static string GetStartedString(this string st, string startingString, string containingString = null)
        {
            return string.IsNullOrEmpty(st) ? "" :
                ((containingString == null || st.Contains(containingString))
                && (startingString != null && st.StartsWith(startingString)) ?
                st : startingString + st);
        }

        /// <summary>
        /// Gets the string with the specified ending string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="endingString">The ending string to consider.</param>
        /// <returns>The formated path.</returns>
        public static string GetEndedString(this string st, string endingString)
        {
            return (string.IsNullOrEmpty(st) ? "" : (st.EndsWith(endingString) ? st : st + endingString));
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
        public static string GetFormatString(this string st, int index, string replaceString, string wholeReplaceString = null)
        {
            string indexString = "{" + index.ToString() + "}";
            int indexStringIndex = st.IndexOf(indexString);
            if (indexStringIndex > -1)
            {
                string stringToReplace = indexString;
                string newString = "";

                int startIndex = StringHelper.GetIndexOfLastString(st, "{", indexStringIndex - 1);
                if (startIndex > -1)
                {
                    int aEndIndex = StringHelper.GetIndexOfNextString(st, "}", indexStringIndex + indexString.Length);
                    if (aEndIndex > -1)
                        stringToReplace = st.Substring(startIndex, aEndIndex - startIndex + 1);

                    newString = wholeReplaceString ?? stringToReplace.Substring(1, stringToReplace.Length - 2).Replace(indexString, replaceString)
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
        /// Gets the index of the last sub string in the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="stv">The string to search.</param>
        /// <param name="startIndex">The start index to consider.</param>
        /// <returns>The formated string.</returns>
        public static void GetIndexOfLastString(this string st, string stv, ref int startIndex)
        {
            startIndex = StringHelper.GetIndexOfLastString(st, stv, startIndex);
        }

        /// <summary>
        /// Gets the index of the last sub string in the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="stv">The string to search.</param>
        /// <param name="startIndex">The start index to consider.</param>
        /// <returns>The formated string.</returns>
        public static int GetIndexOfLastString(this string st, string stv, int startIndex)
        {
            int index = startIndex;
            bool b = false;
            while ((index >= 0) && !b)
            {
                if ((st.Substring(index, 1) == "\"") && (stv != "\""))
                    index = GetIndexOfLastString(st, "\"", index - 1) - 1;
                else if ((st.Substring(index, 1) == ")") && (stv == "("))
                    index = GetIndexOfLastString(st, "(", index - 1) - 1;
                else if ((st.Substring(index, 1) == "}") && (stv == "{"))
                    index = GetIndexOfLastString(st, "{", index - 1) - 1;
                else if (st.Substring(index, 1) == stv)
                    b = true;
                else
                    index--;
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
        public static void GetIndexOfNextString(this string st, string stv, ref int startIndex)
        {
            startIndex = StringHelper.GetIndexOfNextString(st, stv, startIndex);
        }

        /// <summary>
        /// Gets the index of the next sub string in the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="stv">The string to search.</param>
        /// <param name="startIndex">The start index to consider.</param>
        /// <param name="stringComparison">The string comparison to consider.</param>
        /// <returns>The formated string.</returns>
        public static int GetIndexOfNextString(this string st, string stv, int startIndex = 0, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            if (st == null || stv == null)
                return -1;

            int index = startIndex;
            bool b = false;
            int st_l = st.Length;
            int stv_l = stv.Length;
            while ((index < st_l) && index > -1 && !b)
            {
                if ((st.Substring(index, 1) == "\"") && (stv != "\""))
                    index = GetIndexOfNextString(st, "\"", index + 1) + 1;
                else if ((st.Substring(index, 1) == "'") && (stv == "'"))
                    index = GetIndexOfNextString(st, "'", index + 1) + 1;
                else if ((st.Substring(index, 1) == "(") && (stv == ")"))
                    index = GetIndexOfNextString(st, ")", index + 1) + 1;
                else if ((st.GetSubstring(index, index + 1) == "{{") && (stv == "}}"))
                    index = GetIndexOfNextString(st, "}}", index + 1) + 2;
                else if ((st.Substring(index, 1) == "{") && (stv == "}"))
                    index = GetIndexOfNextString(st, "}", index + 1) + 1;
                else if ((index <= st_l - stv_l) && (string.Equals(st.Substring(index, stv_l), stv, stringComparison)))
                    b = true;
                else
                    index++;
            }
            return index;
        }

        /// <summary>
        /// Gets the date string of this instance.
        /// </summary>
        /// <param name="date">The date to consider.</param>
        /// <returns>Returns the date string of this instance.</returns>
        public static string GetString(this DateTime? date)
        {
            return date?.ToString(StringHelper.__DateFormat);
        }

        /// <summary>
        /// Gets the date string of this instance.
        /// </summary>
        /// <param name="date">The date to consider.</param>
        /// <returns>Returns the date string of this instance.</returns>
        public static string GetString(this DateTime date)
        {
            return date.ToString(StringHelper.__DateFormat);
        }

        /// <summary>
        /// Gets the normalized string from the specified string.
        /// </summary>
        /// <param name="st">The string to normalize.</param>
        /// <returns>Returns the normalized string.</returns>
        /// <remarks>The normalized string is a string in which only the alphanumeric characters and _ are allowed.</remarks>
        public static string GetNormalizedName(this string st)
        {
            return Regex.Replace(st, "[^0-9a-zA-Z_]", "_");
        }

        /// <summary>
        /// Gets the titled string from the specified string.
        /// </summary>
        /// <param name="st">The string to normalize.</param>
        /// <returns>Returns the normalized string.</returns>
        /// <remarks>The normalized string is a string in which only the alphanumeric characters and _ are allowed.</remarks>
        public static string GetTitleCasedName(string st)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(st);
        }

        /// <summary>
        /// Gets the string at the specified index from the specified index.
        /// </summary>
        /// <param name="strings">The objects to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static string GetStringAtIndex(this List<string> strings, int index)
        {
            return strings != null && strings.Count > index && strings[index] != null ? strings[index] : "";
        }

        /// <summary>
        /// Concatenates the two specified string only if the second one starts with the specified character. Returns the second string otherwise.
        /// </summary>
        /// <param name="st1">The first string to concatenate.</param>
        /// <param name="st2">The second string to concatenate.</param>
        /// <param name="charString">The string value to consider.</param>
        /// <returns>Returns the concatenated string.</returns>
        /// <remarks>If the leading char is null then the two strings are always concatenated.</remarks>
        public static string Concatenate(this string st1, string st2, string charString = null)
        {
            st1 = (st1 ?? "");
            st2 = (st2 ?? "");
            if ((charString == null) || (st2.StartsWith(charString)))
                return st1 + st2.Substring(1);
            else
                return st2;
        }

        /// <summary>
        /// Sets the first string as the second one if the specified condition is statisfied.
        /// </summary>
        /// <param name="st1">The first string to concatenate.</param>
        /// <param name="condition">The condition to consider.</param>
        /// <param name="st2">The second string to concatenate.</param>
        /// <returns>Returns the concatenated string.</returns>
        public static string If(this string st1, bool condition, string st2)
        {
            if (condition)
            {
                return st2;
            }

            return st1;
        }

        /// <summary>
        /// Concatenates the first string with the second one if the specified condition is statisfied.
        /// </summary>
        /// <param name="st1">The first string to concatenate.</param>
        /// <param name="condition">The condition to consider.</param>
        /// <param name="st2">The second string to concatenate.</param>
        /// <returns>Returns the concatenated string.</returns>
        public static string ConcatenateIf(this string st1, bool condition, string st2)
        {
            if (condition)
            {
                return st1 + st2;
            }

            return st1;
        }

        /// <summary>
        /// Concatenates the first string with the second only if the first one is not empty.
        /// </summary>
        /// <param name="st1">The first string to concatenate.</param>
        /// <param name="st2">The second string to concatenate.</param>
        /// <returns>Returns the concatenated string.</returns>
        public static string ConcatenateIfFirstNotEmpty(this string st1, string st2)
        {
            if (!string.IsNullOrEmpty(st1))
            {
                return st1 + st2;
            }

            return st1;
        }

        /// <summary>
        /// Concatenates the first string with the second only if the second one is not empty.
        /// </summary>
        /// <param name="st1">The first string to concatenate.</param>
        /// <param name="st2">The second string to concatenate.</param>
        /// <returns>Returns the concatenated string.</returns>
        public static string ConcatenateIfSecondNotEmpty(this string st1, string st2)
        {
            if (!string.IsNullOrEmpty(st2))
            {
                return st1 + st2;
            }

            return st1;
        }

        /// <summary>
        /// Excludes the specified string items from the specified string items.
        /// </summary>
        /// <param name="stringItems">The string items to consider.</param>
        /// <param name="excludingStringItems">The string items to exclude.</param>
        /// <returns>Returns the excluded string items.</returns>
        public static IEnumerable<string> Excluding(this IEnumerable<string> stringItems, params string[] excludingStringItems)
        {
            return StringHelper.Excluding(stringItems, excludingStringItems.ToList());
        }

        /// <summary>
        /// Excludes the specified string items from the specified string items.
        /// </summary>
        /// <param name="stringItems">The string items to consider.</param>
        /// <param name="excludingStringItems">The string items to exclude.</param>
        /// <returns>Returns the excluded string items.</returns>
        public static IEnumerable<string> Excluding(this IEnumerable<string> stringItems, IEnumerable<string> excludingStringItems)
        {
            if (stringItems == null)
            {
                return new List<string>();
            }
            else if (excludingStringItems == null)
            {
                return stringItems;
            }
            else
            {
                List<string> stringItems1 = new List<string>(stringItems).Select(p => p.ToKey()).ToList();
                stringItems1.RemoveAll(p => excludingStringItems.Contains(p.ToKey()));
                return stringItems1;
            }
        }

        /// <summary>
        /// Adds the specified string items from the specified string items.
        /// </summary>
        /// <param name="stringItems">The string items to consider.</param>
        /// <param name="addingStringItems">The string items to add.</param>
        /// <returns>Returns the added string items.</returns>
        public static IEnumerable<string> Adding(this IEnumerable<string> stringItems, params string[] addingStringItems)
        {
            return StringHelper.Adding(stringItems, addingStringItems.ToList());
        }

        /// <summary>
        /// Adds the specified string items from the specified string items.
        /// </summary>
        /// <param name="stringItems">The string items to consider.</param>
        /// <param name="addingStringItems">The string items to add.</param>
        /// <returns>Returns the added string items.</returns>
        public static IEnumerable<string> Adding(this IEnumerable<string> stringItems, IEnumerable<string> addingStringItems)
        {
            if (stringItems == null)
            {
                return new List<string>();
            }
            else if (addingStringItems == null)
                return stringItems;
            else
            {
                new List<string>(stringItems).AddRange(addingStringItems);
                return stringItems;
            }
        }

        /// <summary>
        /// Get the key values from the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <returns>Returns the added string items.</returns>
        public static List<DataKeyValue> GetKeyValues(this string st)
        {
            List<DataKeyValue> dataKeyValues = new List<DataKeyValue>();
            foreach (string subString in st.Split('|'))
            {
                if (subString.Contains("="))
                {
                    int i = subString.IndexOf("=");
                    dataKeyValues.Add(
                        new DataKeyValue(subString.Substring(0, i).Trim(), subString.Substring(i + 1).Trim()));
                }
            }

            return dataKeyValues;
        }

        /// <summary>
        /// Gets the quoted string.
        /// </summary>
        /// <param name="st">The string to normalize.</param>
        /// <returns>Returns the quoted string.</returns>
        public static string GetQuotedString(this string st)
        {
            return "'" + (st ?? "").Replace("''", "''") + "'";
        }

        /// <summary>
        /// Gets the unquoted string.
        /// </summary>
        /// <param name="st">The string to normalize.</param>
        /// <param name="quoteChar">The quote character to consider.</param>
        /// <returns>Returns the quoted string.</returns>
        public static string GetUnquotedString(this string st, char quoteChar = '\'')
        {
            if (st == null) return null;

            var quoteString = quoteChar.ToString();

            if (st.Length > 2 && st.StartsWith(quoteString) && st.EndsWith(quoteString))
                st = st.Substring(1, st.Length - 2);
            return st;
        }

        // string conversions --------------------------

        /// <summary>
        /// Gets the object from the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <returns>Returns the object corresponding to the specified string.</returns>
        public static object GuessObjectValueFromString(this string st)
        {
            object object1 = null;

            if (st != null)
            {
                object1 = st.ToObject(DataValueType.Integer);
                if (object1 == null)
                {
                    object1 = st.ToObject(DataValueType.Long);
                    if (object1 == null)
                    {
                        object1 = st.ToObject(DataValueType.ULong);
                        if (object1 == null)
                        {
                            object1 = st.ToObject(DataValueType.Number);
                            if (object1 == null)
                            {
                                object1 = st.ToObject(DataValueType.Date);
                                if (object1 == null)
                                {
                                    object1 = st.ToObject(DataValueType.Time);
                                    if (object1 == null)
                                    {
                                        object1 = st;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return object1;
        }

        /// <summary>
        /// Gets the object from the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="textFormat">The text format to consider.</param>
        /// <returns>Returns the object corresponding to the specified string.</returns>
        public static object ToObject(this string st, DataValueType valueType = DataValueType.Any, string textFormat = null)
        {
            if (valueType == DataValueType.Any)
            {
                return st.GuessObjectValueFromString();
            }

            switch (valueType)
            {
                case DataValueType.Date:
                    if (string.IsNullOrEmpty(textFormat)) textFormat = StringHelper.__DateFormat;
                    DateTime dateTime;
                    if (!DateTime.TryParseExact(st, textFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                        return null;
                    return new DateTime?(dateTime);
                case DataValueType.Time:
                    if (string.IsNullOrEmpty(textFormat)) textFormat = StringHelper.__TimeFormat;
                    TimeSpan aTimeSpan;
                    if (!TimeSpan.TryParseExact(st, textFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out aTimeSpan))
                        return null;
                    return new TimeSpan?(aTimeSpan);
                case DataValueType.Boolean:
                    return st.Equals("$TRUE()", StringComparison.OrdinalIgnoreCase) || st.Equals("TRUE", StringComparison.OrdinalIgnoreCase);
                case DataValueType.Number:
                    double aDouble;
                    if (!double.TryParse(st, NumberStyles.Any, new NumberFormatInfo() { NumberDecimalSeparator = "." }, out aDouble))
                        return null;
                    return new double?(aDouble);
                case DataValueType.Integer:
                    int aInt;
                    if (!int.TryParse(st, out aInt))
                        return null;
                    return new int?(aInt);
                case DataValueType.Long:
                    long aLong;
                    if (!long.TryParse(st, out aLong))
                        return null;
                    return new long?(aLong);
                case DataValueType.ULong:
                    ulong aULong;
                    if (!ulong.TryParse(st, out aULong))
                        return null;
                    return new ulong?(aULong);
                case DataValueType.ByteArray:
                    byte[] aByteArray = Encoding.Default.GetBytes(st);
                    return aByteArray;
                default:
                    return st;
            }
        }

        /// <summary>
        /// Gets the date time object from the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="textFormat">The text format to consider.</param>
        /// <returns>Returns the object corresponding to the specified string.</returns>
        public static DateTime? ToDateTime(this string st, string textFormat = null)
        {
            return st.ToObject(DataValueType.Date, textFormat) as DateTime?;
        }

        /// <summary>
        /// Gets the enumration from the specified string.
        /// </summary>
        /// <typeparam name="T">The structure to consider.</typeparam>
        /// <param name="st">The string to consider.</param>
        /// <param name="defaultEnum">The default enumeration to consider.</param>
        /// <returns>Returns the object corresponding to the specified string.</returns>
        public static T ToEnum<T>(this string st, T defaultEnum = default) where T : struct, IConvertible
        {
            T aEnum = defaultEnum;
            if ((st != null) && (!Enum.TryParse<T>(st, true, out aEnum)))
                aEnum = defaultEnum;
            return aEnum;
        }

        /// <summary>
        /// Get a random string from the specified string with the specified length.
        /// </summary>
        /// <param name="pattern">The pattern to consider.</param>
        /// <param name="charLists">The lists of chars to consider.</param>
        /// <returns>A random password with the specified length.</returns>
        public static string GetRandomString(
            string pattern = "{{char1,8}}",
            params string[] charLists)
        {
            if (charLists == null)
            {
                charLists = new String[1]
                   { "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-" };
            }

            string st = "";
            Random rd = new Random();

            string currentMatch = pattern;
            int currentCharListIndex = 0;
            int currentLength = 8;
            string currentCharList;
            if (currentCharListIndex < 0 && currentCharListIndex < charLists.Length)
                st += currentMatch;
            else
            {
                currentCharList = charLists[currentCharListIndex];
                for (int i = 0; i < currentLength; i++)
                    st += currentCharList[rd.Next(0, currentCharList.Length)];
            }

            return st;
        }

        /// <summary>
        /// Returns the current time stamp.
        /// </summary>
        /// <returns>The current time stamp</returns>
        public static string GetCurrentTimeStamp()
        {
            return DateTime.Now.Year +
                (DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) +
                (DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString()) +
                (DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString()) +
                (DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString()) +
                (DateTime.Now.Second < 10 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString());
        }

        #endregion
    }
}
