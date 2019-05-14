using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.System;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    public interface IAppSettings : IBaseSettings
    {
        new IAppConfiguration AppConfiguration { get; }

        string ApplicationInstanceId { get; set; }

        ApplicationInstanceKind ApplicationInstanceKind { get; set; }

        string ApplicationInstanceName { get; set; }

        string CookieDomain { get; set; }

        RuntimeMode CurrentRuntimeMode { get; set; }

        string DefaultTheme { get; set; }

        string DefaultUICultureName { get; set; }

        ApplicationExecutionLevel ExecutionLevel { get; set; }

        string ExtensionsFolderPath { get; set; }

        bool? IsUserTrackingEnabled { get; set; }

        string LogFolderPath { get; set; }

        string RuntimeFolderPath { get; set; }

        string ServerInstanceName { get; set; }
        void SetAppScope(IAppHostScope appScope);
    }
}