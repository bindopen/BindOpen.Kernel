using BindOpen.Data;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Scoping;

/// <summary>
/// This static class provides extension methods for <see cref="IExtensionLoadOptions"/> 
/// to enable a fluent configuration of extension loading.
/// </summary>
public static partial class ExtensionLoadOptionsExtensions
{
    /// <summary>
    /// Adds a data source to the specified load options.
    /// </summary>
    /// <typeparam name="T">The type of the load options.</typeparam>
    /// <param name="options">The load options instance to update.</param>
    /// <param name="kind">The kind of data source (e.g., Memory, File, etc.).</param>
    /// <param name="uri">The URI of the data source.</param>
    /// <returns>Returns the load options instance for fluent chaining.</returns>
    public static T AddSource<T>(
        this T options,
        DatasourceKind kind,
        string uri = null)
        where T : IExtensionLoadOptions
    {
        if (options != null)
        {
            // Ensure the list is initialized (Null-coalescing assignment)
            options.Sources ??= new List<(DatasourceKind Kind, string Uri)>();
            options.Sources.Add((kind, uri));
        }
        return options;
    }

    /// <summary>
    /// Sets the specific extension kinds to consider for the loading process.
    /// </summary>
    /// <typeparam name="T">The type of the load options.</typeparam>
    /// <param name="options">The load options instance to update.</param>
    /// <param name="extensionKinds">The array of extension kinds to consider.</param>
    /// <returns>Returns the load options instance for fluent chaining.</returns>
    public static T WithExtensionKinds<T>(
        this T options,
        params BdoExtensionKinds[] extensionKinds)
        where T : IExtensionLoadOptions
    {
        if (options != null)
        {
            // Replace the existing list with a new one from the params array
            options.ExtensionKinds = extensionKinds?.ToList() ?? new List<BdoExtensionKinds>();
        }
        return options;
    }
}