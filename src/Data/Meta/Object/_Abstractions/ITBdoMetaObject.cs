using BindOpen.Logging;
using BindOpen.Scoping;

namespace BindOpen.Data.Meta;

/// <summary>
/// Defines a strongly-typed BindOpen metadata object.
/// This interface provides methods to synchronize metadata with a specific 
/// C# object instance.
/// </summary>
/// <typeparam name="TItem">The type of the object mapped by this metadata.</typeparam>
public interface ITBdoMetaObject<TItem> : IBdoMetaObject
{
    /// <summary>
    /// Sets the internal data of this instance by extracting metadata 
    /// from the specified object.
    /// </summary>
    /// <param name="obj">The object to extract data from.</param>
    void SetData(TItem obj);

    /// <summary>
    /// Retrieves the concrete object represented by this metadata.
    /// </summary>
    /// <param name="scope">The scope to use for resolution.</param>
    /// <param name="varSet">The set of variables to use for resolution.</param>
    /// <param name="log">The log to use for tracking the data retrieval process.</param>
    /// <returns>Returns the hydrated object of type TItem.</returns>
    new TItem GetData(
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);
}