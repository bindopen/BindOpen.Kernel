using BindOpen.Data.Helpers;

namespace BindOpen.Data.Meta;

/// <summary>
/// Provides a set of standard script tokens for accessing metadata properties.
/// These constants are used to build dynamic expressions that reference 
/// the current metadata context.
/// </summary>
public static class BdoMetaDataProperties
{
    /// <summary>
    /// Script token to access the name of the current metadata.
    /// </summary>
    public static readonly string Name = "$(this).(name)";

    /// <summary>
    /// Script token to access the primary value of the current metadata.
    /// </summary>
    public static readonly string Value = "$(this).value()";

    /// <summary>
    /// Script token to access the requirement level of the current metadata.
    /// </summary>
    public static readonly string RequirementLevel = "$(this).requirementLevel()";

    /// <summary>
    /// Script token to access the requirement level of the items within the current metadata.
    /// </summary>
    public static readonly string ItemRequirementLevel = "$(this).itemRequirementLevel()";

    /// <summary>
    /// Generates a script to access the value of a specific descendant property.
    /// </summary>
    /// <param name="name">The name of the property to target.</param>
    /// <returns>A script string formatted for the BindOpen interpreter.</returns>
    public static string Property(string name)
        => string.Format("$(this).descendant('{0}').value()", name.ToUnquoted());
}