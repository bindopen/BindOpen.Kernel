/// <summary>
/// This enumeration represents the possible update modes.
/// </summary>
export enum UpdateModes
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
    /// Adds Source items that are not in Target.
    /// </summary>
    /// <remarks>Used in incremental update.</remarks>
    Incremental_AddMissingInTarget = "Incremental_AddMissingInTarget",

    /// <summary>
    /// Removes Target items that are not in Source.
    /// </summary>
    /// <remarks>Used in incremental update.</remarks>
    Incremental_RemoveMissingInSource = "Incremental_RemoveMissingInSource",

    /// <summary>
    /// Updates Target items that are in Source.
    /// </summary>
    /// <remarks>Used in incremental update.</remarks>
    Incremental_UpdateCommon = "Incremental_UpdateCommon",

    /// <summary>
    /// Full update mode.
    /// </summary>
    /// <remarks>Used in full update.</remarks>
    Full = "Full"
}
