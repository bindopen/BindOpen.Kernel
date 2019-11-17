using BindOpen.Framework.Core.System.Diagnostics;
using System;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This class represents a bot scope helper.
    /// </summary>
    public static class BotScopeHelper
    {
        /// <summary>
        /// Creates a new scope.
        /// </summary>
        /// <returns>The log of check log.</returns>
        public static IBotScope CreateHostScope(AppDomain appDomain = null) => new BotScope(appDomain);

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="isExtensionChecked">Indicates whether the application extension is chekced.</param>
        /// <param name="isInterpreterChecked">Indicates whether the script interpreter is chekced.</param>
        /// <param name="isConnectionServiceChecked">Indicates whether the connection service is chekced.</param>
        /// <param name="isContextChecked">Indicates whether the data context is chekced.</param>
        /// <param name="isDataSourceDepotChecked">Indicates whether the data module manager is chekced.</param>
        /// <returns>The log of check log.</returns>
        public static ILog Check(
            this BotScope appScope,
            bool isExtensionChecked = false,
            bool isInterpreterChecked = false,
            bool isConnectionServiceChecked = false,
            bool isContextChecked = false,
            bool isDataSourceDepotChecked = false)
        {
            ILog log = AppScopeHelper.Check(
                appScope,
                isExtensionChecked,
                isInterpreterChecked,
                isConnectionServiceChecked,
                isDataSourceDepotChecked);

            if (appScope != null)
            {
                if (isContextChecked || appScope.Context == null)
                {
                    log.AddError(title: "Context missing", description: "No data context specified.");
                }

                if (isConnectionServiceChecked || appScope.ConnectionService == null)
                {
                    log.AddError(title: "Connection manager missing", description: "No connection service specified.");
                }
            }

            return log;
        }
    }
}