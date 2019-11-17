using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.System;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBotSettings : IBaseSettings
    {
        /// <summary>
        /// 
        /// </summary>
        IBotConfiguration AppConfiguration { get; }

        /// <summary>
        /// 
        /// </summary>
        string ApplicationInstanceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ApplicationInstanceKind ApplicationInstanceKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ApplicationInstanceName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string CookieDomain { get; set; }

        /// <summary>
        /// 
        /// </summary>
        RuntimeMode CurrentRuntimeMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string DefaultTheme { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string DefaultUICultureName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ApplicationExecutionLevel ExecutionLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ExtensionsFolderPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool? IsUserTrackingEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string LogFolderPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string RuntimeFolderPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ServerInstanceName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appScope"></param>
        void SetAppScope(IAppScope appScope);
    }
}