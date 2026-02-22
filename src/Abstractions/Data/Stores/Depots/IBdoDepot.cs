using BindOpen.Data.Assemblies;
using BindOpen.Logging;
using BindOpen.Scoping;
using System;

namespace BindOpen.Data.Stores;

/// <summary>
/// Defines a BindOpen depot, a specialized container for managing and 
/// dynamically loading data from assemblies or through custom functions.
/// </summary>
public interface IBdoDepot : IIdentified, IBdoScoped
{
    /// <summary>
    /// Populates the depot by scanning all available assemblies in the current context.
    /// </summary>
    /// <param name="log">The log used for tracking the discovery and loading process.</param>
    /// <returns>The current depot instance for fluent chaining.</returns>
    IBdoDepot AddFromAllAssemblies(IBdoLog log = null);

    /// <summary>
    /// Populates the depot by scanning a specific assembly.
    /// </summary>
    /// <param name="reference">The reference to the assembly to scan.</param>
    /// <param name="log">The log used for tracking the discovery and loading process.</param>
    /// <returns>The current depot instance for fluent chaining.</returns>
    IBdoDepot AddFromAssembly(IBdoAssemblyReference reference, IBdoLog log = null);

    /// <summary>
    /// Populates the depot by scanning the assembly that contains the specified type.
    /// </summary>
    /// <typeparam name="T">The type used to identify the target assembly.</typeparam>
    /// <param name="log">The log used for tracking the discovery and loading process.</param>
    /// <returns>The current depot instance for fluent chaining.</returns>
    IBdoDepot AddFromAssembly<T>(IBdoLog log = null) where T : class;

    /// <summary>
    /// Executes the internal lazy-loading logic to populate the depot's items.
    /// </summary>
    /// <param name="log">The log used for tracking the lazy-loading process.</param>
    /// <returns>True if the loading was successful; otherwise, false.</returns>
    bool LoadLazy(IBdoLog log);

    /// <summary>
    /// Gets or sets the custom function used for lazy-loading. 
    /// This function typically returns the number of items successfully loaded.
    /// </summary>
    Func<IBdoDepot, IBdoLog, int> LazyLoadFunction { get; set; }
}