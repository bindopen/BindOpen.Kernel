using BindOpen.Data;
using BindOpen.Data.Assemblies;
using System.Collections.Generic;

namespace BindOpen.Scoping;

/// <summary>
/// Defines a package manifest that encapsulates assemblies and extension definitions.
/// </summary>
/// <remarks>
/// This interface is used by the discovery engine to map logical plugins to physical 
/// assemblies and manage their respective dependencies.
/// </remarks>
public interface IBdoPackageDefinition : IReferenced,
    IIdentified, INamed,
    IBdoTitled, IBdoDescribed
{
    /// <summary>
    /// Gets or sets the name of the primary .NET assembly associated with this package.
    /// </summary>
    string AssemblyName { get; set; }

    /// <summary>
    /// Gets or sets the name of the file (e.g., .dll or .bdo) containing the package data.
    /// </summary>
    string FileName { get; set; }

    /// <summary>
    /// Gets or sets the short alias used to reference this package in script or configuration.
    /// </summary>
    string Alias { get; set; }

    /// <summary>
    /// Retrieves the root namespace defined for this package.
    /// </summary>
    /// <returns>Returns the root namespace string.</returns>
    string GetRootNamespace();

    /// <summary>
    /// Gets or sets the group name (e.g., organization or domain) this package belongs to.
    /// </summary>
    string GroupName { get; set; }

    /// <summary>
    /// Initializes the package, preparing internal dictionaries and assembly resolution.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Gets or sets a dictionary mapping full names to item indices for fast lookup.
    /// </summary>
    ITBdoDictionary<string> ItemIndexFullNameDictionary { get; set; }

    /// <summary>
    /// Gets or sets the primary root namespace of the package's code.
    /// </summary>
    string RootNamespace { get; set; }

    /// <summary>
    /// Gets or sets the list of assembly references required by this package.
    /// </summary>
    IList<IBdoAssemblyReference> UsingAssemblyReferences { get; set; }
}