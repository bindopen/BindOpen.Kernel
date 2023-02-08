using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BindOpen.Data.Helpers
{
    /// <summary>
    /// This structure represents a string helper.
    /// </summary>
    public static partial class StringHelper
    {
        /// <summary>
        /// Generates a password.
        /// </summary>
        /// <param name="charNumber">The character number to consider.</param>
        /// <returns>Returns the generated password.</returns>
        public static string GeneratePassword(int charNumber)
        {
            return NewGuid()
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
        /// Hashes the string.
        /// </summary>
        /// <param name="st">The string to hash.</param>
        /// <param name="hashName">The name of the algorithm to consider.</param>
        /// <returns></returns>
        public static string HashString(this string st, string hashName)
        {
            HashAlgorithm hashAlgorithm = (HashAlgorithm)CryptoConfig.CreateFromName(hashName);
            if (hashAlgorithm == null)
            {
                throw new ArgumentException("Unrecognized hash name", nameof(hashName));
            }

            byte[] bytes = hashAlgorithm.ComputeHash(st.ToByteArray(Encoding.UTF8));

            return Convert.ToBase64String(bytes);
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
                charLists = new string[1]
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
    }
}
