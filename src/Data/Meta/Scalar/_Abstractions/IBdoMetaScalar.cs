using BindOpen.Logging;
using BindOpen.Scoping;

namespace BindOpen.Data.Meta;

/// <summary>
/// Defines a BindOpen metadata scalar.
/// A meta-scalar holds one or more atomic values (e.g., strings, integers) 
/// and provides methods to retrieve them with scope-based resolution.
/// </summary>
public interface IBdoMetaScalar : IBdoMetaData
{
    /// <summary>
    /// Returns the number of items contained in this scalar.
    /// </summary>
    /// <param name="scope">The scope to use for resolution.</param>
    /// <param name="varSet">The set of variables to use for resolution.</param>
    /// <param name="log">The BindOpen log used for tracking.</param>
    /// <returns>The number of items.</returns>
    int Count(
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Retrieves the data at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to retrieve.</param>
    /// <param name="scope">The scope to use for resolution.</param>
    /// <param name="varSet">The set of variables to use for resolution.</param>
    /// <param name="log">The BindOpen log used for tracking.</param>
    /// <returns>The data object at the specified index.</returns>
    object GetData(
        int index,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Retrieves and casts the data at the specified index.
    /// </summary>
    /// <typeparam name="Q">The type to cast the data to.</typeparam>
    /// <param name="index">The zero-based index of the item to retrieve.</param>
    /// <param name="scope">The scope to use for resolution.</param>
    /// <param name="varSet">The set of variables to use for resolution.</param>
    /// <param name="log">The BindOpen log used for tracking.</param>
    /// <returns>The data at the specified index cast to Q.</returns>
    Q GetData<Q>(
        int index,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);
}