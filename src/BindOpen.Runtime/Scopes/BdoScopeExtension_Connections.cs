using BindOpen.MetaData.Items;
using BindOpen.Extensions.Connecting;
using BindOpen.Logging;

namespace BindOpen.Runtime.Scopes
{
    /// <summary>
    /// This class represents a connection service.
    /// </summary>
    public static partial class BdoScopeExtension
    {
        /// <summary>
        /// Creates a connector.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="dataSource">The data source to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The connector definition name to consider.</param>
        /// <param name="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public static T Open<T>(
            this IBdoScope scope,
            IBdoDataSource dataSource,
            string connectorDefinitionUniqueId,
            IBdoLog log = null)
            where T : class, IBdoConnection
        {
            if (dataSource == null)
                log?.AddError("Data source missing");
            else if (!string.IsNullOrEmpty(connectorDefinitionUniqueId) && dataSource.HasConfig(connectorDefinitionUniqueId))
                log?.AddError("Connection not defined in data source", description: "No connector is defined in the specified data source.");
            else if (!string.IsNullOrEmpty(connectorDefinitionUniqueId))
                return scope.Open<T>(dataSource.GetConfig(connectorDefinitionUniqueId), log);
            else if (dataSource.ConfigList.Count > 0)
                return scope.Open<T>(dataSource.ConfigList[0], log);

            return default;
        }

        /// <summary>
        /// Creates a connector using the specified data module and connector unique name.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The connector configuration to consider.</param>
        /// <param name="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public static T Open<T>(
            this IBdoScope scope,
            IBdoConnectorConfiguration configuration,
            IBdoLog log = null)
            where T : class, IBdoConnection
        {
            if (configuration == null)
            {
                log?.AddError("Connection missing");
            }
            else if (scope?.Check(true, log: log) == true)
            {
                var connector = scope.NewConnector(configuration, log: log);

                if (connector != null)
                {
                    var connection = connector.NewConnection(log) as T;
                    connection.Connect(log);

                    return connection;
                }
            }

            return default;
        }
    }
}
