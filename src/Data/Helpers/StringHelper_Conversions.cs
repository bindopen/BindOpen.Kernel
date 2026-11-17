using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace BindOpen.Data.Helpers;

/// <summary>
/// This structure represents a string helper.
/// </summary>
public static partial class StringHelper
{
    /// <summary>
    /// Gets the date string of this instance.
    /// </summary>
    /// <param key="date">The date to consider.</param>
    /// <returns>Returns the date string of this instance.</returns>
    public static string ToString(this DateTime date)
    {
        return date.ToString(__DateTimeFormat, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Gets the date string of this instance.
    /// </summary>
    /// <param key="date">The date to consider.</param>
    /// <returns>Returns the date string of this instance.</returns>
    public static string ToString(this DateTime? date)
    {
        return date?.ToString(__DateTimeFormat, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Gets the string shorten to the specified characters.
    /// </summary>
    /// <param key="st">The string to consider.</param>
    /// <param key="charNumber">The number of characters to consider.</param>
    /// <param key="addedString">Indicates whether dots are added.</param>
    /// <returns>Returns the specified string shorten.</returns>
    public static string ToShortString(this string st, int charNumber, string addedString = "...")
    {
        if (string.IsNullOrEmpty(st) || charNumber <= 0) return string.Empty;

        if (st.Length <= charNumber) return st;

        int suffixLength = addedString?.Length ?? 0;
        int take = Math.Max(0, charNumber - suffixLength);

        if (take == 0) return addedString ?? string.Empty;

        // ToSubstring expects endIndex inclusive
        return st.ToSubstring(0, take - 1) + (addedString ?? string.Empty);
    }

    /// <summary>
    /// Gets the normalized string from the specified string.
    /// </summary>
    /// <param key="st">The string to normalize.</param>
    /// <returns>Returns the normalized string.</returns>
    /// <remarks>The normalized string is a string in which only the alphanumeric characters and _ are allowed.</remarks>
    public static string ToNormalizedName(this string st)
    {
        if (st == null) return null;
        return Regex.Replace(st, "[^0-9a-zA-Z_]", "_");
    }

    /// <summary>
    /// Gets the titled string from the specified string.
    /// </summary>
    /// <param key="st">The string to normalize.</param>
    /// <returns>Returns the normalized string.</returns>
    /// <remarks>The normalized string is a string in which only the alphanumeric characters and _ are allowed.</remarks>
    public static string ToTitleCasedName(string st)
    {
        if (string.IsNullOrEmpty(st)) return st;
        return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(st);
    }

    /// <summary>
    /// Gets the object from the specified string.
    /// </summary>
    /// <param key="st">The string to consider.</param>
    /// <param key="valueType">The value type to consider.</param>
    /// <param key="textFormat">The text format to consider.</param>
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
                // Try in order: integer, long, ulong, number, date, time, boolean, fallback to string
                if (int.TryParse(st, NumberStyles.Integer, CultureInfo.InvariantCulture, out var ai))
                    return ai;
                if (long.TryParse(st, NumberStyles.Integer, CultureInfo.InvariantCulture, out var al))
                    return al;
                if (ulong.TryParse(st, NumberStyles.Integer, CultureInfo.InvariantCulture, out var aul))
                    return aul;
                if (double.TryParse(st, NumberStyles.Any, CultureInfo.InvariantCulture, out var ad))
                    return ad;
                if (DateTime.TryParseExact(st, new[] { __DateTimeFormat, __DateTimeFormatShort }, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                    return dt;
                if (TimeSpan.TryParseExact(st, __TimeFormat, CultureInfo.InvariantCulture, out var ts))
                    return ts;
                if (bool.TryParse(st, out var ab))
                    return ab;
                return st;
            case DataValueTypes.Date:
                if (string.IsNullOrEmpty(textFormat))
                {
                    textFormat = st?.Trim().Length == 10 ? __DateTimeFormatShort : __DateTimeFormat;
                }
                if (DateTime.TryParseExact(st, textFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
                    return dateTime;
                return null;
            case DataValueTypes.Time:
                if (string.IsNullOrEmpty(textFormat)) textFormat = __TimeFormat;
                if (TimeSpan.TryParseExact(st, textFormat, CultureInfo.InvariantCulture, out var timeSpan))
                    return timeSpan;
                return null;
            case DataValueTypes.Boolean:
                if (bool.TryParse(st, out var boolVal)) return boolVal;
                if (st == "1") return true;
                if (st == "0") return false;
                return false;
            case DataValueTypes.Number:
                if (double.TryParse(st, NumberStyles.Any, CultureInfo.InvariantCulture, out var aDouble))
                    return aDouble;
                return null;
            case DataValueTypes.Integer:
                if (int.TryParse(st, NumberStyles.Integer, CultureInfo.InvariantCulture, out var aInt))
                    return aInt;
                return null;
            case DataValueTypes.Long:
                if (long.TryParse(st, NumberStyles.Integer, CultureInfo.InvariantCulture, out var aLong))
                    return aLong;
                return null;
            case DataValueTypes.ULong:
                if (ulong.TryParse(st, NumberStyles.Integer, CultureInfo.InvariantCulture, out var aULong))
                    return aULong;
                return null;
            case DataValueTypes.Binary:
                try
                {
                    return Convert.FromBase64String(st);
                }
                catch
                {
                    return null;
                }
            default:
                return st;
        }
    }

    /// <summary>
    /// Gets the date time object from the specified string.
    /// </summary>
    /// <param key="st">The string to consider.</param>
    /// <param key="textFormat">The text format to consider.</param>
    /// <returns>Returns the object corresponding to the specified string.</returns>
    public static DateTime? ToDateTime(this string st, string textFormat = null)
    {
        return st.ToObject(DataValueTypes.Date, textFormat) as DateTime?;
    }

    /// <summary>
    /// Gets the enumration from the specified string.
    /// </summary>
    /// <typeparam name="T">The structure to consider.</typeparam>
    /// <param key="st">The string to consider.</param>
    /// <param key="defaultEnum">The default enumeration to consider.</param>
    /// <returns>Returns the object corresponding to the specified string.</returns>
    public static T ToEnum<T>(this string st, T defaultEnum = default) where T : struct, IConvertible
    {
        T aEnum = defaultEnum;
        if (st != null && !Enum.TryParse(st, true, out aEnum))
            aEnum = defaultEnum;
        return aEnum;
    }
}
