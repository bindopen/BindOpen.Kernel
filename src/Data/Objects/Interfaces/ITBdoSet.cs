using System;
using System.Collections.Generic;

namespace BindOpen.Data.Objects.Interfaces;

/// <summary>
/// Represents a strongly-typed set of referenced objects.
/// This interface provides advanced methods for item retrieval, insertion, 
/// and collection management.
/// </summary>
/// <typeparam name="T">The type of referenced items in the set.</typeparam>
public interface ITBdoSet<T> : IEnumerable<T>, IBdoSet, IUpdatable
    where T : IReferenced
{
    /// <summary>
    /// Gets the number of items contained in the set.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Gets the underlying list of items.
    /// </summary>
    List<T> Items { get; }

    /// <summary>
    /// Gets the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to retrieve.</param>
    /// <returns>The item at the specified index.</returns>
    new T this[int index] { get; }

    /// <summary>
    /// Gets the item associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the item to retrieve.</param>
    /// <returns>The item associated with the specified key.</returns>
    new T this[string key] { get; }

    /// <summary>
    /// Gets the item associated with the specified key or an alternate name.
    /// </summary>
    /// <param name="key">The primary key to search for.</param>
    /// <param name="alternateName">The alternate name to check if the primary key is not found.</param>
    /// <returns>The found item; otherwise, null.</returns>
    T this[string key, string alternateName] { get; }

    /// <summary>
    /// Retrieves the item associated with the specified key.
    /// </summary>
    /// <param name="key">The key to search for.</param>
    /// <returns>The found item.</returns>
    T Get(string key);

    /// <summary>
    /// Retrieves the item associated with the specified key or an alternate name.
    /// </summary>
    /// <param name="key">The primary key to search for.</param>
    /// <param name="alternateName">The alternate name to check if the primary key is not found.</param>
    /// <returns>The found item.</returns>
    T Get(string key, string alternateName = null);

    /// <summary>
    /// Retrieves the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item.</param>
    /// <returns>The item at the specified index.</returns>
    T Get(int index = 0);

    /// <summary>
    /// Retrieves and casts the item associated with the specified key.
    /// </summary>
    /// <typeparam name="Q">The type to cast the item to.</typeparam>
    /// <param name="key">The key to search for.</param>
    /// <returns>The found item cast to Q.</returns>
    Q Get<Q>(string key) where Q : T;

    /// <summary>
    /// Retrieves and casts the item associated with the specified key or alternate name.
    /// </summary>
    /// <typeparam name="Q">The type to cast the item to.</typeparam>
    /// <param name="key">The primary key to search for.</param>
    /// <param name="alternateName">The alternate name to check.</param>
    /// <returns>The found item cast to Q.</returns>
    Q Get<Q>(string key, string alternateName = null) where Q : T;

    /// <summary>
    /// Retrieves and casts the item at the specified index.
    /// </summary>
    /// <typeparam name="Q">The type to cast the item to.</typeparam>
    /// <param name="index">The zero-based index of the item.</param>
    /// <returns>The item at the specified index cast to Q.</returns>
    Q Get<Q>(int index = 0) where Q : T;

    /// <summary>
    /// Inserts the specified item into the set.
    /// </summary>
    /// <param name="item">The item to insert.</param>
    /// <returns>The inserted item.</returns>
    T Insert(T item);

    /// <summary>
    /// Determines whether the set contains an item with the specified key.
    /// </summary>
    /// <param name="key">The key to check for. If null, checks if the set has any items.</param>
    /// <returns>True if the item or any item exists; otherwise, false.</returns>
    bool Has(string key = null);

    /// <summary>
    /// Removes all items from the set.
    /// </summary>
    void Clear();

    /// <summary>
    /// Removes the items associated with the specified keys.
    /// </summary>
    /// <param name="keys">The keys of the items to remove.</param>
    /// <returns>The number of items removed.</returns>
    int Remove(params string[] keys);

    /// <summary>
    /// Removes items that match the specified filter.
    /// </summary>
    /// <param name="filter">The predicate to filter items for removal.</param>
    /// <returns>The number of items removed.</returns>
    int Remove(Predicate<T> filter);

    /// <summary>
    /// Copies the items of the set to a new array.
    /// </summary>
    /// <returns>An array containing the items.</returns>
    T[] ToArray();

    /// <summary>
    /// Copies the items of the set to a new list.
    /// </summary>
    /// <returns>A list containing the items.</returns>
    IList<T> ToList();
}