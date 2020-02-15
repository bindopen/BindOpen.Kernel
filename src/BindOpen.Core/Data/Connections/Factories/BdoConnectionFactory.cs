using BindOpen.Application.Scopes;
using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;

namespace BindOpen.Data.Connections
{
    /// <summary>
    /// This class represents a connection service.
    /// </summary>
    public static class BdoConnectionFactory
    {
        // Create -------------------------------------

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
            IDatasource dataSource,
            string connectorDefinitionUniqueId,
            IBdoLog log = null)
            where T : class, IBdoConnection
        {
            if (log == null) log = new BdoLog();

            if (dataSource == null)
                log.AddError("Data source missing");
            else if (!string.IsNullOrEmpty(connectorDefinitionUniqueId) && dataSource.HasConfiguration(connectorDefinitionUniqueId))
                log.AddError("Connection not defined in data source", description: "No connector is defined in the specified data source.");
            else if (!string.IsNullOrEmpty(connectorDefinitionUniqueId))
                return scope.Open<T>(dataSource.GetConfiguration(connectorDefinitionUniqueId), log);
            else if (dataSource.Configurations.Count > 0)
                return scope.Open<T>(dataSource.Configurations[0], log);

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
            T connection = default;
            if (configuration == null)
            {
                log?.AddError("Connection missing");
            }
            else if (scope != null && !scope.Check(true).AddEventsTo(log).HasErrorsOrExceptions())
            {
                var connector = scope.CreateConnector(configuration, null, log);

                if (connector == null)
                {
                    connection = default;
                }
                else
                {
                    connection = connector.CreateConnection(log) as T;
                    connection.Connect().AddEventsTo(log);
                }
            }

            return connection;
        }
    }
}
