using BindOpen.Data.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Scoping;

/// <summary>
/// This static class provides generic extension methods for <see cref="IBdoExtensionStore"/>.
/// </summary>
public static class IBdoExtensionStoreExtensions
{
    /// <summary>
    /// Retrieves all extension definitions that match the specified generic type.
    /// </summary>
    /// <typeparam name="T">The specialized definition type (e.g., IBdoTaskDefinition).</typeparam>
    /// <param name="store">The extension store to query.</param>
    /// <returns>An enumeration of strongly-typed extension definitions.</returns>
    public static IEnumerable<T> GetDefinitions<T>(this IBdoExtensionStore store)
        where T : IBdoExtensionDefinition
    {
        if (store != null)
        {
            // Infers Kind from T, filters nulls, and casts to T
            return store.GetDefinitions(typeof(T).GetExtensionKind())
                        .Cast<T>()
                        .Where(q => q is not null);
        }

        return default;
    }

    /// <summary>
    /// Retrieves a specific extension definition by its unique name, cast to the specified type.
    /// </summary>
    /// <typeparam name="T">The specialized definition type.</typeparam>
    /// <param name="store">The extension store to query.</param>
    /// <param name="uniqueName">The unique identifier of the definition.</param>
    /// <returns>The strongly-typed extension definition, or default if not found.</returns>
    public static T GetDefinition<T>(
        this IBdoExtensionStore store,
        string uniqueName) where T : IBdoExtensionDefinition
    {
        if (store != null)
        {
            return store.GetDefinition(typeof(T).GetExtensionKind(), uniqueName).As<T>();
        }

        return default;
    }

    /// <summary>
    /// Retrieves the definition associated with the specified implementation type.
    /// </summary>
    /// <typeparam name="T">The implementation type of the extension.</typeparam>
    /// <param name="store">The extension store to query.</param>
    /// <returns>The definition associated with type T, cast to the appropriate definition type.</returns>
    public static T GetDefinitionFromType<T>(this IBdoExtensionStore store) where T : IBdoExtension
    {
        if (store != null)
        {
            return store.GetDefinitionFromType(typeof(T)).As<T>();
        }

        return default;
    }
}