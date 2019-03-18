using System;
using System.Xml.Schema;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Security;
using BindOpen.Framework.Runtime.Application.Settings;
using static BindOpen.Framework.Runtime.Application.Services.AppService;

namespace BindOpen.Framework.Runtime.Application.Services
{
    public interface IAppService : IScopedService
    {
        TimeSpan AckPeriod { get; set; }
        AppDomainPool AppDomainPool { get; set; }
        AppModule ApplicationModule { get; }
        AppSettings Settings { get; set; }
        DataElementSpecificationSet SettingsSpecificationSet { get; set; }
        ProcessExecutionState CurrentExecutionState { get; set; }
        ProcessExecutionStatus CurrentExecutionStatus { get; set; }
        bool IsLoadCompleted { get; }
        Log Log { get; }
        DataElementSet UserSettingsSet { get; set; }

        event OnLoadCompletedEventHandler OnLoadCompleted;

        Log Acknowledge();
        IAppService End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);
        ApplicationCredential GetCredential(string name);
        string GetPath(ApplicationPathKind pathKind);
        void LoadComplete();
        AppSettings LoadSettings(string filePath, Log log, IAppScope appScope = null, XmlSchemaSet xmlSchemaSet = null);
        void SaveConfiguration();
        IAppService Start(Log log = null);
        IAppService UseSettingsFile(string appSettingsFilePath);
        IAppService UseAppFolder(string appFolderPath);
        IAppService UseSettings<T>(string settingsFilePath = null, DataElementSpecificationSet specificationSet = null) where T : AppSettings, new();
        IAppService UseSettings<T>(DataElementSpecificationSet specificationSet) where T : AppSettings, new();
        IAppService UseDefaultLogger();
        IAppService UseExtensions(AppExtensionConfiguration extensionConfiguration);
        IAppService UseLoggers(params Logger[] loggers);
        IAppService UseModule(AppModule module);
        IAppService UseRuntimeFolder(string runtimeFolderPath);
    }
}