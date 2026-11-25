namespace BindOpen.Data.Meta;

/// <summary>
/// Defines a BindOpen metadata object.
/// A meta-object is a specialized metadata set that represents a complex, 
/// often typed, data structure.
/// </summary>
public interface IBdoMetaObject : IBdoMetaSet
{
    /// <summary>
    /// Synchronizes the underlying CLR object data with the metadata object's child items.
    /// </summary>
    bool UpdateObjectData();
}