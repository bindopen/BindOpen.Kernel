namespace BindOpen.Data.Stores;

/// <summary>
/// Provides extension methods for IBdoDatasource to facilitate fluent configuration 
/// of data source properties.
/// </summary>
public static class IBdoDatasourceExtensions
{
    /// <summary>
    /// Sets the kind of data source and returns the instance for fluent chaining.
    /// </summary>
    /// <param name="datasource">The data source to update.</param>
    /// <param name="datasourceKind">The kind of data source (e.g., Database, Rest, Email).</param>
    /// <returns>The updated data source instance.</returns>
    public static IBdoDatasource WithDatasourceKind(
        this IBdoDatasource datasource,
        DatasourceKind datasourceKind)
    {
        if (datasource != null)
        {
            datasource.DatasourceKind = datasourceKind;
        }

        return datasource;
    }

    /// <summary>
    /// Sets the connection string for the data source and returns the instance for fluent chaining.
    /// </summary>
    /// <param name="datasource">The data source to update.</param>
    /// <param name="connectionString">The connection string or resolution expression.</param>
    /// <returns>The updated data source instance.</returns>
    public static IBdoDatasource WithConnectionString(
        this IBdoDatasource datasource,
        string connectionString)
    {
        if (datasource != null)
        {
            datasource.ConnectionString = connectionString;
        }

        return datasource;
    }
}