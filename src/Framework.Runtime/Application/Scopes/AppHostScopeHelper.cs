using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This class represents a runtime application scope helper.
    /// </summary>
    public static class AppHostScopeHelper
    {
        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="isAppExtensionChecked">Indicates whether the application extension is chekced.</param>
        /// <param name="isScriptInterpreterChecked">Indicates whether the script interpreter is chekced.</param>
        /// <param name="isConnectionManagerChecked">Indicates whether the connection service is chekced.</param>
        /// <param name="isDataContextChecked">Indicates whether the data context is chekced.</param>
        /// <param name="isDataSourceDepotChecked">Indicates whether the data module manager is chekced.</param>
        /// <returns>The log of check log.</returns>
        public static ILog Check(
            this AppHostScope appScope,
            bool isAppExtensionChecked = false,
            bool isScriptInterpreterChecked = false,
            bool isConnectionManagerChecked = false,
            bool isDataContextChecked = false,
            bool isDataSourceDepotChecked = false)
        {
            ILog log = AppScopeHelper.Check(
                appScope,
                isAppExtensionChecked,
                isScriptInterpreterChecked,
                isConnectionManagerChecked,
                isDataSourceDepotChecked);

            if (appScope != null)
            {
                if (isConnectionManagerChecked || appScope.ConnectionService == null)
                {
                    log.AddError(title: "Connection manager missing", description: "No connection service specified.");
                }
            }

            return log;
        }
    }
}