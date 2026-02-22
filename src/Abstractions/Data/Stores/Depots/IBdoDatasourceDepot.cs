namespace BindOpen.Data.Stores;

/// <summary>
/// Defines a BindOpen datasource depot.
/// A datasource depot is a specialized container for managing IBdoDatasource objects,
/// allowing the system to centralize and retrieve connection settings for various
/// external data systems.
/// </summary>
public interface IBdoDatasourceDepot : ITBdoDepot<IBdoDatasource>
{
    // This interface serves as the primary registry for connection strings
    // and data source configurations within a BindOpen scope.
}