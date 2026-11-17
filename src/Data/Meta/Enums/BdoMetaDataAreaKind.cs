namespace BindOpen.Data.Meta;

/// <summary>
/// Specifies the various kinds of data areas within a metadata structure.
/// This helps the engine categorize where specific metadata resides or how it should be targeted.
/// </summary>
public enum BdoMetaDataAreaKind
{
    /// <summary>
    /// No specific data area defined.
    /// </summary>
    None,

    /// <summary>
    /// Represents any kind of data area. Typically used for broad searches or filters.
    /// </summary>
    Any,

    /// <summary>
    /// Represents the area containing element-level attributes or properties.
    /// </summary>
    Element
}