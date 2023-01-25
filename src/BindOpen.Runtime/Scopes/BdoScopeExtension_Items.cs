using BindOpen.Data;
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
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <typeparam name="T">The entity class to return.</typeparam>
        /// <returns>Returns the created entity.</returns>
        public static T NewEntity<T>(
            this IBdoScope scope,
            IBdoEntityConfiguration configuration = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : BdoEntity
        {
            return scope.NewEntity(configuration, varSet, log) as T;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the created entity.</returns>
        public static BdoEntity NewEntity(
            this IBdoScope scope,
            IBdoEntityConfiguration configuration,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            BdoEntity entity = null;

            if (configuration != null && scope?.Check(true, log: log) == false)
            {
                // we get the entity class reference

                IBdoEntityDefinition definition = scope.ExtensionStore.GetItemDefinitionWithUniqueId<IBdoEntityDefinition>(configuration.DefinitionUniqueId);
                if (definition == null)
                {
                    log?.AddError("Could not retrieve the extension entity '" + configuration.DefinitionUniqueId + "' definition");
                }
                else
                {
                    // we intantiate the entity

                    AssemblyHelper.CreateInstance(definition.RuntimeType, out object item, log);

                    if (item != null)
                    {
                        entity = item as BdoEntity;
                        entity.UpdateFromElementSet<BdoDataAttribute>(configuration, scope, varSet);
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
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <returns>Returns the created connector.</returns>
        public static T NewConnector<T>(
            this IBdoScope scope,
            IBdoConnectorConfiguration configuration,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : class, IBdoConnector, new()
        {
            return scope.NewConnector(configuration, varSet, log) as T;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the created connector.</returns>
        public static IBdoConnector NewConnector(
            this IBdoScope scope,
            IBdoConnectorConfiguration configuration,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (configuration != null && scope?.Check(true, log: log) == true)
            {
                // we get the connector class reference

                IBdoConnectorDefinition definition = scope.ExtensionStore.GetItemDefinitionWithUniqueId<IBdoConnectorDefinition>(configuration?.DefinitionUniqueId);
                if (definition == null)
                {
                    log?.AddError("Could not retrieve the extension connector '" + configuration.DefinitionUniqueId + "' definition");
                }
                else
                {
                    // we intantiate the connector
                    AssemblyHelper.CreateInstance(definition.RuntimeType, out object item, log);

                    if (item != null)
                    {
                        var connector = item as IBdoConnector;
                        connector.UpdateFromElementSet<BdoDataAttribute>(configuration, scope, varSet);
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
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <typeparam name="T">The entity class to return.</typeparam>
        /// <returns>Returns the created entity.</returns>
        public static T NewTask<T>(
            this IBdoScope scope,
            IBdoTaskConfiguration configuration = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : BdoTask
        {
            return scope.NewTask(configuration, varSet, log) as T;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the created task.</returns>
        public static BdoTask NewTask(
            this IBdoScope scope,
            IBdoTaskConfiguration configuration = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            BdoTask task = null;

            if (configuration != null && scope?.Check(true, log: log) == true)
            {
                // we get the task class reference

                IBdoTaskDefinition definition = scope.ExtensionStore.GetItemDefinitionWithUniqueId<IBdoTaskDefinition>(configuration?.DefinitionUniqueId);
                if (definition == null)
                {
                    log?.AddError("Could not retrieve the extension task '" + configuration.DefinitionUniqueId + "' definition");
                }
                else
                {
                    // we intantiate the task

                    AssemblyHelper.CreateInstance(definition.RuntimeType, out object item, log);

                    if (log?.HasEvent(EventKinds.Error, EventKinds.Exception) != false)
                    {
                        task = item as BdoTask;
                        task.UpdateFromElementSet<BdoTaskInputAttribute>(configuration, scope, varSet);
                        task.UpdateFromElementSet<BdoTaskOutputAttribute>(configuration, scope, varSet);
                    }
                }
            }

            return task;
        }
    }
}
