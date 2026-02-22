using BindOpen.Logging;
using BindOpen.Scoping;
using System.Collections.Generic;

namespace BindOpen.Data.Meta;

/// <summary>
/// Provides advanced semantic retrieval methods for metadata sets.
/// This partial interface adds specialized accessors for objects, scalars, 
/// and composite sets.
/// </summary>
public partial interface IBdoMetaSet
{
    /// <summary>
    /// Retrieves the metadata object associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the object to retrieve.</param>
    /// <returns>The metadata object.</returns>
    public IBdoMetaObject Object(string key = null);

    /// <summary>
    /// Retrieves the metadata object at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the object.</param>
    /// <returns>The metadata object.</returns>
    IBdoMetaObject Object(int index);

    /// <summary>
    /// Retrieves the metadata scalar associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the scalar to retrieve.</param>
    /// <returns>The metadata scalar.</returns>
    IBdoMetaScalar Scalar(string key = null);

    /// <summary>
    /// Retrieves the metadata scalar at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the scalar.</param>
    /// <returns>The metadata scalar.</returns>
    IBdoMetaScalar Scalar(int index);

    /// <summary>
    /// Retrieves the metadata set (composite) associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the set to retrieve.</param>
    /// <returns>The metadata set.</returns>
    IBdoMetaSet Composite(string key = null);

    /// <summary>
    /// Retrieves the metadata set (composite) at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the set.</param>
    /// <returns>The metadata set.</returns>
    IBdoMetaSet Composite(int index);

    // --- Data Retrieval ---

    /// <summary>
    /// Retrieves a list of raw data values associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the data element.</param>
    /// <param name="scope">The resolution scope.</param>
    /// <param name="varSet">The variable set for resolution.</param>
    /// <param name="log">The log for tracking.</param>
    /// <returns>A list of resolved data objects.</returns>
    public IList<object> GetDataList(
        string key,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Retrieves a typed list of raw data values associated with the specified key.
    /// </summary>
    /// <typeparam name="Q">The expected data type.</typeparam>
    /// <param name="key">The key of the data element.</param>
    /// <param name="scope">The resolution scope.</param>
    /// <param name="varSet">The variable set for resolution.</param>
    /// <param name="log">The log for tracking.</param>
    /// <returns>A list of resolved data items of type Q.</returns>
    public IList<Q> GetDataList<Q>(
        string key,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Retrieves the raw data value associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the data element.</param>
    /// <param name="scope">The resolution scope.</param>
    /// <param name="varSet">The variable set for resolution.</param>
    /// <param name="log">The log for tracking.</param>
    /// <returns>The resolved data object.</returns>
    public object GetData(
        string key,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Retrieves the typed raw data value associated with the specified key.
    /// </summary>
    /// <typeparam name="Q">The expected data type.</typeparam>
    /// <param name="key">The key of the data element.</param>
    /// <param name="scope">The resolution scope.</param>
    /// <param name="varSet">The variable set for resolution.</param>
    /// <param name="log">The log for tracking.</param>
    /// <returns>The resolved data item of type Q.</returns>
    public Q GetData<Q>(
        string key,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);
}