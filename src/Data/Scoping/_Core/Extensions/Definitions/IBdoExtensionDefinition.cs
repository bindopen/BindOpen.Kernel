using BindOpen.Data;
using BindOpen.Data.Schema;

namespace BindOpen.Scoping;

/// <summary>
/// Defines the metadata and structural blueprint for a BindOpen extension.
/// </summary>
public interface IBdoExtensionDefinition : IBdoDefinition, IGrouped
{
    /// <summary>
    /// Gets or sets the package definition associated with this extension.
    /// </summary>
    IBdoPackageDefinition PackageDefinition { get; set; }

    /// <summary>
    /// The unique name of this extension, typically following a naming convention (e.g., 'pluginName$extensionName').
    /// </summary> 
    string UniqueName { get; }

    /// <summary>
    /// Gets or sets the URI of the image or icon representing this extension in a UI.
    /// </summary>
    string ImageUri { get; set; }

    /// <summary>
    /// Indicates whether this extension's configuration can be modified at runtime.
    /// </summary>
    bool IsEditable { get; set; }

    /// <summary>
    /// Indicates whether this extension is indexed within the BindOpen discovery catalog.
    /// </summary>
    bool IsIndexed { get; set; }

    /// <summary>
    /// Gets or sets the ID of the library containing this extension.
    /// </summary>
    string LibraryId { get; set; }
}