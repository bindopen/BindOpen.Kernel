using BindOpen.Data.Objects.Interfaces;
using BindOpen.Logging;
using BindOpen.Scoping;

namespace BindOpen.Data.Meta;

/// <summary>
/// Defines a BindOpen metadata set.
/// A meta-set is a collection of metadata elements that itself acts as a 
/// metadata element, allowing for hierarchical and recursive data structures.
/// </summary>
public partial interface IBdoMetaSet : ITBdoSet<IBdoMetaData>, IBdoMetaData, INamed, IBdoScoped
{
    /// <summary>
    /// Retrieves the data at the specified index, resolving any dynamic content.
    /// </summary>
    /// <param name="index">The zero-based index of the item to retrieve.</param>
    /// <param name="scope">The scope to use for resolution.</param>
    /// <param name="varSet">The set of variables to use for resolution.</param>
    /// <param name="log">The BindOpen log used for tracking.</param>
    /// <returns>The resolved data object at the specified index.</returns>
    object GetData(
        int index,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Retrieves and casts the data at the specified index, resolving any dynamic content.
    /// </summary>
    /// <typeparam name="Q">The type to cast the data to.</typeparam>
    /// <param name="index">The zero-based index of the item to retrieve.</param>
    /// <param name="scope">The scope to use for resolution.</param>
    /// <param name="varSet">The set of variables to use for resolution.</param>
    /// <param name="log">The BindOpen log used for tracking.</param>
    /// <returns>The resolved data at the specified index cast to Q.</returns>
    Q GetData<Q>(
        int index,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);
}