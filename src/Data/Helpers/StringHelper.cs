using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BindOpen.Data.Helpers;

/// <summary>
/// This structure represents a string helper.
/// </summary>
public static partial class StringHelper
{
    /// <summary>
    /// Generates a password.
    /// </summary>
    /// <param key="charNumber">The character number to consider.</param>
    /// <returns>Returns the generated password.</returns>
    public static string GeneratePassword(int charNumber)
    {
        if (charNumber <= 0) return string.Empty;

        var s = NewGuid()
            .Replace("-", string.Empty)
            .Replace("l", string.Empty)
            .Replace("1", string.Empty)
            .Replace("o", string.Empty)
            .Replace("0", string.Empty);

        return s.Length <= charNumber ? s : s.Substring(0, charNumber);
    }

    /// <summary>
    /// Gets the number of occurrences of a specified substring in the specified string.
    /// </summary>
    public static int CountOccurences(this string st, string subString)
    {
        if (st == null) throw new ArgumentNullException(nameof(st));
        if (string.IsNullOrEmpty(subString)) throw new ArgumentException("subString cannot be null or empty", nameof(subString));

        int count = 0;
        int i = 0;
        while ((i = st.IndexOf(subString, i, StringComparison.Ordinal)) != -1)
        {
            i += subString.Length;
            count++;
        }
        return count;
    }

    /// <summary>
    /// Returns the string value of the specified settings.
    /// </summary>
    public static bool CheckNameFormat(this string stringValue, int limitSize)
    {
        if (stringValue == null) return false;

        string aRegEx = "^[a-zA-Z0-9_]{0," + limitSize.ToString() + "}$";
        return Regex.IsMatch(stringValue, aRegEx);
    }

    /// <summary>
    /// Hashes the string.
    /// </summary>
    public static string HashString(this string st, string hashName)
    {
        if (hashName == null) throw new ArgumentNullException(nameof(hashName));

        using var hashAlgorithm = (HashAlgorithm?)CryptoConfig.CreateFromName(hashName);
        if (hashAlgorithm == null)
        {
            throw new ArgumentException("Unrecognized hash name", nameof(hashName));
        }

        byte[] input = Encoding.UTF8.GetBytes(st ?? string.Empty);
        byte[] bytes = hashAlgorithm.ComputeHash(input);

        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Sanitizes the specified string.
    /// </summary>
    public static string Sanitize(this string st)
    {
        return st?.Replace("\r", string.Empty).Replace("\n", string.Empty);
    }

    /// <summary>
    /// Generates a new short ID.
    /// </summary>
    public static string NewGuid()
    {
        return Guid.NewGuid().ToString().ToLower();
    }

    /// <summary>
    /// Generates a new short ID.
    /// </summary>
    public static string NewShortId()
    {
        return Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
    }

    /// <summary>
    /// Concatenates the two specified string only if the second one starts with the specified character(s). Returns the second string otherwise.
    /// </summary>
    public static string Concatenate(this string st1, string st2, string charString = null)
    {
        st1 ??= string.Empty;
        st2 ??= string.Empty;
        if (charString == null)
        {
            return st1 + st2;
        }
        if (st2.StartsWith(charString))
        {
            return st1 + st2.Substring(charString.Length);
        }
        return st2;
    }

    /// <summary>
    /// Sets the first string as the second one if the specified condition is satisfied.
    /// </summary>
    public static string If(this string st1, bool condition, string st2)
    {
        if (condition)
        {
            return st2;
        }

        return st1;
    }

    /// <summary>
    /// Concatenates the first string with the second one if the specified condition is satisfied.
    /// </summary>
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
    public static string GetRandomString(
        string pattern = "{{char1,8}}",
        params string[] charLists)
    {
        if (charLists == null || charLists.Length == 0)
        {
            charLists = new string[]
               { "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-" };
        }

        int currentLength = 8;
        if (!string.IsNullOrEmpty(pattern))
        {
            var m = Regex.Match(pattern, @"\d+");
            if (m.Success && int.TryParse(m.Value, out var parsed))
            {
                currentLength = parsed;
            }
        }

        var currentCharList = charLists[0] ?? string.Empty;
        var sb = new StringBuilder(currentLength);
        for (int i = 0; i < currentLength; i++)
        {
            int idx = RandomNumberGenerator.GetInt32(currentCharList.Length);
            sb.Append(currentCharList[idx]);
        }

        return sb.ToString();
    }
}
