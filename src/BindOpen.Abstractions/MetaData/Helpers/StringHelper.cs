using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace BindOpen.MetaData
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
        public static readonly string __UniqueToken = "|*|";

        /// <summary>
        /// The pattern empty value.
        /// </summary>
        public static readonly string __PatternEmptyValue = "{{*}}";

        /// <summary>
        /// The string that is returned when the instance is not found.
        /// </summary>
        public static readonly string __NoneString = "<!--NONE-->";

        /// <summary>
        /// The string that is returned when the instance is not found.
        /// </summary>
        public static readonly string __DateFormat = "s";

        /// <summary>
        /// The string that is returned when the instance is not found.
        /// </summary>
        public static readonly string __TimeFormat = "HH:mm:ss.fff";

        /// <summary>
        /// The string that presents all items.
        /// </summary>
        public const string __Star = "*";

        #endregion

        // --------------------------------------------------
        // METHODS
        // --------------------------------------------------

        #region Methods

        /// <summary>
        /// Generates a password.
        /// </summary>
        /// <param name="charNumber">The character number to consider.</param>
        /// <returns>Returns the generated password.</returns>
        public static string GeneratePassword(int charNumber)
        {
            return StringHelper.NewGuid()
                .Replace("-", string.Empty).Replace("l", string.Empty).Replace("1", string.Empty).Replace("o", string.Empty).Replace("0", string.Empty)
                [..charNumber];
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
        public static string ToShortString(this string st, int charNumber, string addedString = "...")
        {
            string shortString = string.Empty;
            if (st != null)
                shortString = (st.Length > charNumber ? st.ToSubstring(0, charNumber - 4) + addedString : st);
            return shortString;
        }

        /// <summary>
        /// Hashes the string.
        /// </summary>
        /// <param name="st">The string to hash.</param>
        /// <param name="hashName">The name of the algorithm to consider.</param>
        /// <returns></returns>
        public static string HashString(this string st, string hashName)
        {
            HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashName);
            if (hashAlgorithm == null)
            {
                throw new ArgumentException("Unrecognized hash name", nameof(hashName));
            }

            byte[] bytes = hashAlgorithm.ComputeHash(st.ToByteArray(Encoding.UTF8));

            return Convert.ToBase64String(bytes);
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
            if ((st?.Length == 0) || (startIndex >= st.Length) || (endIndex < startIndex)) return string.Empty;

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
                (startingString != null && st.StartsWith(startingString) ? st : startingString + st);
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
                (endingString != null && st.EndsWith(endingString) ? st : st + endingString);
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
            while ((index >= 0) && !b)
            {
                if ((st.Substring(index, 1) == "\"") && (stv != "\""))
                {
                    index = st.IndexOfLastString("\"", index - 1) - 1;
                }
                if ((st.Substring(index, 1) == "'") && (stv != "'"))
                {
                    index = st.IndexOfLastString("'", index - 1) - 1;
                }
                else if ((st.Substring(index, 1) == ")") && (stv == "("))
                {
                    index = st.IndexOfLastString("(", index - 1) - 1;
                }
                else if ((st.Substring(index, 1) == "}") && (stv == "{"))
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
        /// Generates a new short ID.
        /// </summary>
        /// <returns>Returns a new short ID.</returns>
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString().ToLower();
        }

        /// <summary>
        /// Generates a new short ID.
        /// </summary>
        /// <returns>Returns a new short ID.</returns>
        public static string NewShortId()
        {
            return Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
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
        /// Sanitizes the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <returns>The formated string.</returns>
        public static string Sanitize(this string st)
        {
            return st?.Replace((char)13, '\0')
                .Replace((char)10, '\0');
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
            while ((index < st_l) && index > -1 && !b)
            {
                if ((st.Substring(index, 1) == "\"") && (stv != "\""))
                {
                    index = st.IndexOfNextString("\"", index + 1) + 1;
                }
                else if ((st.Substring(index, 1) == "'") && (stv == "'"))
                {
                    index = st.IndexOfNextString("'", index + 1) + 1;
                }
                else if ((st.Substring(index, 1) == "(") && (stv == ")"))
                {
                    index = st.IndexOfNextString(")", index + 1) + 1;
                }
                else if ((st.ToSubstring(index, index + 1) == "{{") && (stv == "}}"))
                {
                    index = st.IndexOfNextString("}}", index + 1) + 2;
                }
                else if ((st.Substring(index, 1) == "{") && (stv == "}"))
                {
                    index = st.IndexOfNextString("}", index + 1) + 1;
                }
                else if ((index <= st_l - stv_l) && (string.Equals(st.Substring(index, stv_l), stv, stringComparison)))
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

        /// <summary>
        /// Gets the date string of this instance.
        /// </summary>
        /// <param name="date">The date to consider.</param>
        /// <returns>Returns the date string of this instance.</returns>
        public static string ToString(this DateTime? date)
        {
            return date?.ToString(StringHelper.__DateFormat);
        }

        /// <summary>
        /// Gets the date string of this instance.
        /// </summary>
        /// <param name="date">The date to consider.</param>
        /// <returns>Returns the date string of this instance.</returns>
        public static string ToString(this DateTime date)
        {
            return date.ToString(StringHelper.__DateFormat);
        }

        /// <summary>
        /// Gets the normalized string from the specified string.
        /// </summary>
        /// <param name="st">The string to normalize.</param>
        /// <returns>Returns the normalized string.</returns>
        /// <remarks>The normalized string is a string in which only the alphanumeric characters and _ are allowed.</remarks>
        public static string ToNormalizedName(this string st)
        {
            return Regex.Replace(st, "[^0-9a-zA-Z_]", "_");
        }

        /// <summary>
        /// Gets the titled string from the specified string.
        /// </summary>
        /// <param name="st">The string to normalize.</param>
        /// <returns>Returns the normalized string.</returns>
        /// <remarks>The normalized string is a string in which only the alphanumeric characters and _ are allowed.</remarks>
        public static string ToTitleCasedName(string st)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(st);
        }

        /// <summary>
        /// Gets the string at the specified index from the specified index.
        /// </summary>
        /// <param name="strings">The objects to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static string GetAtIndex(this IList<string> strings, int index)
        {
            return strings != null && strings.Count > index && strings[index] != null ? strings[index] : string.Empty;
        }

        /// <summary>
        /// Gets the string at the specified index from the specified index.
        /// </summary>
        /// <param name="strings">The objects to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static string GetAtIndex(this string[] strings, int index)
        {
            return strings != null && strings.Length > index && strings[index] != null ? strings[index] : string.Empty;
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
            st1 ??= string.Empty;
            st2 ??= string.Empty;
            if (charString == null || st2.StartsWith(charString))
            {
                return st1 + st2[1..];
            }
            else
            {
                return st2;
            }
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
                List<string> stringItems1 = new List<string>(stringItems).Select(p => p.ToBdoKey()).ToList();
                stringItems1.RemoveAll(p => excludingStringItems.Contains(p.ToBdoKey()));
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

        // string conversions --------------------------

        /// <summary>
        /// Gets the object from the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="textFormat">The text format to consider.</param>
        /// <returns>Returns the object corresponding to the specified string.</returns>
        public static object ToObject(this string st, DataValueTypes valueType = DataValueTypes.Any, string textFormat = null)
        {
            if (st == null)
            {
                return null;
            }

            switch (valueType)
            {
                case DataValueTypes.Any:
                    object object1 = null;

                    if (st != null)
                    {
                        object1 = st.ToObject(DataValueTypes.Integer);
                        if (object1 == null)
                        {
                            object1 = st.ToObject(DataValueTypes.Long);
                            if (object1 == null)
                            {
                                object1 = st.ToObject(DataValueTypes.ULong);
                                if (object1 == null)
                                {
                                    object1 = st.ToObject(DataValueTypes.Number);
                                    if (object1 == null)
                                    {
                                        object1 = st.ToObject(DataValueTypes.Date);
                                        if (object1 == null)
                                        {
                                            object1 = st.ToObject(DataValueTypes.Time);
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
                case DataValueTypes.Date:
                    if (string.IsNullOrEmpty(textFormat)) textFormat = StringHelper.__DateFormat;
                    DateTime dateTime;
                    if (!DateTime.TryParseExact(st, textFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                        return null;
                    return new DateTime?(dateTime);
                case DataValueTypes.Time:
                    if (string.IsNullOrEmpty(textFormat)) textFormat = StringHelper.__TimeFormat;
                    TimeSpan aTimeSpan;
                    if (!TimeSpan.TryParseExact(st, textFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out aTimeSpan))
                        return null;
                    return new TimeSpan?(aTimeSpan);
                case DataValueTypes.Boolean:
                    return st.Equals("true", StringComparison.OrdinalIgnoreCase);
                case DataValueTypes.Number:
                    double aDouble;
                    if (!double.TryParse(st, NumberStyles.Any, new NumberFormatInfo() { NumberDecimalSeparator = "." }, out aDouble))
                        return null;
                    return new double?(aDouble);
                case DataValueTypes.Integer:
                    int aInt;
                    if (!int.TryParse(st, out aInt))
                        return null;
                    return new int?(aInt);
                case DataValueTypes.Long:
                    long aLong;
                    if (!long.TryParse(st, out aLong))
                        return null;
                    return new long?(aLong);
                case DataValueTypes.ULong:
                    ulong aULong;
                    if (!ulong.TryParse(st, out aULong))
                        return null;
                    return new ulong?(aULong);
                case DataValueTypes.ByteArray:
                    byte[] aByteArray = Convert.FromBase64String(st);
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
            return st.ToObject(DataValueTypes.Date, textFormat) as DateTime?;
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

            string st = string.Empty;
            Random rd = new();

            string currentMatch = pattern;
            int currentCharListIndex = 0;
            int currentLength = 8;
            string currentCharList;

            if (currentCharListIndex < 0 && currentCharListIndex < charLists.Length)
            {
                st += currentMatch;
            }
            else
            {
                currentCharList = charLists[currentCharListIndex];
                for (int i = 0; i < currentLength; i++)
                    st += currentCharList[rd.Next(0, currentCharList.Length)];
            }

            return st;
        }

        #endregion
    }
}
