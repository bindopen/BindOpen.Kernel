using BindOpen.Data.Helpers;
using System.Collections.Generic;

namespace BindOpen.Data.Objects.Interfaces;

/// <summary>
/// This class provides extension methods for the ITBdoDictionary interface.
/// It facilitates the fluent configuration of dictionaries such as Titles or Descriptions.
/// </summary>
public static class ITBdoDictionaryExtensions
{
    // Add -----------------------------

    /// <summary>
    /// Populates the dictionary with the specified key-value pairs.
    /// </summary>
    /// <typeparam name="T">The dictionary type.</typeparam>
    /// <typeparam name="TItem">The item type.</typeparam>
    /// <param name="dico">The dictionary to populate.</param>
    /// <param name="pairs">The key-value pairs to add.</param>
    /// <returns>Returns the dictionary instance for fluent chaining.</returns>
    public static T With<T, TItem>(
        this T dico,
        params KeyValuePair<string, TItem>[] pairs)
        where T : ITBdoDictionary<TItem>
    {
        if (dico != null && pairs != null)
        {
            foreach (var pair in pairs)
            {
                dico.Add(pair.Key, pair.Value);
            }
        }

        return dico;
    }

    /// <summary>
    /// Adds an item to the dictionary using the default wildcard key.
    /// </summary>
    /// <typeparam name="T">The dictionary type.</typeparam>
    /// <typeparam name="TItem">The item type.</typeparam>
    /// <param name="dico">The dictionary to populate.</param>
    /// <param name="item">The item to add.</param>
    /// <returns>Returns the dictionary instance for fluent chaining.</returns>
    public static T With<T, TItem>(
        this T dico,
        TItem item)
        where T : ITBdoDictionary<TItem>
    {
        dico?.Add(StringHelper.__Star, item);

        return dico;
    }

    /// <summary>
    /// Adds an item to the dictionary with the specified key.
    /// </summary>
    /// <typeparam name="T">The dictionary type.</typeparam>
    /// <typeparam name="TItem">The item type.</typeparam>
    /// <param name="dico">The dictionary to populate.</param>
    /// <param name="key">The key to associate with the item.</param>
    /// <param name="item">The item to add.</param>
    /// <returns>Returns the dictionary instance for fluent chaining.</returns>
    public static T With<T, TItem>(
        this T dico,
        string key,
        TItem item)
        where T : ITBdoDictionary<TItem>
    {
        dico?.Add(key, item);

        return dico;
    }
}