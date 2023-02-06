using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a helper for objects.
    /// </summary>
    public static class CollectionHelper
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
        /// <param name="strings">The objects to consider.</param>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the normalized string.</returns>
        public static T GetAt<T>(
            this T[] strings,
            int index)
        {
            return strings != null
                && strings.Length > index
                && strings[index] != null ? strings[index] : default;
        }

        /// <summary>
        /// Excludes the specified string items from the specified string items.
        /// </summary>
        /// <param name="items">The string items to consider.</param>
        /// <param name="excludingStringItems">The string items to exclude.</param>
        /// <returns>Returns the excluded string items.</returns>
        public static IEnumerable<T> Excluding<T>(
            this IEnumerable<T> items,
            params string[] excludingItems)
        {
            return items.Excluding(excludingItems.ToList());
        }

        /// <summary>
        /// Excludes the specified string items from the specified string items.
        /// </summary>
        /// <param name="items">The string items to consider.</param>
        /// <param name="excludingStringItems">The string items to exclude.</param>
        /// <returns>Returns the excluded string items.</returns>
        public static IEnumerable<T> Excluding<T>(
            this IEnumerable<T> items,
            IEnumerable<string> excludingItems)
        {
            if (items == null)
            {
                return new List<T>();
            }
            else if (excludingItems == null)
            {
                return items;
            }
            else
            {
                var r = new List<T>(items);
                r.RemoveAll(p =>
                    items.Any(q => q.BdoKeyEquals(p)));
                return r;
            }
        }

        /// <summary>
        /// Adds the specified string items from the specified string items.
        /// </summary>
        /// <param name="items">The string items to consider.</param>
        /// <param name="addingItems">The string items to add.</param>
        /// <returns>Returns the added string items.</returns>
        public static IEnumerable<T> Adding<T>(
            this IEnumerable<T> items,
            params T[] addingItems)
        {
            return items.Adding(addingItems.ToList());
        }

        /// <summary>
        /// Adds the specified string items from the specified string items.
        /// </summary>
        /// <param name="items">The string items to consider.</param>
        /// <param name="addingItems">The string items to add.</param>
        /// <returns>Returns the added string items.</returns>
        public static IEnumerable<T> Adding<T>(
            this IEnumerable<T> items,
            IEnumerable<T> addingItems)
        {
            if (items == null)
            {
                return new List<T>();
            }
            else if (addingItems == null)
                return items;
            else
            {
                new List<T>(items).AddRange(addingItems);
                return items;
            }
        }
    }
}
