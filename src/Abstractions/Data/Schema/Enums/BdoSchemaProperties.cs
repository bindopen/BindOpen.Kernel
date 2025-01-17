﻿namespace BindOpen.Data.Schema;

/// <summary>
/// This enumeration represents the possible meta data properties.
/// </summary>
public static class BdoSchemaProperties
{
    /// <summary>
    /// The name of this meta data.
    /// </summary>
    public static readonly string IsStatic = "$isStatic";

    /// <summary>
    /// The pattern empty value.
    /// </summary>
    public static readonly string IsAllocatable = "$isAllocatable";

    public static readonly string IsScriptParameter = "$scriptParameter";
}
