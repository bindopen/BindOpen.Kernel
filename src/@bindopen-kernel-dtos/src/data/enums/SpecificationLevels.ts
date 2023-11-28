/// <summary>
/// This enumeration represents the possible levels of specification.
/// </summary>
export enum SpecificationLevels
{
    /// <summary>
    /// None.
    /// </summary>
    None,

    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Definition.
    /// </summary>
    Definition,

    /// <summary>
    /// Design.
    /// </summary>
    Design,

    /// <summary>
    /// Configuration.
    /// </summary>
    Configuration,

    /// <summary>
    /// Runtime.
    /// </summary>
    Runtime,

    /// <summary>
    /// Any specification level.
    /// </summary>
    Any = Definition | Design | Configuration | Runtime
}
