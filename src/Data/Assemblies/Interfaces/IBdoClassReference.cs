using BindOpen.Logging;
using BindOpen.Scoping;
using System;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// Defines a reference to a specific C# class within an assembly.
/// This interface is used to dynamically resolve and instantiate types at runtime.
/// </summary>
public interface IBdoClassReference : IBdoAssemblyReference
{
    /// <summary>
    /// Gets or sets the full name of the class (including namespace).
    /// </summary>
    string ClassName { get; set; }

    /// <summary>
    /// Resolves and returns the actual .NET Type of the referenced class.
    /// </summary>
    /// <param name="scope">The scope to use for assembly resolution.</param>
    /// <param name="log">The log to record any resolution errors (e.g., TypeNotFound).</param>
    /// <returns>Returns the resolved Type if found; otherwise, null.</returns>
    Type GetRuntimeType(IBdoScope scope = null, IBdoLog log = null);

    /// <summary>
    /// Determines if the referenced class is explicitly specified/defined enough 
    /// to be used for data transfer or instantiation.
    /// </summary>
    /// <returns>True if the class reference is fully specified; otherwise, false.</returns>
    bool IsSpecified();
}