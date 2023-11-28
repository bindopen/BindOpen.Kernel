/// <summary>
/// This enumeration represents the possible persistence levels.
/// </summary>
export enum PersistenceLevels
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
    /// The information remains alive.
    /// </summary>
    Singleton,

    /// <summary>
    /// The information remains alive until the session ends.
    /// </summary>
    Scoped,

    /// <summary>
    /// The information remains alive until the request ends.
    /// </summary>
    Transient,

    /// <summary>
    /// Any the persistence level.
    /// </summary>
    Any
}
