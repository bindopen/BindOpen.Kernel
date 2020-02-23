using BindOpen.Data.Helpers.Objects;
using BindOpen.Extensions.Definition;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Assemblies;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;

namespace BindOpen.Application.Scopes
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static class BdoScopeExtension_Items
    {
        // Carriers ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <typeparam name="T">The carrier class to return.</typeparam>
        /// <returns>Returns the created carrier.</returns>
        public static T CreateCarrier<T>(
            this IBdoScope scope,
            IBdoCarrierConfiguration configuration = null,
            string name = null,
            IBdoLog log = null,
            IBdoScriptVariableSet scriptVariableSet = null) where T : BdoCarrier
        {
            return scope.CreateCarrier(configuration, name, log, scriptVariableSet) as T;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the created carrier.</returns>
        public static BdoCarrier CreateCarrier(
            this IBdoScope scope,
            IBdoCarrierConfiguration configuration = null,
            string name = null,
            IBdoLog log = null,
            IBdoScriptVariableSet scriptVariableSet = null)
        {
            BdoCarrier carrier = null;

            if (scope?.Check(true).HasErrorsOrExceptions() == false)
            {
                if (configuration != null)
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

                        AssemblyHelper.CreateInstance(definition.RuntimeType, out object item).AddEventsTo(log);

                        if (item != null)
                        {
                            carrier = item as BdoCarrier;
                            carrier.Name = name ?? configuration?.Name;
                            carrier.UpdateFromElementSet<DetailPropertyAttribute>(configuration, scope, scriptVariableSet);
                        }
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
        /// <param name="name">The name to consider.</param>
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <returns>Returns the created connector.</returns>
        public static T CreateConnector<T>(
            this IBdoScope scope,
            string name = null) where T : class, IBdoConnector, new()
        {
            T connector = new T
            {
                Name = name
            };
            connector.WithScope(scope);

            return connector;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <returns>Returns the created connector.</returns>
        public static T CreateConnector<T>(
            this IBdoScope scope,
            IBdoConnectorConfiguration configuration,
            string name = null,
            IBdoLog log = null,
            IBdoScriptVariableSet scriptVariableSet = null) where T : class, IBdoConnector, new()
        {
            return scope.CreateConnector(configuration, name, log, scriptVariableSet) as T;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the created connector.</returns>
        public static IBdoConnector CreateConnector(
            this IBdoScope scope,
            IBdoConnectorConfiguration configuration = null,
            string name = null,
            IBdoLog log = null,
            IBdoScriptVariableSet scriptVariableSet = null)
        {
            BdoConnector connector = null;

            if (scope?.Check(true).HasErrorsOrExceptions() == false)
            {
                if (configuration != null)
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
                        AssemblyHelper.CreateInstance(definition.RuntimeType, out object item).AddEventsTo(log);

                        if (item != null)
                        {
                            connector = item as BdoConnector;
                            connector.Name = name ?? configuration?.Name;
                            connector.WithScope(scope);
                            connector.UpdateFromElementSet<DetailPropertyAttribute>(configuration, scope, scriptVariableSet);
                        }
                    }
                }
            }

            return connector;
        }

        // Tasks ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <typeparam name="T">The carrier class to return.</typeparam>
        /// <returns>Returns the created carrier.</returns>
        public static T CreateTask<T>(
            this IBdoScope scope,
            IBdoTaskConfiguration configuration = null,
            string name = null,
            IBdoLog log = null,
            IBdoScriptVariableSet scriptVariableSet = null) where T : BdoTask
        {
            return scope.CreateTask(configuration, name, log, scriptVariableSet) as T;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the created task.</returns>
        public static BdoTask CreateTask(
            this IBdoScope scope,
            IBdoTaskConfiguration configuration = null,
            string name = null,
            IBdoLog log = null,
            IBdoScriptVariableSet scriptVariableSet = null)
        {
            BdoTask task = null;

            if (scope?.Check(true).HasErrorsOrExceptions() == false)
            {
                if (configuration != null)
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

                        AssemblyHelper.CreateInstance(definition.RuntimeType, out object item).AddEventsTo(log);

                        if (!log.HasErrorsOrExceptions())
                        {
                            task = item as BdoTask;
                            task.Name = name ?? configuration?.Name;
                            task.UpdateFromElementSet<TaskInputAttribute>(configuration, scope, scriptVariableSet);
                            task.UpdateFromElementSet<TaskOutputAttribute>(configuration, scope, scriptVariableSet);
                        }
                    }
                }
            }

            return task;
        }
    }
}
