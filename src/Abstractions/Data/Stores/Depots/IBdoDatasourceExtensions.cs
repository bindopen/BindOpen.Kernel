namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents an data source extensions.
    /// </summary>
    public static class IBdoDatasourceExtensions
    {
        public static IBdoDatasource WithDatasourceKind(this IBdoDatasource datasource, DatasourceKind datasourceKind)
        {
            if (datasource != null)
            {
                datasource.DatasourceKind = datasourceKind;
            }

            return datasource;
        }

        public static IBdoDatasource WithConnectionString(this IBdoDatasource datasource, string connectionString)
        {
            if (datasource != null)
            {
                datasource.ConnectionString = connectionString;
            }

            return datasource;
        }
    }
}