using System.Collections.Generic;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    public static class BdoMergerExtension
    {
        /// <summary>
        /// Adds the specified strings.
        /// </summary>
        /// <param key="values">The string values to consider.</param>
        /// <returns>Returns the script exp.</returns>
        public static T Adding<T>(
            this T filter,
            params string[] values)
            where T : IBdoFilter
        {
            if (filter != null)
            {
                filter.AddedValues ??= new List<string>();
                foreach (var value in values)
                {
                    filter.AddedValues.Add(value);
                }
            }

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
            where T : IBdoFilter
        {
            if (filter != null)
            {
                filter.RemovedValues ??= new List<string>();
                foreach (var value in values)
                {
                    filter.RemovedValues.Add(value);
                }
            }

            return filter;
        }
    }
}