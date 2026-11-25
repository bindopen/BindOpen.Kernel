/// <summary>
/// This enumeration lists the possible event kinds.
/// </summary>
export enum EventKinds
{
    /// <summary>
    /// None.
    /// </summary>
    None = 0,

    /// <summary>
    /// Message.
    /// </summary>
    Message = 1,

    /// <summary>
    /// Checkpoint.
    /// </summary>
    Checkpoint = 2,

    /// <summary>
    /// Warning.
    /// </summary>
    Warning = 4,

    /// <summary>
    /// Error.
    /// </summary>
    Error = 8,

    /// <summary>
    /// Exception.
    /// </summary>
    Exception = 16,

    /// <summary>
    /// Other.
    /// </summary>
    Other = 32,

    /// <summary>
    /// None.
    /// </summary>
    Any = 62
}
