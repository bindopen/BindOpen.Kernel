using BindOpen.Data;
using System.Collections.Generic;

namespace BindOpen.Scoping;

/// <summary>
/// Defines a group used to categorize extensions within a dictionary.
/// </summary>
/// <remarks>
/// This interface supports recursive grouping, allowing for complex 
/// hierarchical taxonomies of extensions.
/// </remarks>
public interface IBdoExtensionGroup :
    IIdentified, INamed,
    IBdoTitled, IBdoDescribed,
    IDated
{
    /// <summary>
    /// Gets the list of sub-groups contained within this group.
    /// </summary>
    IList<IBdoExtensionGroup> SubGroups { get; }

    /// <summary>
    /// Retrieves a sub-group by its name.
    /// </summary>
    /// <param name="name">The name of the sub-group to find.</param>
    /// <returns>Returns the matching sub-group if found; otherwise, null.</returns>
    IBdoExtensionGroup GetGroupWithName(string name);
}