using System;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// This interface defines an assembly reference.
/// </summary>
public interface IBdoAssemblyReference : IBdoObject, IReferenced, IIdentified
{
    /// <summary>
    /// The library name.
    /// </summary>
    string AssemblyName { get; set; }

    /// <summary>
    /// The library version.
    /// </summary>
    Version AssemblyVersion { get; set; }

    /// <summary>
    /// The file name.
    /// </summary>
    string AssemblyFileName { get; set; }

    /// <summary>
    /// Indicates whether this object is empty.
    /// </summary>
    /// <returns>Returns True whether it is empty.</returns>
    bool IsEmpty();
}