namespace BindOpen.Data;

/// <summary>
/// 
/// </summary>
public static class ITBdoSetExtensions
{
    /// <summary>
    /// Adds the specified item.
    /// </summary>
    /// <param key="items">The items of the item to add.</param>
    /// <returns>Returns the new item that has been added.
    /// Returns null if the new item is null or else its name is null.</returns>
    /// <remarks>The new item must have a name.</remarks>
    public static Q WithItems<Q, T>(
        this Q set,
        params T[] items)
        where Q : ITBdoSet<T>
        where T : IReferenced
    {
        if (set != null && items != null)
        {
            foreach (var item in items)
            {
                set.Insert(item);
            }
        }

        return set;
    }

    /// <summary>
    /// Adds the specified item.
    /// </summary>
    /// <param key="items">The items of the item to add.</param>
    /// <returns>Returns the new item that has been added.
    /// Returns null if the new item is null or else its name is null.</returns>
    /// <remarks>The new item must have a name.</remarks>
    public static Q WithRange<Q, T>(
        this Q set,
        ITBdoSet<T> list)
        where Q : ITBdoSet<T>
        where T : IReferenced
    {
        set?.WithItems(list?.Items?.ToArray());

        return set;
    }
}