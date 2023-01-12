using BindOpen.Meta.Items;
using BindOpen.Meta.Stores;

namespace BindOpen.Meta.Stores
{
    /// <summary>
    /// This class represents a runtime data source extensions.
    /// </summary>
    public static class BdoRuntimeDatasourceExtensions
    {
        /// <summary>
        /// Adds the specified source.
        /// </summary>
        /// <param name="depot">The depot to consider.</param>
        /// <param name="datasource">The datasource to consider.</param>
        public static IBdoSourceDepot AddDatasource(
            this IBdoSourceDepot depot,
            BdoDatasource datasource)
        {
            depot?.Add(datasource);

            return depot;
        }
    }
}