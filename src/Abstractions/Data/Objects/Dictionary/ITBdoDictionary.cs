using BindOpen.Data.Helpers;
using System.Collections.Generic;

namespace BindOpen.Data;

/// <summary>
/// Represents a generic BindOpen dictionary.
/// As it is not metable, it is intended to be used as a technical container 
/// rather than a metadata element within a tree.
/// </summary>
/// <typeparam name="T">The type of elements in the dictionary.</typeparam>
public interface ITBdoDictionary<T> :
    IBdoObjectNotMetable, IIdentified, IReferenced,
    IDictionary<string, T>
{
    /// <summary>
    /// Gets the item associated with the specified key. 
    /// If not found, returns the item associated with the default key.
    /// </summary>
    /// <param name="key">The key of the item to get.</param>
    /// <param name="defaultKey">The default key to use if the primary key is missing.</param>
    /// <returns>The found item or the default item.</returns>
    T this[string key, string defaultKey] { get; }

    /// <summary>
    /// Adds the specified item to the dictionary.
    /// </summary>
    /// <param name="key">The key of the item to add.</param>
    /// <param name="item">The item to add.</param>
    /// <returns>Returns this instance for fluent chaining.</returns>
    new ITBdoDictionary<T> Add(string key, T item);

    /// <summary>
    /// Removes the items associated with the specified keys.
    /// </summary>
    /// <param name="keys">The keys of the items to remove.</param>
    void Remove(params string[] keys);

    /// <summary>
    /// Gets the item associated with the specified key or alternate name.
    /// </summary>
    /// <param name="key">The key of the item to get.</param>
    /// <param name="alternateName">The alternate name to check if the key is not found.</param>
    /// <returns>The found item.</returns>
    T Get(string key = StringHelper.__Star, string alternateName = null);

    /// <summary>
    /// Determines whether the specified dictionary is equal to the current one.
    /// </summary>
    /// <param name="dictionary">The dictionary to compare with.</param>
    /// <returns>True if the dictionaries are equal; otherwise, false.</returns>
    bool Equals(ITBdoDictionary<T> dictionary);
}