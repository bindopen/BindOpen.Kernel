using BindOpen.Extensions.Connecting;
using BindOpen.Meta.Items;
using BindOpen.Meta.Stores;

namespace BindOpen.Meta
{
    /// <summary>
    /// This class represents a data item factory.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Instantiates a new instance of the Datasource class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="kind">The kind of the data source to consider.</param>
        /// <param name="configs">The configuration items to consider.</param>
        public static BdoDatasource NewDatasource(
            DatasourceKind kind = DatasourceKind.Any,
            params IBdoConnectorConfiguration[] configs)
        {
            var datasource = new BdoDatasource();
            datasource
                .WithKind(kind)
                .WithConfiguration(configs);

            return datasource;
        }

        /// <summary>
        /// Instantiates a new instance of the Datasource class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="kind">The kind of the data source to consider.</param>
        /// <param name="configs">The configuration items to consider.</param>
        public static BdoDatasource NewDatasource(
            string name,
            DatasourceKind kind,
            params IBdoConnectorConfiguration[] configs)
        {
            var datasource = new BdoDatasource(name);
            datasource
                .WithKind(kind)
                .WithConfiguration(configs);

            return datasource;
        }

        /// <summary>
        /// Instantiates a new instance of the Datasource class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="kind">The kind of the data source to consider.</param>
        public static IBdoSourceDepot NewDatasourceDepot(params IBdoDataSource[] datasources)
            => BdoMeta.NewItemSet<BdoDatasourceDepot, IBdoDataSource>(datasources);
    }
}
