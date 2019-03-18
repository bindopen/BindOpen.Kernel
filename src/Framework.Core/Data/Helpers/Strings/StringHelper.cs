using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items.Dictionary;

namespace BindOpen.Framework.Core.Data.Helpers.Strings
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
        /// The pattern empty value.
        /// </summary>
        public static String __PatternEmptyValue = @"{{*}}";

        /// <summary>
        /// The string that is returned when the instance is not found.
        /// </summary>
        public static String __NoneString = @"<!--NONE-->";

        /// <summary>
        /// The string that is returned when the instance is not found.
        /// </summary>
        public static String __DateFormat = "s";

        /// <summary>
        /// The string that is returned when the instance is not found.
        /// </summary>
        public static String __TimeFormat = "HH:mm:ss.fff";

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
        public static String GeneratePassword(int charNumber)
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
            if (!String.IsNullOrEmpty(st))
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
        /// <param name="stringValue">String value to consider.</param>
        /// <param name="limitSize">Limit string size to consider.</param>
        /// <returns>The string value of the specified settings.</returns>
        public static Boolean CheckNameFormat(this String stringValue, int limitSize)
        {
            Boolean aBoolean = true;

            // Valid special characters (which are configurable) are ?_!.*
            String aRegEx = @"^[a-zA-Z0-9_]{0," + limitSize.ToString() + "}$";
            try
            {
                if (!Regex.IsMatch(stringValue, aRegEx))
                    aBoolean = false;
            }
            catch
            {
            }

            return aBoolean;
        }

        ///// <summary>
        ///// Returns the index of the next specified char in the specified string from the specified index.
        ///// </summary>
        ///// <param name="st">The string to consider.</param>
        ///// <param name="aChar">The char to consider.</param>
        ///// <param name="indexDeb">The start index to consider.</param>
        ///// <returns>Reutrns the index of the next specified char.</returns>
        //public static int GetIndexOfNextString(String st, String aChar, int indexDeb)
        //{
        //    if (indexDeb > st.Length-1)
        //        return -1;

        //    int aSimpleCharIndex = st.IndexOf(aChar, indexDeb);
        //    if (aSimpleCharIndex == -1)
        //        return st.Length;
        //    else
        //        if ((aSimpleCharIndex < st.Length - 1) && (st.Substring(aSimpleCharIndex + 1, 1) == aChar))
        //            return StringHelper.GetIndexOfNextString(st, aChar, aSimpleCharIndex + 2);
        //        else
        //            return aSimpleCharIndex;
        //}

        /// <summary>
        /// Gets the string shorten to the specified characters.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="charNumber">The number of characters to consider.</param>
        /// <param name="dotAdded">Indicates whether dots are added.</param>
        /// <returns>Returns the specified string shorten.</returns>
        public static String GetShortString(this String st, int charNumber, String addedString = "...")
        {
            String shortString = "";
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
        public static String GetStringBetween(this String st, Char character)
        {
            String aSubString = "";
            int index1 = st.IndexOf(character);
            int index2 = (index1 == -1 ? -1 : st.IndexOf(character, index1 + 1));

            if ((index1 > -1) & (index2 > -1))
                aSubString = st.Substring(index1 + 1, index2 - index1 - 1);

            return aSubString;
        }

        /// <summary>
        /// Hashes the string.
        /// </summary>
        /// <param name="st">The string to hash.</param>
        /// <param name="hashName">The name of the algorithm to consider.</param>
        /// <returns></returns>
        public static String HashString(this string st, string hashName)
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
        public static String ToPath(this string st)
        {
            return st?.Replace('\\', Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// Gets the specified sub string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="startIndex">The start index to consider.</param>
        /// <param name="endIndex">The end index to consider.</param>
        /// <returns>The formated path.</returns>
        public static String GetSubstring(this String st, int startIndex, int endIndex=-1)
        {
            if (st == null) return null;

            if (startIndex < 0) startIndex = 0;
            if (endIndex == -1 || endIndex >= st.Length) endIndex = st.Length - 1;
            if ((st?.Length == 0) || (startIndex>=st.Length)|| (endIndex < startIndex)) return "";

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
        public static String GetStartedString(this String st, String startingString, String containingString = null)
        {
            return (String.IsNullOrEmpty(st) ? "" :
                ((containingString == null || st.Contains(containingString)) &&
                (startingString != null && st.StartsWith(startingString)) ?
                st : startingString + st));
        }

        /// <summary>
        /// Gets the string with the specified ending string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="endingString">The ending string to consider.</param>
        /// <returns>The formated path.</returns>
        public static String GetEndedString(this String st, String endingString)
        {
            return (String.IsNullOrEmpty(st) ? "" : (st.EndsWith(endingString) ? st : st + endingString));
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
        public static String GetFormatString(this String st, int index, String replaceString, String wholeReplaceString = null)
        {
            String indexString = "{" + index.ToString() + "}";
            int indexStringIndex = st.IndexOf(indexString);
            if (indexStringIndex > -1)
            {
                String stringToReplace = indexString;
                String newString = "";

                int startIndex = StringHelper.GetIndexOfLastString(st, "{", indexStringIndex - 1);
                if (startIndex > -1)
                {
                    int aEndIndex = StringHelper.GetIndexOfNextString(st, "}", indexStringIndex + indexString.Length);
                    if (aEndIndex > -1)
                        stringToReplace = st.Substring(startIndex, aEndIndex - startIndex + 1);

                    if (wholeReplaceString == null)
                        newString = stringToReplace.Substring(1, stringToReplace.Length - 2).Replace(indexString, replaceString);
                    else
                        newString = wholeReplaceString;
                }
                else
                    newString = replaceString;

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
        public static void GetIndexOfLastString(this String st, String stv, ref int startIndex)
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
            public static int GetIndexOfLastString(this String st, String stv, int startIndex)
        {
            int index = startIndex;
            Boolean b = false;
            while ((index >= 0) & !b)
            {
                if ((st.Substring(index, 1) == "\"") & (stv != "\""))
                    index = GetIndexOfLastString(st, "\"", index - 1) - 1;
                else if ((st.Substring(index, 1) == ")") & (stv == "("))
                    index = GetIndexOfLastString(st, "(", index - 1) - 1;
                else if ((st.Substring(index, 1) == "}") & (stv == "{"))
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
        public static void GetIndexOfNextString(this String st, String stv, ref int startIndex)
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
        public static int GetIndexOfNextString(this String st, String stv, int startIndex = 0, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            if (st == null || stv == null)
                return -1;

            int index = startIndex;
            Boolean b = false;
            int st_l = st.Length;
            int stv_l = stv.Length;
            while ((index < st_l) & index > -1 & !b)
            {
                if ((st.Substring(index, 1) == "\"") & (stv != "\""))
                    index = GetIndexOfNextString(st, "\"", index + 1) + 1;
                else if ((st.Substring(index, 1) == "'") & (stv == "'"))
                    index = GetIndexOfNextString(st, "'", index + 1) + 1;
                else if ((st.Substring(index, 1) == "(") & (stv == ")"))
                    index = GetIndexOfNextString(st, ")", index + 1) + 1;
                else if ((st.GetSubstring(index, index + 1) == "{{") & (stv == "}}"))
                    index = GetIndexOfNextString(st, "}}", index + 1) + 2;
                else if ((st.Substring(index, 1) == "{") & (stv == "}"))
                    index = GetIndexOfNextString(st, "}", index + 1) + 1;
                else if ((index <= st_l - stv_l) && (string.Equals(st.Substring(index, stv_l), stv, stringComparison)))
                    b = true;
                else
                    index ++;
            }
            return index;
        }

        /// <summary>
        /// Gets the date string of this instance.
        /// </summary>
        /// <param name="date">The date to consider.</param>
        /// <returns>Returns the date string of this instance.</returns>
        public static String GetString(this DateTime? date)
        {
            return (date == null ? null : date.Value.ToString(StringHelper.__DateFormat));
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
        public static string GetStringAtIndex(this List<String> strings, int index)
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
        public static String Concatenate(this String st1, String st2, String charString = null)
        {
            st1 = (st1 ?? "");
            st2 = (st2 ?? "");
            if ((charString == null) || (st2.StartsWith(charString)))
                return st1 + st2.Substring(1);
            else
                return st2;
        }

        /// <summary>
        /// Excludes the specified string items from the specified string items.
        /// </summary>
        /// <param name="stringItems">The string items to consider.</param>
        /// <param name="excludingStringItems">The string items to exclude.</param>
        /// <returns>Returns the excluded string items.</returns>
        public static List<String> Excluding(this List<String> stringItems, params String[] excludingStringItems)
        {
            return StringHelper.Excluding(stringItems, excludingStringItems.ToList());
        }

        /// <summary>
        /// Excludes the specified string items from the specified string items.
        /// </summary>
        /// <param name="stringItems">The string items to consider.</param>
        /// <param name="excludingStringItems">The string items to exclude.</param>
        /// <returns>Returns the excluded string items.</returns>
        public static List<String> Excluding(this List<String> stringItems, List<String> excludingStringItems)
        {
            if (stringItems == null)
            {
                return new List<String>();
            }
            else if (excludingStringItems == null)
                return stringItems;
            else
            {
                List<String> stringItems1 = new List<String>(stringItems).Select(p => p.ToKey()).ToList();
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
        public static List<String> Adding(this List<String> stringItems, params String[] addingStringItems)
        {
            return StringHelper.Adding(stringItems, addingStringItems.ToList());
        }

        /// <summary>
        /// Adds the specified string items from the specified string items.
        /// </summary>
        /// <param name="stringItems">The string items to consider.</param>
        /// <param name="addingStringItems">The string items to add.</param>
        /// <returns>Returns the added string items.</returns>
        public static List<String> Adding(this List<String> stringItems, List<String> addingStringItems)
        {
            if (stringItems == null)
            {
                return new List<String>();
            }
            else if (addingStringItems == null)
                return stringItems;
            else
            {
                new List<String>(stringItems).AddRange(addingStringItems);
                return stringItems;
            }
        }

        /// <summary>
        /// Get the key values from the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <returns>Returns the added string items.</returns>
        public static List<DataKeyValue> GetKeyValues(this String st)
        {
            List<DataKeyValue> dataKeyValues = new List<DataKeyValue>();
            foreach (String subString in st.Split('|'))
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

            if (st.Length>2 && st.StartsWith(quoteString) && st.EndsWith(quoteString))
                st = st.Substring(1, st.Length - 2);
            return st;
        }

        // String conversions --------------------------

        /// <summary>
        /// Gets the object from the specified string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="textFormat">The text format to consider.</param>
        /// <returns>Returns the object corresponding to the specified string.</returns>
        public static Object ToObject(this String st, DataValueType valueType = DataValueType.Any, String textFormat = null)
        {
            if (valueType == DataValueType.Any)
                valueType = st.GetValueType();

            switch (valueType)
            {
                case DataValueType.Date:
                    if (String.IsNullOrEmpty(textFormat)) textFormat = StringHelper.__DateFormat;
                    DateTime dateTime = new DateTime();
                    if (!DateTime.TryParseExact(st, textFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                        return null;
                    return new DateTime?(dateTime);
                case DataValueType.Time:
                    if (String.IsNullOrEmpty(textFormat)) textFormat = StringHelper.__TimeFormat;
                    TimeSpan aTimeSpan = new TimeSpan();
                    if (!TimeSpan.TryParseExact(st, textFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out aTimeSpan))
                        return null;
                    return new TimeSpan?(aTimeSpan);
                case DataValueType.Boolean:
                    return st.Equals("$TRUE()", StringComparison.OrdinalIgnoreCase) || st.Equals("TRUE", StringComparison.OrdinalIgnoreCase);
                case DataValueType.Number:
                    float afloat = new float();
                    float.TryParse(st, out afloat);
                    return new float?(afloat);
                case DataValueType.Integer:
                    int aInt = new int();
                    int.TryParse(st, out aInt);
                    return new int?(aInt);
                case DataValueType.Long:
                    long aLong = new long();
                    long.TryParse(st, out aLong);
                    return new long?(aLong);
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
        public static DateTime? ToDateTime(this String st, String textFormat = null)
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
        public static T ToEnum<T>(this String st, T defaultEnum = default(T)) where T : struct, IConvertible
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
            String pattern = "{{char1,8}}",
            params String[] charLists)
        {
            if (charLists==null)
            {
                charLists = new String[1]
                   { "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-" };
            }

            String st = "";
            Random rd = new Random();

            String currentMatch = pattern;
            int currentCharListIndex = 0;
            int currentLength = 8;
            String currentCharList;
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

        #endregion

    }
}
