using BindOpen.System.Data.Meta;
using BindOpen.System.Data;
using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;
using BindOpen.System.Logging;
using System;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// This class represents an application 
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
            IBdoDataType dataType,
            IBdoMetaSet metaSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            IBdoConnector connector = null;

            if (metaSet != null && scope?.Check(true, log: log) == true)
            {
                Type type = dataType?.GetRuntimeType(scope, log);

                // we get the connector class reference

                if (type != null)
                {
                    object item = type.CreateInstance(log);

                    if (log?.HasEvent(EventKinds.Error, EventKinds.Exception) != false)
                    {
                        if ((connector = item as IBdoConnector) != null)
                        {
                            connector.DefinitionUniqueName = dataType?.DefinitionUniqueName;
                            connector.DefinitionUniqueName ??= scope.ExtensionStore?.GetDefinitionFromType(type)?.UniqueName;

                            connector.UpdateFromMeta(metaSet, true, scope: scope, varSet: varSet);
                        }
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
            var connector = scope.CreateConnector(meta?.DataType, meta, varSet, log);

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
            IBdoMetaSet metaSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : class, IBdoConnector, new()
        {
            var dataType = BdoData.NewDataType<T>();

            var connector = scope.CreateConnector(dataType, metaSet, varSet, log) as T;

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
            string definitionUniqueName,
            IBdoMetaSet metaSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var dataType = BdoData.NewDataType(BdoExtensionKinds.Connector, definitionUniqueName);

            var connector = scope.CreateConnector(dataType, metaSet, varSet, log);

            return connector;
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
            IBdoMetaObject meta,
            IBdoLog log = null)
            where T : class, IBdoConnection
        {
            if (meta == null)
            {
                log?.AddEvent(EventKinds.Error, "Connection missing");
            }
            else if (scope?.Check(true, log: log) == true)
            {
                var connector = scope.CreateConnector(meta, log: log);

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
            where T : IBdoMetaSet
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
            where T : IBdoMetaSet
        {
            return metaSet?.GetData<string>("connectionString");
        }
    }
}
