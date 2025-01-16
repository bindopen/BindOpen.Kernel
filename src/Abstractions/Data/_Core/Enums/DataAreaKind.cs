namespace BindOpen.Data;

/// <summary>
/// This enumerates the possible kinds of data area.
/// </summary>
public enum DataAreaKind
{
    /// <summary>
    /// None.
    /// </summary>
    None,

    /// <summary>
    /// Any.
    /// </summary>
    Any,

    /// <summary>
    /// SchemaRule attributes.
    /// </summary>
    SchemaRules,

    /// <summary>
    /// Design attribute.
    /// </summary>
    Design,

    /// <summary>
    /// Item attribute.
    /// </summary>
    Items,

    /// <summary>
    /// Property attribute.
    /// </summary>
    Properties
}
