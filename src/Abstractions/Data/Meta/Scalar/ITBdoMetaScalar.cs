using BindOpen.Logging;
using BindOpen.Scoping;

namespace BindOpen.Data.Meta;

/// <summary>
/// Defines a strongly-typed metadata scalar.
/// This interface provides typed methods for setting and retrieving atomic values,
/// ensuring type safety during metadata hydration and extraction.
/// </summary>
/// <typeparam name="TItem">The type of the data item held by this scalar.</typeparam>
public interface ITBdoMetaScalar<TItem> : IBdoMetaScalar
{
    /// <summary>
    /// Sets the data item for this scalar.
    /// </summary>
    /// <param name="obj">The data item of type TItem to store.</param>
    void SetData(TItem obj);

    /// <summary>
    /// Retrieves the primary data item (at index 0).
    /// </summary>
    /// <param name="scope">The scope used for resolution.</param>
    /// <param name="varSet">The set of variables for resolution.</param>
    /// <param name="log">The BindOpen log used for tracking.</param>
    /// <returns>The data item of type TItem.</returns>
    new TItem GetData(
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Retrieves the data item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item.</param>
    /// <param name="scope">The scope used for resolution.</param>
    /// <param name="varSet">The set of variables for resolution.</param>
    /// <param name="log">The BindOpen log used for tracking.</param>
    /// <returns>The data item of type TItem at the specified index.</returns>
    new TItem GetData(
        int index,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);
}