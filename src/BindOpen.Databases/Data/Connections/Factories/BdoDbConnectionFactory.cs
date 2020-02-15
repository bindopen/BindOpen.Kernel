using BindOpen.Application.Scopes;
using BindOpen.Data.Stores;
using BindOpen.System.Diagnostics;

namespace BindOpen.Data.Connections
{
    /// <summary>
    /// This class represents a connection service.
    /// </summary>
    public static class BdoDbConnectionFactory
    {
        // Create -------------------------------------

        /// <summary>
        /// Creates a connector.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="depot">The data source depot to consider.</param>
        /// <param name="dataSourceName">The data source name to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The connector definition name to consider.</param>
        /// <param name="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public static T Open<T>(
            this IBdoScope scope,
            IBdoDatasourceDepot depot,
            string dataSourceName,
            string connectorDefinitionUniqueId,
            IBdoLog log = null)
            where T : class, IBdoConnection
        {
            if (log == null) log = new BdoLog();

            if (depot == null)
                depot = scope?.DataStore?.Get<IBdoDatasourceDepot>();

            if (depot == null)
                log.AddError("Data source depot missing");
            else if (!depot.HasItem(dataSourceName))
                log.AddError("Data source '" + dataSourceName + "' missing in depot");
            else
                return scope.Open<T>(depot.GetItem(dataSourceName), connectorDefinitionUniqueId, log);

            return default;
        }

        /// <summary>
        /// Creates a connector.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="dataSourceName">The data source name to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The connector definition name to consider.</param>
        /// <param name="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public static T Open<T>(
            this IBdoScope scope,
            string dataSourceName,
            string connectorDefinitionUniqueId,
            IBdoLog log = null)
            where T : class, IBdoConnection
        {
            return scope.Open<T>(null, dataSourceName, connectorDefinitionUniqueId, log);
        }
    }
}