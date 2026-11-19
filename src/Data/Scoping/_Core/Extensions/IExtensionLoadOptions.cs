using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Objects.Interfaces;
using System.Collections.Generic;

namespace BindOpen.Scoping;

/// <summary>
/// This interface defines the options used to load BindOpen extensions.
/// </summary>
/// <remarks>
/// These options allow for granular control over which directories are scanned,
/// which assembly references are prioritized, and which types of extensions are indexed.
/// </remarks>
public interface IExtensionLoadOptions : IBdoObject
{
    /// <summary>
    /// Gets or sets the list of sources where extensions can be discovered.
    /// </summary>
    /// <value>A list of tuples containing the datasource kind (e.g., Folder, Repository) and the URI.</value>
    IList<(DatasourceKind Kind, string Uri)> Sources { get; set; }

    /// <summary>
    /// Gets or sets the list of extension kinds to specifically load.
    /// </summary>
    /// <value>If empty, all discovered kinds will be loaded by default.</value>
    IList<BdoExtensionKinds> ExtensionKinds { get; set; }

    /// <summary>
    /// Gets or sets the list of explicit assembly references to be loaded.
    /// </summary>
    IList<IBdoAssemblyReference> References { get; set; }
}