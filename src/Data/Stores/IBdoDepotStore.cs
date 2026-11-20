using BindOpen.Data.Objects;
using BindOpen.Logging;
using BindOpen.Scoping;
using System;
using System.Collections.Generic;

namespace BindOpen.Data.Stores;

/// <summary>
/// Defines a BindOpen depot store.
/// A depot store manages a collection of depots, providing methods to add, 
/// retrieve, and lazily load data containers within a specific scope.
/// </summary>
public interface IBdoDepotStore : IBdoObject
{
    /// <summary>
    /// Gets the collection of depots managed by this store, indexed by their type.
    /// </summary>
    Dictionary<Type, IBdoDepot> Depots { get; }

    /// <summary>
    /// Adds a depot to the store and optionally executes an initialization action.
    /// </summary>
    /// <typeparam name="T">The specific type of the depot.</typeparam>
    /// <param name="depot">The depot instance to add.</param>
    /// <param name="action">An optional action to perform on the depot after it is added.</param>
    /// <returns>Returns the updated depot store instance for fluent chaining.</returns>
    IBdoDepotStore Add<T>(T depot, Action<T> action = null) where T : IBdoDepot;

    /// <summary>
    /// Retrieves the depot of the specified type from the store.
    /// </summary>
    /// <typeparam name="T">The type of the depot to retrieve.</typeparam>
    /// <returns>The depot instance of type T, or null if not found.</returns>
    T Get<T>() where T : IBdoDepot;

    /// <summary>
    /// Removes all depots from this store and clears internal resources.
    /// </summary>
    void Clear();

    /// <summary>
    /// Triggers the lazy-loading logic for all registered depots within the specified scope.
    /// </summary>
    /// <param name="scope">The scope to use for data resolution during loading.</param>
    /// <param name="log">The log used to track the loading process and any issues.</param>
    /// <returns>Returns true if all depots loaded successfully; otherwise, false.</returns>
    bool LoadLazy(IBdoScope scope, IBdoLog log);
}