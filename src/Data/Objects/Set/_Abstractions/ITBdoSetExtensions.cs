namespace BindOpen.Data.Objects;

/// <summary>
/// This class provides extension methods for the ITBdoSet interface to facilitate
/// fluent collection population and management.
/// </summary>
public static class ITBdoSetExtensions
{
    /// <summary>
    /// Populates the specified set with the provided items.
    /// </summary>
    /// <typeparam name="Q">The type of the set.</typeparam>
    /// <typeparam name="T">The type of the referenced items.</typeparam>
    /// <param name="set">The set to populate.</param>
    /// <param name="items">The items to add to the set.</param>
    /// <returns>Returns the set instance for fluent chaining.</returns>
    /// <remarks>The items should typically be named or uniquely identifiable depending on 
    /// the set's implementation of the Insert method.</remarks>
    public static Q AddItems<Q, T>(
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
    /// Adds a range of items from another set to the specified set.
    /// </summary>
    /// <typeparam name="Q">The type of the target set.</typeparam>
    /// <typeparam name="T">The type of the referenced items.</typeparam>
    /// <param name="set">The target set to populate.</param>
    /// <param name="list">The source set containing items to add.</param>
    /// <returns>Returns the target set instance for fluent chaining.</returns>
    public static Q AddRange<Q, T>(
        this Q set,
        ITBdoSet<T> list)
        where Q : ITBdoSet<T>
        where T : IReferenced
    {
        set?.AddItems(list?.Items?.ToArray());

        return set;
    }
}