using System;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Extensions.Configuration.Scriptwords;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Runtime.Application.Hosts;

namespace BindOpen.Framework.Runtime.Extensions.Scriptwords
{

    /// <summary>
    /// This class represents a 'Runtime' script word definition.
    /// </summary>
    public static class ScriptWordDefinition_runtime
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// Evaluates the script word $(application.folderPath).
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Var_ApplicationFolderPath(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            ScriptWord scriptWord,
            params Object[] parameters)
        {
            if (appScope == null)
                return "<!--Application scope missing-->";
            AppHost appHostService =
                appScope.DataContext.GetSystemItem("appHost") as AppHost;
            if (appHostService == null)
                return "<!--Application manager missing-->";

            return appHostService.GetKnownPath(ApplicationPathKind.AppFolder);
        }

        /// <summary>
        /// Evaluates the script word $(roaming.folderPath).
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Var_RoamingFolderPath(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            ScriptWord scriptWord,
            params Object[] parameters)
        {
            if (appScope == null)
                return "<!--Application scope missing-->";
            IAppHost appHostService =
                appScope.DataContext.GetSystemItem("appHost") as AppHost;
            if (appHostService == null)
                return "<!--Application manager missing-->";

            return appHostService.GetKnownPath(ApplicationPathKind.RoamingFolder);
        }

        /// <summary>
        /// Evaluates the script word $(applicationModuleName).
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Var_ApplicationModuleName(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            ScriptWord scriptWord,
            params Object[] parameters)
        {
            if (appScope == null)
                return "<!--Application scope missing-->";
            AppHost appHostService =
                appScope.DataContext.GetSystemItem("appHost") as AppHost;
            if (appHostService == null)
                return "<!--Application manager missing-->";

            return appHostService?.Options.ApplicationModule?.Name ?? "";
        }

        /// <summary>
        /// Evaluates the script word $(applicationInstanceName).
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static String Var_ApplicationInstanceName(
            IAppScope appScope,
            ScriptVariableSet scriptVariableSet,
            ScriptWord scriptWord,
            params Object[] parameters)
        {
            if (appScope == null || appScope.DataContext == null)
                return "<!--Application scope missing-->";
            AppHost appHostService =
                appScope.DataContext.GetSystemItem("appHost") as AppHost;
            if (appHostService == null)
                return "<!--Application manager missing-->";

            return appHostService?.Options.Settings?.ApplicationInstanceName ?? "";
        }

        #endregion

    }
}