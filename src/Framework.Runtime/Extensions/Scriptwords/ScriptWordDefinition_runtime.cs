using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Runtime.Application.Hosts;

namespace BindOpen.Framework.Runtime.Extensions.Scriptwords
{
    /// <summary>
    /// This class represents a 'Runtime' script word definition.
    /// </summary>
    [ScriptwordDefinition]
    public static class ScriptwordDefinition_runtime
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
        [Scriptword]
        public static string Var_ApplicationFolderPath(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            if (appScope == null)
                return "<!--Application scope missing-->";
            IBaseBdoAppHost appHost = appScope.DataContext.GetSystemItem("appHost") as IBaseBdoAppHost;
            if (appHost == null)
                return "<!--Application manager missing-->";

            return appHost.GetKnownPath(ApplicationPathKind.AppFolder);
        }

        /// <summary>
        /// Evaluates the script word $(roaming.folderPath).
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword]
        public static string Var_RoamingFolderPath(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            if (appScope == null)
                return "<!--Application scope missing-->";
            IBaseBdoAppHost appHost = appScope.DataContext.GetSystemItem("appHost") as IBaseBdoAppHost;
            if (appHost == null)
                return "<!--Application manager missing-->";

            return appHost.GetKnownPath(ApplicationPathKind.RoamingFolder);
        }

        /// <summary>
        /// Evaluates the script word $(applicationModuleName).
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword]
        public static string Var_ApplicationModuleName(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            if (appScope == null)
                return "<!--Application scope missing-->";
            IBaseBdoAppHost appHost = appScope.DataContext.GetSystemItem("appHost") as IBaseBdoAppHost;
            if (appHost == null)
                return "<!--Application manager missing-->";

            return appHost?.BaseOptions.ApplicationModule?.Name ?? "";
        }

        /// <summary>
        /// Evaluates the script word $(applicationInstanceName).
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [Scriptword]
        public static string Var_ApplicationInstanceName(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters)
        {
            if (appScope == null || appScope.DataContext == null)
                return "<!--Application scope missing-->";
            IBaseBdoAppHost appHost = appScope.DataContext.GetSystemItem("appHost") as IBaseBdoAppHost;
            if (appHost == null)
                return "<!--Application manager missing-->";

            return appHost?.BaseOptions.BaseSettings?.ApplicationInstanceName ?? "";
        }

        #endregion
    }
}