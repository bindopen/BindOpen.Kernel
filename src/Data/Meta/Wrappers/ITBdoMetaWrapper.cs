namespace BindOpen.Data.Meta;

/// <summary>
/// Defines a strongly-typed metadata wrapper.
/// This interface provides a specialized detail property, allowing for 
/// type-safe management of the underlying metadata set.
/// </summary>
/// <typeparam name="TDetail">The type of metadata set used for the detail.</typeparam>
public interface ITBdoMetaWrapper<TDetail> : IBdoMetaWrapper
    where TDetail : IBdoMetaSet
{
    /// <summary>
    /// Gets or sets the specialized metadata set representing the detail of this instance.
    /// </summary>
    /// <remarks>
    /// This property shadows the base IBdoDetailed.Detail to provide the specific 
    /// TDetail type.
    /// </remarks>
    new TDetail Detail { get; set; }
}