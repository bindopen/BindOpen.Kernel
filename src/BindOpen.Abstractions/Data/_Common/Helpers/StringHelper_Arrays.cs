using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Helpers
{
    /// <summary>
    /// This class represents a helper for objects.
    /// </summary>
    public static partial class StringHelper
    {
        /// <summary>
        /// Returns the string representation of the specified object.
        /// </summary>
        /// <param name="obj">The object to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <returns></returns>
        public static Q GetAt<Q>(
            this IList<Q> obj,
            int index)
            => obj != null
            && index >= 0
            && index < obj.Count ? obj[index] : default;

        /// <summary>
        /// Gets the string at the specified index from the specified index.
        /// </summary>
        /// <param name="arr">The objects to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static T GetAt<T>(
            this T[] arr,
            int index)
        {
            return arr != null
                && arr.Length > index
                && arr[index] != null ? arr[index] : default;
        }

        /// <summary>
        /// Excludes the specified string items from the specified string items.
        /// </summary>
        /// <param name="items">The string items to consider.</param>
        /// <param name="excludingStringItems">The string items to exclude.</param>
        /// <returns>Returns the excluded string items.</returns>
        public static T Excluding<T>(
            this T items,
            ICollection<string> excludingItems)
            where T : ICollection<string>, new()
        {
            if (items == null || excludingItems == null)
                return items;
            else
            {
                var r = new List<string>(items);
                r.RemoveAll(p => items.Any(q => q.BdoKeyEquals(p)));

                var q = new T();
                return q.Adding(r);
            }
        }

        /// <summary>
        /// Adds the specified string items from the specified string items.
        /// </summary>
        /// <param name="items">The string items to consider.</param>
        /// <param name="addingItems">The string items to add.</param>
        /// <returns>Returns the added string items.</returns>
        public static T Adding<T>(
            this T items,
            ICollection<string> addingItems)
            where T : ICollection<string>, new()
        {
            if (items == null || addingItems == null)
                return items;
            else
            {
                var r = new List<string>(items);
                r.AddRange(addingItems);

                var q = new T();
                foreach (var r_item in r.Distinct())
                {
                    q.Add(r_item);
                }

                return q;
            }
        }
    }
}
