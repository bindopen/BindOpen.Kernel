namespace BindOpen.Data.Meta;

/// <summary>
/// Defines a BindOpen metadata object.
/// A meta-object is a specialized metadata set that represents a complex, 
/// often typed, data structure.
/// </summary>
public interface IBdoMetaObject : IBdoMetaSet
{
    // This interface acts as a semantic marker to distinguish between
    // a simple collection of data (MetaSet) and a structured object 
    // representing a specific entity or class instance.
}