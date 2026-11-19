using BindOpen.Data.Meta;
using System;
using System.Linq;

namespace BindOpen.Scoping.Script;

/// <summary>
/// Provides standard text and string manipulation functions for the scripting engine.
/// </summary>
public static partial class FunctionStore
{
    /// <summary>
    /// Converts the specified object into its string representation.
    /// </summary>
    /// <param name="obj1">The object to convert.</param>
    /// <returns>Returns the string representation of the object, or null if the object is null.</returns>
    [BdoFunction(Name = "text")]
    public static string Text([BdoParameter(Title = "Object")] object obj1)
        => obj1?.ToString();

    /// <summary>
    /// Concatenates the string representations of a list of objects.
    /// </summary>
    /// <param name="objs">The objects to concatenate.</param>
    /// <returns>Returns a single concatenated string.</returns>
    /// <remarks>
    /// This function iterates through all provided arguments, converts them to strings, 
    /// and joins them without separators.
    /// </remarks>
    [BdoFunction(Name = "concat")]
    public static string Concat([BdoParameter(Title = "Objects")] params object[] objs)
    {
        if (objs == null) return string.Empty;

        // Converts each object to string (handling nulls as empty) and concatenates
        return string.Concat(objs.Select(q => q?.ToString() ?? string.Empty));
    }
}