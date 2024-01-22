/// <summary>
/// This enumerates the possible levels of accesibility.
/// </summary>
export enum AccessibilityLevels
{
    /// <summary>
    /// Anyone can access.
    /// </summary>
    Public = "Public",

    /// <summary>
    /// None.
    /// </summary>
    None = "None",

    /// <summary>
    /// Inherited.
    /// </summary>
    Inherited = "Inherited",

    /// <summary>
    /// Only authentized people can access.
    /// </summary>
    Private = "Private",

    /// <summary>
    /// Only allowed people can access.
    /// </summary>
    Restricted = "Restricted",

    /// <summary>
    /// Any accessibility level.
    /// </summary>
    Any = "Any"
}
