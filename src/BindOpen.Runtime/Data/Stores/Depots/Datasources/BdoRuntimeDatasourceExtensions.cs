using BindOpen.Application.Scopes;
using BindOpen.Data.Items;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents a runtime data source extensions.
    /// </summary>
    public static class BdoRuntimeDatasourceExtensions
    {
        /// <summary>
        /// Adds sources from BindOpen configuration.
        /// </summary>
        /// <param name="depot">The depot to consider.</param>
        /// <param name="options">The host options to consider.</param>
        public static IBdoDatasourceDepot AddFromConfiguration(this IBdoDatasourceDepot depot, IBdoHostOptions options)
        {
            if (options?.AppSettings?.AppConfiguration?.Datasources != null)
            {
                foreach (var dataSource in options?.AppSettings?.AppConfiguration?.Datasources)
                {
                    depot?.Add(dataSource);
                }
            }

            return depot;
        }

        /// <summary>
        /// Adds the specified source.
        /// </summary>
        /// <param name="depot">The depot to consider.</param>
        /// <param name="datasource">The datasource to consider.</param>
        public static IBdoDatasourceDepot AddDatasource(
            this IBdoDatasourceDepot depot,
            Datasource datasource)
        {
            depot?.Add(datasource);

            return depot;
        }
    }
}