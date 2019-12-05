using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items.Datasources;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Connections
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
            where T : IBdoConnection, new()
        {
            if (log == null) log = new BdoLog();

            if (dataSource == null)
                log.AddError("Data source missing");
            //else if (string.IsNullOrEmpty(connectorDefinitionUniqueId))
            //    log.AddError("Connection definition missing");
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
            where T : IBdoConnection, new()
        {
            IBdoLog subLog = new BdoLog();

            T connection = default;
            if (configuration == null)
            {
                subLog.AddError("Connection missing");
            }
            else if (scope != null && !subLog.Append(scope.Check(true)).HasErrorsOrExceptions())
            {
                connection = new T();
                connection.SetConnector(scope.CreateConnector(configuration, null, subLog));

                if (connection.Connector == null)
                {
                    connection = default;
                }
                else
                {
                    subLog.Append(connection.Open());
                }
            }

            (log ?? (log = new BdoLog())).Append(subLog);

            return connection;
        }

        // Close -------------------------------------

        /// <summary>
        /// Closes the specified connector.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        /// <returns>Returns the log of execution.</returns>
        public static IBdoLog Close(this IBdoConnection connector)
        {
            IBdoLog log = new BdoLog();

            connector?.Close();

            return log;
        }
    }
}
