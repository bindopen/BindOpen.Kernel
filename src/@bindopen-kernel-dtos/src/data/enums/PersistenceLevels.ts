/// <summary>
/// This enumeration represents the possible persistence levels.
/// </summary>
export enum PersistenceLevels
{
    /// <summary>
    /// None.
    /// </summary>
    None = "None",

    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined = "Undefined",

    /// <summary>
    /// The information remains alive.
    /// </summary>
    Singleton = "Singleton",

    /// <summary>
    /// The information remains alive until the session ends.
    /// </summary>
    Scoped = "Scoped",

    /// <summary>
    /// The information remains alive until the request ends.
    /// </summary>
    Transient = "Transient",

    /// <summary>
    /// Any the persistence level.
    /// </summary>
    Any = "Any"
}
