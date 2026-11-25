using BindOpen.Data.Objects;

namespace BindOpen.Data.Stores;

/// <summary>
/// Defines a strongly-typed BindOpen depot.
/// This interface provides a managed collection of items of type T, while 
/// inheriting the assembly discovery and lazy-loading capabilities of a depot.
/// </summary>
/// <typeparam name="T">The type of referenced items managed by this depot.</typeparam>
public interface ITBdoDepot<T> : ITBdoSet<T>, IBdoDepot
    where T : IReferenced
{
    // This interface serves as the specialized "shelf" for specific 
    // objects like IBdoDatasource or IBdoSchema.
}