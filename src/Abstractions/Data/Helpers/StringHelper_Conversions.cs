using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace BindOpen.Kernel.Data.Helpers
{
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
            return date.ToString(__DateTimeFormat);
        }

        /// <summary>
        /// Gets the date string of this instance.
        /// </summary>
        /// <param key="date">The date to consider.</param>
        /// <returns>Returns the date string of this instance.</returns>
        public static string ToString(this DateTime? date)
        {
            return date?.ToString(__DateTimeFormat);
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
            string shortString = string.Empty;
            if (st != null)
                shortString = st.Length > charNumber ? st.ToSubstring(0, charNumber - 4) + addedString : st;
            return shortString;
        }

        /// <summary>
        /// Gets the normalized string from the specified string.
        /// </summary>
        /// <param key="st">The string to normalize.</param>
        /// <returns>Returns the normalized string.</returns>
        /// <remarks>The normalized string is a string in which only the alphanumeric characters and _ are allowed.</remarks>
        public static string ToNormalizedName(this string st)
        {
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
                    if (string.IsNullOrEmpty(textFormat))
                    {
                        textFormat = st?.Trim().Length == 10 ? __DateTimeFormatShort : __DateTimeFormat;
                    }
                    DateTime dateTime;
                    if (!DateTime.TryParseExact(st, textFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                        return null;
                    return new DateTime?(dateTime);
                case DataValueTypes.Time:
                    if (string.IsNullOrEmpty(textFormat)) textFormat = __TimeFormat;
                    TimeSpan timeSpan;
                    if (!TimeSpan.TryParseExact(st, textFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out timeSpan))
                        return null;
                    return new TimeSpan?(timeSpan);
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
                case DataValueTypes.Binary:
                    byte[] aByteArray = Convert.FromBase64String(st);
                    return aByteArray;
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
}
