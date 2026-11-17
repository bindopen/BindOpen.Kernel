namespace BindOpen.Data.Schema;

/// <summary>
/// Provides a set of standard property keys used within BindOpen schema definitions.
/// These constants define structural behaviors and flags for metadata elements.
/// </summary>
public static class BdoSchemaProperties
{
    /// <summary>
    /// Property key indicating whether the metadata element is static.
    /// Static elements typically do not change during the execution lifecycle.
    /// </summary>
    public static readonly string IsStatic = "$isStatic";

    /// <summary>
    /// Property key indicating whether the metadata element can be dynamically allocated.
    /// </summary>
    public static readonly string IsAllocatable = "$isAllocatable";

    /// <summary>
    /// Property key indicating whether the metadata element is specifically 
    /// intended to be treated as a script parameter.
    /// </summary>
    public static readonly string IsScriptParameter = "$scriptParameter";
}