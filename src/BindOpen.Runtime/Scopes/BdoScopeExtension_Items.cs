using BindOpen.Data;
using BindOpen.Data.Configuration;
using BindOpen.Data.Meta;
using BindOpen.Extensions;
using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Extensions.Processing;
using BindOpen.Logging;
using BindOpen.Runtime.Definition;

namespace BindOpen.Runtime.Scopes
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static partial class BdoScopeExtension
    {
        // Entities ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <typeparam name="T">The entity class to return.</typeparam>
        /// <returns>Returns the created entity.</returns>
        public static T NewEntity<T>(
            this IBdoScope scope,
            IBdoConfiguration config = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : BdoEntity
        {
            return scope.NewEntity(config, varSet, log) as T;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the created entity.</returns>
        public static BdoEntity NewEntity(
            this IBdoScope scope,
            IBdoConfiguration config,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            BdoEntity entity = null;

            if (config != null && scope?.Check(true, log: log) == false)
            {
                // we get the entity class reference

                IBdoEntityDefinition definition = scope.ExtensionStore.GetItemDefinitionWithUniqueId<IBdoEntityDefinition>(config.DefinitionUniqueId);
                if (definition == null)
                {
                    log?.AddError("Could not retrieve the extension entity '" + config.DefinitionUniqueId + "' definition");
                }
                else
                {
                    // we intantiate the entity

                    AssemblyHelper.CreateInstance(definition.RuntimeType, out object item, log);

                    if (item != null)
                    {
                        entity = item as BdoEntity;
                        entity.UpdateFromMeta(config, true, scope, varSet);
                    }
                }
            }

            return entity;
        }

        // Connectors ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <returns>Returns the created connector.</returns>
        public static T NewConnector<T>(
            this IBdoScope scope,
            IBdoConfiguration config,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : class, IBdoConnector, new()
        {
            return scope.NewConnector(config, varSet, log) as T;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the created connector.</returns>
        public static IBdoConnector NewConnector(
            this IBdoScope scope,
            IBdoConfiguration config,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (config != null && scope?.Check(true, log: log) == true)
            {
                // we get the connector class reference

                IBdoConnectorDefinition definition = scope.ExtensionStore.GetItemDefinitionWithUniqueId<IBdoConnectorDefinition>(config?.DefinitionUniqueId);
                if (definition == null)
                {
                    log?.AddError("Could not retrieve the extension connector '" + config.DefinitionUniqueId + "' definition");
                }
                else
                {
                    // we intantiate the connector
                    AssemblyHelper.CreateInstance(definition.RuntimeType, out object item, log);

                    if (item != null)
                    {
                        var connector = item as IBdoConnector;
                        connector.UpdateFromMeta(config, true, scope, varSet);
                    }
                }
            }

            return null;
        }

        // Tasks ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <typeparam name="T">The entity class to return.</typeparam>
        /// <returns>Returns the created entity.</returns>
        public static T NewTask<T>(
            this IBdoScope scope,
            IBdoConfiguration config = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : BdoTask
        {
            return scope.NewTask(config, varSet, log) as T;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the created task.</returns>
        public static BdoTask NewTask(
            this IBdoScope scope,
            IBdoConfiguration config = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            BdoTask task = null;

            if (config != null && scope?.Check(true, log: log) == true)
            {
                // we get the task class reference

                IBdoTaskDefinition definition = scope.ExtensionStore.GetItemDefinitionWithUniqueId<IBdoTaskDefinition>(config?.DefinitionUniqueId);
                if (definition == null)
                {
                    log?.AddError("Could not retrieve the extension task '" + config.DefinitionUniqueId + "' definition");
                }
                else
                {
                    // we intantiate the task

                    AssemblyHelper.CreateInstance(definition.RuntimeType, out object item, log);

                    if (log?.HasEvent(EventKinds.Error, EventKinds.Exception) != false)
                    {
                        task = item as BdoTask;
                        //task.UpdateFromMetaSet<BdoTaskInputAttribute>(config, scope, varSet);
                        //task.UpdateFromMetaSet<BdoTaskOutputAttribute>(config, scope, varSet);
                    }
                }
            }

            return task;
        }
    }
}
