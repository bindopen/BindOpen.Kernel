using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoStringSetExtensions
    {
        /// <summary>
        /// Gets the values allowed by this instance.
        /// </summary>
        /// <param key="all">All the values to consider.</param>
        /// <returns>Returns all the values allowed by this instance.</returns>
        public static IList<string> Merge(
            this IList<string> values,
            IBdoStringSet set)
        {
            var all = new List<string>();

            if (values != null)
            {
                all.AddRange(values);

                if (set?.AddedValues != null)
                {
                    all.AddRange(set.AddedValues);
                    all = all.Distinct()?.ToList();
                }
                if (set?.RemovedValues != null)
                {
                    all.RemoveAll(p => set.RemovedValues.Contains(p));
                }
            }

            return all ?? new();
        }

        /// <summary>
        /// Indicates whether the value is allowed by this instance.
        /// </summary>
        /// <param key="value">The value to validate.</param>
        /// <param key="all">All the values to consider.</param>
        /// <returns>Returns True if the specified is validated by this instance.</returns>
        public static bool Contains(
            this IList<string> values,
            string value,
            IBdoStringSet set)
        {
            return values?.Merge(set).Contains(value) ?? false;
        }
    }
}