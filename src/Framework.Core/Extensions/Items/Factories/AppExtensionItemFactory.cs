using System;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.Extensions.Definition.Tasks;
using BindOpen.Framework.Core.Extensions.Items.Carriers;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Factories
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
        /// <param name="dto">The DTO item to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the created carrier.</returns>
        public static Carrier CreateCarrier(
            this IAppScope appScope,
            ICarrierDto dto = null,
            ILog log = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            Carrier carrier = null;

            if (!appScope.Check(true).HasErrorsOrExceptions())
            {
                if (dto != null)
                {
                    // we get the carrier class reference

                    ICarrierDefinition definition = appScope.AppExtension.GetItemDefinitionWithUniqueId<ICarrierDefinition>(dto.DefinitionUniqueId);

                    if (definition == null)
                    {
                        log?.AddError("Could not retrieve the extension carrier '" + dto.DefinitionUniqueId + "' definition");
                    }
                    else
                    {
                        // we intantiate the carrier

                        log.Append(AppDomain.CurrentDomain.CreateInstance(definition.RuntimeType, out object item));

                        if (!log.HasErrorsOrExceptions())
                        {
                            carrier = item as Carrier;
                            carrier.MapProperties(dto, typeof(DetailPropertyAttribute), appScope, scriptVariableSet);
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
        /// <param name="dto">The DTO item to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the created connector.</returns>
        public static Connector CreateConnector(
            this IAppScope appScope,
            IConnectorDto dto = null,
            ILog log = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            Connector connector = null;

            if (!appScope.Check(true).HasErrorsOrExceptions())
            {
                if (dto != null)
                {
                    // we get the connector class reference

                    IConnectorDefinition definition = appScope.AppExtension.GetItemDefinitionWithUniqueId<IConnectorDefinition>(dto?.DefinitionUniqueId);
                    if (definition == null)
                    {
                        log?.AddError("Could not retrieve the extension connector '" + dto.DefinitionUniqueId + "' definition");
                    }
                    else
                    {
                        // we intantiate the connector

                        log.Append(AppDomain.CurrentDomain.CreateInstance(definition.RuntimeType, out object item));

                        if (!log.HasErrorsOrExceptions())
                        {
                            connector = item as Connector;
                            connector.MapProperties(dto, typeof(DetailPropertyAttribute), appScope, scriptVariableSet);
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
        /// <param name="dto">The DTO item to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the created task.</returns>
        public static Task CreateTask(
            this IAppScope appScope,
            ITaskDto dto = null,
            ILog log = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            Task task = null;

            if (!appScope.Check(true).HasErrorsOrExceptions())
            {
                if (dto != null)
                {
                    // we get the task class reference

                    ITaskDefinition definition = appScope.AppExtension.GetItemDefinitionWithUniqueId<ITaskDefinition>(dto?.DefinitionUniqueId);
                    if (definition == null)
                    {
                        log?.AddError("Could not retrieve the extension task '" + dto.DefinitionUniqueId + "' definition");
                    }
                    else
                    {
                        // we intantiate the task

                        log.Append(AppDomain.CurrentDomain.CreateInstance(definition.RuntimeType, out object item));

                        if (!log.HasErrorsOrExceptions())
                        {
                            task = item as Task;
                            task.MapProperties(dto?.InputDetail, typeof(TaskInputAttribute), appScope, scriptVariableSet);
                            task.MapProperties(dto?.OutputDetail, typeof(TaskOutputAttribute), appScope, scriptVariableSet);
                        }
                    }
                }
            }

            return task;
        }
    }
}
