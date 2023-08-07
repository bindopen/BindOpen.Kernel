using BindOpen.System.Data.Meta;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a data item factory.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static partial class BdoData
    {
        /// <summary>
        /// Instantiates a new instance of the Datasource class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="kind">The kind of the data source to consider.</param>
        /// <param key="configs">The config items to consider.</param>
        public static BdoDatasource NewDatasource(
            DatasourceKind kind = DatasourceKind.Any,
            params IBdoMetaObject[] metas)
        {
            var datasource = new BdoDatasource();
            datasource
                .WithKind(kind)
                .With(metas);

            return datasource;
        }

        /// <summary>
        /// Instantiates a new instance of the Datasource class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="kind">The kind of the data source to consider.</param>
        /// <param key="configs">The config items to consider.</param>
        public static BdoDatasource NewDatasource(
            string name,
            DatasourceKind kind,
            params IBdoMetaObject[] metas)
        {
            var datasource = new BdoDatasource(name);
            datasource
                .WithKind(kind)
                .With(metas);

            return datasource;
        }
    }
}