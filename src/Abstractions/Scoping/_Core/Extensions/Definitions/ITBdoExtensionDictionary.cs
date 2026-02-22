using BindOpen.Data;

namespace BindOpen.Scoping;

/// <summary>
/// Defines a strongly-typed dictionary of BindOpen extensions.
/// </summary>
/// <typeparam name="T">The type of extension definition contained in this dictionary.</typeparam>
/// <remarks>
/// This interface combines the metadata of an extension dictionary with the 
/// set operations of a BindOpen set, providing a robust container for plugin management.
/// </remarks>
public interface ITBdoExtensionDictionary<T> : IBdoExtensionDictionary, ITBdoSet<T>
    where T : IBdoExtensionDefinition
{
}