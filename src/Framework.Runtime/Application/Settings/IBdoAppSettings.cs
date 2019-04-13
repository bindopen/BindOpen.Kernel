using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Security;
using BindOpen.Framework.Runtime.System;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    public interface IBdoAppSettings : IConfigurationDto
    {
        List<IApplicationCredential> Credentials { get; set; }
        List<IDataSource> DataSources { get; set; }

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
    }
}