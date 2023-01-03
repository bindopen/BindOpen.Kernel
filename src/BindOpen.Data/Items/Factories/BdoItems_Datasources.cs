using BindOpen.Extensions.Connecting;
using BindOpen.Data.Stores;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data item factory.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static partial class BdoItems
    {
        /// <summary>
        /// Instantiates a new instance of the Datasource class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="kind">The kind of the data source to consider.</param>
        /// <param name="configs">The configuration items to consider.</param>
        public static BdoSource NewDatasource(
            DatasourceKind kind = DatasourceKind.Any,
            params IBdoConnectorConfiguration[] configs)
        {
            var datasource = new BdoSource();
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
        public static BdoSource NewDatasource(
            string name,
            DatasourceKind kind,
            params IBdoConnectorConfiguration[] configs)
        {
            var datasource = new BdoSource(name);
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
        public static IBdoSourceDepot NewDatasourceDepot(params IBdoSource[] datasources)
            => BdoItems.NewSet<BdoDatasourceDepot, IBdoSource>(datasources);
    }
}
