using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Items.Carriers;
using BindOpen.Framework.Core.Extensions.Items.Carriers.Definition;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.Extensions.Items.Connectors.Definition;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.Extensions.Items.Tasks.Definition;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static class AppExtensionItemFactory
    {
        // Carriers ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <typeparam name="T">The carrier class to return.</typeparam>
        /// <returns>Returns the created carrier.</returns>
        public static T CreateCarrier<T>(
            this IAppScope appScope,
            ICarrierConfiguration configuration = null,
            string name = null,
            ILog log = null,
            IScriptVariableSet scriptVariableSet = null) where T : Carrier
        {
            return appScope.CreateCarrier(configuration, name, log, scriptVariableSet) as T;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the created carrier.</returns>
        public static Carrier CreateCarrier(
            this IAppScope appScope,
            ICarrierConfiguration configuration = null,
            string name = null,
            ILog log = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            log = log ?? new Log();
            Carrier carrier = null;

            if (!appScope.Check(true).HasErrorsOrExceptions())
            {
                if (configuration != null)
                {
                    // we get the carrier class reference

                    ICarrierDefinition definition = appScope.Extension.GetItemDefinitionWithUniqueId<ICarrierDefinition>(configuration.DefinitionUniqueId);
                    if (definition == null)
                    {
                        log?.AddError("Could not retrieve the extension carrier '" + configuration.DefinitionUniqueId + "' definition");
                    }
                    else
                    {
                        // we intantiate the carrier

                        log.Append(AssemblyHelper.CreateInstance(definition.RuntimeType, out object item));

                        if (!log.HasErrorsOrExceptions())
                        {
                            carrier = item as Carrier;
                            carrier.Name = name ?? configuration?.Name;
                            carrier.UpdateFromElementSet<DetailPropertyAttribute>(configuration, appScope, scriptVariableSet);
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <typeparam name="T">The carrier class to return.</typeparam>
        /// <returns>Returns the created carrier.</returns>
        public static T CreateConnector<T>(
            this IAppScope appScope,
            IConnectorConfiguration configuration = null,
            string name = null,
            ILog log = null,
            IScriptVariableSet scriptVariableSet = null) where T : Connector
        {
            return appScope.CreateConnector(configuration, name, log, scriptVariableSet) as T;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the created connector.</returns>
        public static Connector CreateConnector(
            this IAppScope appScope,
            IConnectorConfiguration configuration = null,
            string name = null,
            ILog log = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            log = log ?? new Log();
            Connector connector = null;

            if (!appScope.Check(true).HasErrorsOrExceptions())
            {
                if (configuration != null)
                {
                    // we get the connector class reference

                    IConnectorDefinition definition = appScope.Extension.GetItemDefinitionWithUniqueId<IConnectorDefinition>(configuration?.DefinitionUniqueId);
                    if (definition == null)
                    {
                        log?.AddError("Could not retrieve the extension connector '" + configuration.DefinitionUniqueId + "' definition");
                    }
                    else
                    {
                        // we intantiate the connector

                        log.Append(AssemblyHelper.CreateInstance(definition.RuntimeType, out object item));

                        if (!log.HasErrorsOrExceptions())
                        {
                            connector = item as Connector;
                            connector.Name = name ?? configuration?.Name;
                            connector.UpdateFromElementSet<DetailPropertyAttribute>(configuration, appScope, scriptVariableSet);
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <typeparam name="T">The carrier class to return.</typeparam>
        /// <returns>Returns the created carrier.</returns>
        public static T CreateTask<T>(
            this IAppScope appScope,
            ITaskConfiguration configuration = null,
            string name = null,
            ILog log = null,
            IScriptVariableSet scriptVariableSet = null) where T : Task
        {
            return appScope.CreateTask(configuration, name, log, scriptVariableSet) as T;
        }

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the created task.</returns>
        public static Task CreateTask(
            this IAppScope appScope,
            ITaskConfiguration configuration = null,
            string name = null,
            ILog log = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            log = log ?? new Log();
            Task task = null;

            if (!appScope.Check(true).HasErrorsOrExceptions())
            {
                if (configuration != null)
                {
                    // we get the task class reference

                    ITaskDefinition definition = appScope.Extension.GetItemDefinitionWithUniqueId<ITaskDefinition>(configuration?.DefinitionUniqueId);
                    if (definition == null)
                    {
                        log?.AddError("Could not retrieve the extension task '" + configuration.DefinitionUniqueId + "' definition");
                    }
                    else
                    {
                        // we intantiate the task

                        log.Append(AssemblyHelper.CreateInstance(definition.RuntimeType, out object item));

                        if (!log.HasErrorsOrExceptions())
                        {
                            task = item as Task;
                            task.Name = name ?? configuration?.Name;
                            task.UpdateFromElementSet<TaskInputAttribute>(configuration, appScope, scriptVariableSet);
                            task.UpdateFromElementSet<TaskOutputAttribute>(configuration, appScope, scriptVariableSet);
                        }
                    }
                }
            }

            return task;
        }
    }
}
