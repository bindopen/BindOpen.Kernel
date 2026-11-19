using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BindOpen.Data;

/// <summary>
/// This class provides extension methods for IBdoMetaSet to facilitate 
/// metadata manipulation, grouping, and conditional invocation.
/// </summary>
public static class IBdoMetaSetExtensions
{
    /// <summary>
    /// Maps specific metadata elements to actions based on their keys.
    /// </summary>
    /// <param name="set">The metadata set to process.</param>
    /// <param name="pairs">A collection of key-action pairs to execute.</param>
    public static void Map(
        this IBdoMetaSet set,
        params (string Key, Action<IBdoMetaData> Action)[] pairs)
    {
        if (set != null && pairs != null)
        {
            foreach (var (Key, Action) in pairs)
            {
                var meta = set[Key];
                if (meta != null)
                {
                    Action?.Invoke(meta);
                }
            }
        }
    }

    /// <summary>
    /// Retrieves a metadata element belonging to a specific group.
    /// </summary>
    /// <param name="set">The metadata set to search.</param>
    /// <param name="key">The key of the element. If null, returns the first found in group.</param>
    /// <param name="groupId">The group identifier to filter by.</param>
    /// <returns>The found metadata element; otherwise, null.</returns>
    public static IBdoMetaData GetOfGroup(
        this IBdoMetaSet set,
        string key,
        string groupId)
    {
        if (set == null) return null;

        return set.FirstOrDefault(p =>
            p.OfGroup(groupId)
            && (key == null || p.BdoKeyEquals(key)));
    }

    /// <summary>
    /// Retrieves all metadata elements belonging to a specific group.
    /// </summary>
    /// <param name="set">The metadata set to filter.</param>
    /// <param name="groupId">The group identifier to filter by.</param>
    /// <returns>A collection of metadata elements in the specified group.</returns>
    public static IEnumerable<IBdoMetaData> GetOfGroup(
        this IBdoMetaSet set,
        string groupId)
    {
        return set?.Where(p => p.OfGroup(groupId)) ?? Enumerable.Empty<IBdoMetaData>();
    }

    /// <summary>
    /// Determines whether the set contains any element belonging to the specified group.
    /// </summary>
    /// <param name="set">The metadata set to check.</param>
    /// <param name="groupId">The group identifier.</param>
    /// <returns>True if the group exists in the set; otherwise, false.</returns>
    public static bool HasGroup(
        this IBdoMetaSet set,
        string groupId = null)
    {
        return set?.Any(p => p.OfGroup(groupId)) == true;
    }

    /// <summary>
    /// Determines whether the set contains a specific element within a group.
    /// </summary>
    /// <param name="set">The metadata set to check.</param>
    /// <param name="key">The key of the element.</param>
    /// <param name="groupId">The group identifier (defaults to any/star).</param>
    /// <returns>True if the element exists; otherwise, false.</returns>
    public static bool Has(
        this IBdoMetaSet set,
        string key,
        string groupId = StringHelper.__Star)
    {
        return set?.Any(p =>
            p.OfGroup(groupId)
            && p.BdoKeyEquals(key)) == true;
    }

    // --- Conditional Invocation ---

    /// <summary>
    /// Executes the specified action if the predicate returns true for the current object.
    /// </summary>
    public static T Invoke<T>(this T obj, Predicate<T> fun, Action action)
    {
        if (obj != null && fun?.Invoke(obj) == true)
        {
            action?.Invoke();
        }

        return obj;
    }

    /// <summary>
    /// Asynchronously executes the specified action if the predicate returns true for the current object.
    /// </summary>
    public static async Task<T> InvokeAsync<T>(this T obj, Predicate<T> fun, Func<Task> action)
    {
        if (obj != null && fun?.Invoke(obj) == true)
        {
            if (action != null)
            {
                await action();
            }
        }

        return obj;
    }
}