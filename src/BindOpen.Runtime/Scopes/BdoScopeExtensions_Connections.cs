using BindOpen.Data.Configuration;
using BindOpen.Data.Items;
using BindOpen.Extensions.Connecting;
using BindOpen.Logging;

namespace BindOpen.Runtime.Scopes
{
    /// <summary>
    /// This class represents a connection service.
    /// </summary>
    public static partial class BdoScopeExtensions
    {
        /// <summary>
        /// Creates a connector.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="dataSource">The data source to consider.</param>
        /// <param key="connectorDefinitionUniqueName">The connector definition name to consider.</param>
        /// <param key="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public static T Open<T>(
            this IBdoScope scope,
            IBdoDatasource dataSource,
            string connectorDefinitionUniqueName,
            IBdoLog log = null)
            where T : class, IBdoConnection
        {
            if (dataSource == null)
                log?.AddError("Data source missing");
            else if (!string.IsNullOrEmpty(connectorDefinitionUniqueName) && dataSource.Has(connectorDefinitionUniqueName))
                log?.AddError("Connection not defined in data source", description: "No connector is defined in the specified data source.");
            else if (!string.IsNullOrEmpty(connectorDefinitionUniqueName))
                return scope.Open<T>(dataSource.Get(connectorDefinitionUniqueName), log);
            else if (dataSource.Count > 0)
                return scope.Open<T>(dataSource[0], log);

            return default;
        }

        /// <summary>
        /// Creates a connector using the specified data module and connector unique name.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The connector config to consider.</param>
        /// <param key="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public static T Open<T>(
            this IBdoScope scope,
            IBdoConfiguration config,
            IBdoLog log = null)
            where T : class, IBdoConnection
        {
            if (config == null)
            {
                log?.AddError("Connection missing");
            }
            else if (scope?.Check(true, log: log) == true)
            {
                var connector = scope.CreateConnector(config, log: log);

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
