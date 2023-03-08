using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    public static class BdoStringFilterExtension
    {
        /// <summary>
        /// Adds the specified strings.
        /// </summary>
        /// <param key="values">The string values to consider.</param>
        /// <returns>Returns the script exp.</returns>
        public static T Adding<T>(
            this T filter,
            params string[] values)
            where T : IBdoStringFilter
        {
            filter.AddedValues ??= new List<string>();
            filter.AddedValues.AddRange(values?.ToList());
            return filter;
        }

        /// <summary>
        /// Adds the specified strings.
        /// </summary>
        /// <param key="values">The string values to consider.</param>
        /// <returns>Returns the script exp.</returns>
        public static T Removing<T>(
            this T filter,
            params string[] values)
            where T : IBdoStringFilter
        {
            filter.RemovedValues ??= new List<string>();
            filter.RemovedValues.AddRange(values?.ToList());
            return filter;
        }
    }
}