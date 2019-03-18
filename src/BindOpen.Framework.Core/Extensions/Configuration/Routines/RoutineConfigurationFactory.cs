using System;
using BindOpen.Framework.Core.Application.Commands;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Extensions.Definition.Routines;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Configuration.Routines
{
    /// <summary>
    /// This class represents a routine configuration factory.
    /// </summary>
    public static class RoutineConfigurationFactory
    {

        /// <summary>
        /// Creates a routine of the specified unique name.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="routineUniqueName">The unique name of connection to create.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created connector.</returns>
        public static RoutineConfiguration CreateConfiguration(AppScope appScope, String routineUniqueName, Log log = null)
        {
            log = (log ?? new Log());

            if (!log.Append(appScope.Check()).HasErrorsOrExceptions())
                return appScope.AppExtension.CreateConfiguration<RoutineDefinition>(routineUniqueName, null, log) as RoutineConfiguration;

            return null;
        }

        /// <summary>
        /// Creates a routine of the specified unique name.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="routineUniqueName">The unique name of connection to create.</param>
        /// <param name="parameterDetail">The parameter detail to consider.</param>
        /// <param name="aCommandSet">The command set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created connector.</returns>
        public static RoutineConfiguration CreateConfiguration(
            AppScope appScope,
            String routineUniqueName,
            DataElementSet parameterDetail = null,
            DataItemSet<Command> aCommandSet = null,
            Log log = null)
        {
            log = (log ?? new Log());

            if (!log.Append(appScope.Check()).HasErrorsOrExceptions())
            {
                RoutineConfiguration routine = appScope.AppExtension.CreateConfiguration<RoutineDefinition>(
                    routineUniqueName, null, log) as RoutineConfiguration;
                if (routine != null)
                {
                    routine.ParameterDetail = parameterDetail;
                    routine.CommandSet = aCommandSet;
                }
                return routine;
            }

            return null;
        }

        /// <summary>
        /// Creates a routine of the specified unique name.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="routineUniqueName">The unique name of connection to create.</param>
        /// <param name="referenceRoutineConfiguration">The reference routine configuration to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created connector.</returns>
        public static RoutineConfiguration CreateConfiguration(
            AppScope appScope,
            String routineUniqueName,
            RoutineConfiguration referenceRoutineConfiguration, Log log = null)
        {
            log = (log ?? new Log());

            if (!log.Append(appScope.Check()).HasErrorsOrExceptions())
                return RoutineConfigurationFactory.CreateConfiguration(
                    appScope, routineUniqueName
                    , (referenceRoutineConfiguration != null ? referenceRoutineConfiguration.ParameterDetail : null)
                    , (referenceRoutineConfiguration != null ? referenceRoutineConfiguration.CommandSet : null)
                    , log);

            return null;
        }

    }
}
