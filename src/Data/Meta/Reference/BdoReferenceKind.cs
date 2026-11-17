namespace BindOpen.Data;

/// <summary>
/// Specifies the various kinds of references used within the BindOpen framework.
/// This determines the logic used to resolve a target value or object.
/// </summary>
public enum BdoReferenceKind
{
    /// <summary>
    /// Any kind of reference. Usually used for broad filtering or discovery.
    /// </summary>
    Any,

    /// <summary>
    /// A reference based on a dynamic expression (e.g., a script or a formula).
    /// </summary>
    Expression,

    /// <summary>
    /// A reference based on a specific target identifier (e.g., a GUID or a unique name).
    /// </summary>
    Identifier,

    /// <summary>
    /// A direct reference to a metadata element or structure.
    /// </summary>
    MetaData,

    /// <summary>
    /// No specific reference kind. Often used for atomic values that do not point elsewhere.
    /// </summary>
    None
}