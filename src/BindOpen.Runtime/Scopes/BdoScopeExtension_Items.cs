using BindOpen.MetaData;
using BindOpen.MetaData.Elements;
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
        // Carriers ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <typeparam name="T">The carrier class to return.</typeparam>
        /// <returns>Returns the created carrier.</returns>
        public static T NewCarrier<T>(
            this IBdoScope scope,
            IBdoCarrierConfiguration configuration = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null) where T : BdoCarrier
        {
            return scope.NewCarrier(configuration, varElementSet, log) as T;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <returns>Returns the created carrier.</returns>
        public static BdoCarrier NewCarrier(
            this IBdoScope scope,
            IBdoCarrierConfiguration configuration,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            BdoCarrier carrier = null;

            if (configuration != null && scope?.Check(true, log: log) == false)
            {
                // we get the carrier class reference

                IBdoCarrierDefinition definition = scope.ExtensionStore.GetItemDefinitionWithUniqueId<IBdoCarrierDefinition>(configuration.DefinitionUniqueId);
                if (definition == null)
                {
                    log?.AddError("Could not retrieve the extension carrier '" + configuration.DefinitionUniqueId + "' definition");
                }
                else
                {
                    // we intantiate the carrier

                    AssemblyHelper.CreateInstance(definition.RuntimeType, out object item, log);

                    if (item != null)
                    {
                        carrier = item as BdoCarrier;
                        carrier.UpdateFromElementSet<BdoMetaAttribute>(configuration, scope, varElementSet);
                    }
                }
            }

            return carrier;
        }

        // Connectors ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <returns>Returns the created connector.</returns>
        public static T NewConnector<T>(
            this IBdoScope scope,
            IBdoConnectorConfiguration configuration,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null) where T : class, IBdoConnector, new()
        {
            return scope.NewConnector(configuration, varElementSet, log) as T;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <returns>Returns the created connector.</returns>
        public static IBdoConnector NewConnector(
            this IBdoScope scope,
            IBdoConnectorConfiguration configuration,
            IBdoElementSet varElementSet = null,
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
                        connector.UpdateFromElementSet<BdoMetaAttribute>(configuration, scope, varElementSet);
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
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <typeparam name="T">The carrier class to return.</typeparam>
        /// <returns>Returns the created carrier.</returns>
        public static T NewTask<T>(
            this IBdoScope scope,
            IBdoTaskConfiguration configuration = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null) where T : BdoTask
        {
            return scope.NewTask(configuration, varElementSet, log) as T;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <returns>Returns the created task.</returns>
        public static BdoTask NewTask(
            this IBdoScope scope,
            IBdoTaskConfiguration configuration = null,
            IBdoElementSet varElementSet = null,
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
                        task.UpdateFromElementSet<BdoTaskInputAttribute>(configuration, scope, varElementSet);
                        task.UpdateFromElementSet<BdoTaskOutputAttribute>(configuration, scope, varElementSet);
                    }
                }
            }

            return task;
        }
    }
}
