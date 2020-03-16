using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Stores;
using BindOpen.Extensions.References;
using BindOpen.System.Diagnostics;
using BindOpen.System.Diagnostics.Loggers;
using BindOpen.Tests.Databases.Models;
using BindOpen.Tests.Databases.Settings;
using System;

namespace BindOpen.Tests.Databases
{
    public static class GlobalVariables
    {
        static string _workingFolder = null;
        static IBdoHost _appHost = null;

        public static string WorkingFolder
        {
            get
            {
                String workingFolder = GlobalVariables._workingFolder;
                if (workingFolder == null)
                    GlobalVariables._workingFolder = workingFolder = ((_appHost?.GetKnownPath(BdoHostPathKind.RuntimeFolder) ?? AppDomain.CurrentDomain.BaseDirectory.GetEndedString(@"\")) + @"temp\").ToPath();

                return workingFolder;
            }
        }

        public static IBdoHost AppHost
        {
            get
            {
                return _appHost ?? (_appHost = BdoHostFactory.CreateBindOpenHost<TestAppSettings>(
                        options => options
                            .SetModule("app.test")
                            .SetRootFolder(q => q.HostSettings.Environment != "Development", @".\..\..")
                            .AddExtensions(p => p.AddMSSqlServer().AddPostgreSql())
                            .AddDataStore(s => s
                                .RegisterDatasources(m => m.AddFromConfiguration(options))
                                .RegisterDbModels((m, l) => m.AddFromAssembly<TestDbModel>(l)))
                            .AddDefaultFileLogger()
                            .ThrowExceptionOnStartFailure()
                            .AddLoggers(
                                BdoLoggerFactory.Create<BdoSnapLogger>(null, BdoLoggerMode.Auto).AddConsoleOutput())
                        ));
            }
        }
    }

}
