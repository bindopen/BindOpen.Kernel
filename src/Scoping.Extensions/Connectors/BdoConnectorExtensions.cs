using BindOpen.System.Data;
using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;
using BindOpen.System.Logging;

namespace BindOpen.System.Scoping.Connectors
{
    /// <summary>
    /// This class represents a connection service.
    /// </summary>
    public static partial class BdoConnectorExtensions
    {
        // Create

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the created connector.</returns>
        public static IBdoConnector CreateConnector(
            this IBdoScope scope,
            IBdoConfiguration config,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            IBdoConnector connector = null;

            if (config != null && scope?.Check(true, log: log) == true)
            {
                // we get the connector class reference

                IBdoConnectorDefinition definition = scope.ExtensionStore.GetDefinition<IBdoConnectorDefinition>(config?.DefinitionUniqueName);
                if (definition == null)
                {
                    log?.AddEvent(EventKinds.Error,
                        "Could not retrieve the extension connector '" + config.DefinitionUniqueName + "' definitio in scope");
                }
                else
                {
                    // we intantiate the connector
                    object item = definition.RuntimeType.CreateInstance(log);

                    if ((connector = item as IBdoConnector) != null)
                    {
                        connector.DefinitionUniqueName = definition.UniqueName;
                        connector.UpdateFromMeta(config, true, scope: scope, varSet: varSet);
                    }
                }
            }

            return connector;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <returns>Returns the created connector.</returns>
        public static T CreateConnector<T>(
            this IBdoScope scope,
            IBdoConfiguration config,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : class, IBdoConnector, new()
        {
            return scope.CreateConnector(config, varSet, log) as T;
        }

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
                log?.AddEvent(EventKinds.Error, "Data source missing");
            else if (!string.IsNullOrEmpty(connectorDefinitionUniqueName) && dataSource.Has(connectorDefinitionUniqueName))
                log?.AddEvent(EventKinds.Error,
                    "Connection not defined in data source", description: "No connector is defined in the specified data source.");
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
                log?.AddEvent(EventKinds.Error, "Connection missing");
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
