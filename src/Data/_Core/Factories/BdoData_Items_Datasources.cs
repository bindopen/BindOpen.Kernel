using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Stores;

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
            params IBdoConfiguration[] configs)
        {
            var datasource = new BdoDatasource();
            datasource
                .WithKind(kind)
                .With(configs);

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
            params IBdoConfiguration[] configs)
        {
            var datasource = new BdoDatasource(name);
            datasource
                .WithKind(kind)
                .With(configs);

            return datasource;
        }

        /// <summary>
        /// Instantiates a new instance of the Datasource class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="kind">The kind of the data source to consider.</param>
        public static IBdoSourceDepot NewDatasourceDepot(
            params IBdoDatasource[] datasources)
            => BdoData.NewSet<BdoDatasourceDepot, IBdoDatasource>(datasources);
    }
}