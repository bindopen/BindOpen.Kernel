using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Helpers;

/// <summary>
/// This class represents a helper for objects.
/// </summary>
public static partial class StringHelper
{
    /// <summary>
    /// Excludes the specified string items from the specified string items, except the exception items.
    /// </summary>
    /// <param name="items">The string items to consider.</param>
    /// <param name="exceptionItems">The string items to keep (exceptions to the exclusion).</param>
    /// <returns>Returns the excluded string items.</returns>
    public static T Excluding<T>(
        this T items,
        ICollection<string> exceptionItems)
        where T : ICollection<string>, new()
    {
        if (items == null || exceptionItems == null)
            return items;
        else
        {
            var r = new List<string>(items);
            r.RemoveAll(p => items.Any(q => q.BdoKeyEquals(p)) && !exceptionItems.Any(q => q.BdoKeyEquals(p)));

            var q = new T();
            return q.Adding(r);
        }
    }

    /// <summary>
    /// Adds the specified string items from the specified string items.
    /// </summary>
    /// <param key="items">The string items to consider.</param>
    /// <param key="addingItems">The string items to add.</param>
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
