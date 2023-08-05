using BindOpen.System.Data;
using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;
using BindOpen.System.Logging;

namespace BindOpen.System.Scoping
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
        private static IBdoConnector CreateConnector(
            this IBdoScope scope,
            IBdoMetaComposite metaSet,
            string definitionUniqueName,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            IBdoConnector connector = null;

            if (metaSet != null && scope?.Check(true, log: log) == true)
            {
                // we get the connector class reference

                IBdoConnectorDefinition definition = scope.ExtensionStore.GetDefinition<IBdoConnectorDefinition>(definitionUniqueName);
                if (definition == null)
                {
                    log?.AddEvent(EventKinds.Error,
                        "Could not retrieve the extension connector '" + definitionUniqueName + "' definitio in scope");
                }
                else
                {
                    // we intantiate the connector

                    object item = definition.RuntimeType.CreateInstance(log);

                    if ((connector = item as IBdoConnector) != null)
                    {
                        connector.DefinitionUniqueName = definition.UniqueName;
                        connector.UpdateFromMeta(metaSet, true, scope: scope, varSet: varSet);
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
        public static IBdoConnector CreateConnector(
            this IBdoScope scope,
            IBdoMetaObject meta,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var definitionUniqueName = meta.DataType.ClassReference?.DefinitionUniqueName;

            var connector = scope.CreateConnector(meta, definitionUniqueName, varSet, log);

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
            IBdoMetaComposite metaSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : class, IBdoConnector, new()
        {
            var extensionDefinition = scope.ExtensionStore?.GetDefinitionFromType(
                BdoExtensionKind.Connector,
                BdoData.Class(typeof(T)));

            var connector = scope.CreateConnector(metaSet, extensionDefinition?.UniqueName, varSet, log) as T;

            return connector;
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
            IBdoMetaObject obj,
            IBdoLog log = null)
            where T : class, IBdoConnection
        {
            if (obj == null)
            {
                log?.AddEvent(EventKinds.Error, "Connection missing");
            }
            else if (scope?.Check(true, log: log) == true)
            {
                var connector = scope.CreateConnector(obj, log: log);

                if (connector != null)
                {
                    var connection = connector.NewConnection(log) as T;
                    connection.Connect(log);

                    return connection;
                }
            }

            return default;
        }

        /// <summary>
        /// Creates a new literal exp into auto mode.
        /// </summary>
        /// <param key="text">The script text to consider.</param>
        /// <returns>Returns the script exp.</returns>
        public static T WithConnectionString<T>(
            this T metaSet,
            string connectionString)
            where T : IBdoMetaComposite
        {
            metaSet?.Add(("connectionString", connectionString));
            return metaSet;
        }

        /// <summary>
        /// Creates a new literal exp into auto mode.
        /// </summary>
        /// <param key="text">The script text to consider.</param>
        /// <returns>Returns the script exp.</returns>
        public static string GetConnectionString<T>(
            this T metaSet)
            where T : IBdoMetaComposite
        {
            return metaSet?.GetData<string>("connectionString");
        }
    }
}
