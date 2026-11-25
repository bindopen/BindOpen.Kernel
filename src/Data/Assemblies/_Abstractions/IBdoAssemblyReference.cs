using BindOpen.Data.Objects;
using System;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// Defines a reference to a .NET assembly within the BindOpen framework.
/// This interface facilitates dynamic assembly loading and version management.
/// </summary>
public interface IBdoAssemblyReference : IBdoObject, IReferenced, IIdentified
{
    /// <summary>
    /// Gets or sets the simple or full name of the assembly.
    /// </summary>
    string AssemblyName { get; set; }

    /// <summary>
    /// Gets or sets the version of the assembly to be referenced.
    /// </summary>
    Version AssemblyVersion { get; set; }

    /// <summary>
    /// Gets or sets the physical file name or path of the assembly.
    /// </summary>
    string AssemblyFileName { get; set; }

    /// <summary>
    /// Gets or sets the alias for this assembly, used for simplified referencing in scripts or metadata.
    /// </summary>
    string Alias { get; set; }

    /// <summary>
    /// Determines whether the reference contains sufficient information to be resolved.
    /// </summary>
    /// <returns>True if the reference is considered empty; otherwise, false.</returns>
    bool IsEmpty();
}