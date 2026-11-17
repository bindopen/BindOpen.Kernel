using BindOpen.Data;
using System.Collections.Generic;

namespace BindOpen.Scoping;

/// <summary>
/// Defines a dictionary that catalogs extensions and organizes them into groups.
/// </summary>
/// <remarks>
/// This interface provides the metadata and structural collection necessary 
/// for the discovery and categorization of extensions within a specific library.
/// </remarks>
public interface IBdoExtensionDictionary :
    IIdentified, INamed,
    IBdoTitled, IBdoDescribed,
    IDated
{
    /// <summary>
    /// Gets or sets the list of extension groups contained in this dictionary.
    /// </summary>
    IList<IBdoExtensionGroup> Groups { get; set; }

    /// <summary>
    /// Gets or sets the unique ID of the library associated with this dictionary.
    /// </summary>
    string LibraryId { get; set; }

    /// <summary>
    /// Gets or sets the display name of the library associated with this dictionary.
    /// </summary>
    string LibraryName { get; set; }
}