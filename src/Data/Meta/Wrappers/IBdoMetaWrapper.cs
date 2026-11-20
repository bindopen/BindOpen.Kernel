using BindOpen.Data.Objects;
using BindOpen.Scoping;

namespace BindOpen.Data.Meta;

/// <summary>
/// Defines a BindOpen metadata wrapper.
/// A wrapper bridges a concrete C# object with its metadata representation, 
/// allowing for automatic property synchronization and scoped data retrieval.
/// </summary>
public interface IBdoMetaWrapper : IBdoObject, IBdoScoped, IBdoDetailed
{
    /// <summary>
    /// Retrieves and casts the data associated with the specified name.
    /// </summary>
    /// <typeparam name="T">The type to cast the data to.</typeparam>
    /// <param name="name">The name of the metadata element to retrieve.</param>
    /// <returns>The resolved data of type T.</returns>
    T GetData<T>(string name);

    /// <summary>
    /// Retrieves the raw data object associated with the specified name.
    /// </summary>
    /// <param name="name">The name of the metadata element to retrieve.</param>
    /// <returns>The resolved data object.</returns>
    object GetData(string name);

    /// <summary>
    /// Updates the internal detail (MetaSet) of this wrapper.
    /// </summary>
    /// <param name="detail">The new metadata set to apply.</param>
    /// <param name="onlyMetaAttributes">If true, only properties decorated with meta-attributes are updated.</param>
    void UpdateDetail(
        IBdoMetaSet detail,
        bool onlyMetaAttributes = true);

    /// <summary>
    /// Synchronizes the C# properties of this instance with its internal metadata detail.
    /// </summary>
    /// <param name="onlyMetaAttributes">If true, only properties decorated with meta-attributes are updated.</param>
    void UpdateProperties(
        bool onlyMetaAttributes = true);
}