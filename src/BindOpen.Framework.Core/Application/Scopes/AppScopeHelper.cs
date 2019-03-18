using System;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This class represents an application scope helper.
    /// </summary>
    public static class AppScopeHelper
    {
        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="isAppExtensionChecked">Indicates whether the application extension is chekced.</param>
        /// <param name="isScriptInterpreterChecked">Indicates whether the script interpreter is chekced.</param>
        /// <param name="isDataContextChecked">Indicates whether the data context is chekced.</param>
        /// <param name="isDataSourceServiceChecked">Indicates whether the data module manager is chekced.</param>
        /// <returns>The log of check log.</returns>
        public static Log Check(
            this IAppScope appScope,
            Boolean isAppExtensionChecked = false,
            Boolean isScriptInterpreterChecked = false,
            Boolean isDataContextChecked = false,
            Boolean isDataSourceServiceChecked = false)
        {
            Log log = new Log();

            if (appScope == null)
            {
                log.AddError(title: "Application scope missing", description: "No application scope specified.");
            }
            else
            {
                if (isAppExtensionChecked && appScope.AppExtension == null)
                    log.AddError(title: "Application extension missing", description: "No application extension specified.");
                if (isScriptInterpreterChecked && appScope.ScriptInterpreter == null)
                    log.AddError(title: "Script interpreter missing", description: "No script interpreter specified.");
                if (isDataContextChecked && appScope.DataContext == null)
                    log.AddError(title: "Data context missing", description: "No data context specified.");
                if (isDataSourceServiceChecked && appScope.DataSourceService == null)
                    log.AddError(title: "Data module dictionary missing", description: "No data module manager specified.");
            }

            return log;
        }
    }
}