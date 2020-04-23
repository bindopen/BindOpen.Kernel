using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Files;
using BindOpen.Data.Helpers.Strings;
using BindOpen.System.Diagnostics;
using BindOpen.System.Diagnostics.Loggers;
using BindOpen.Tests.Core.Settings;
using Microsoft.Extensions.Configuration;
using System;

namespace BindOpen.Tests.Core
{
    public static class GlobalVariables
    {
        static string _workingFolder = null;
        static IBdoHost _appHost = null;
        static IConfiguration _netCoreConfiguration;

        public static string WorkingFolder
        {
            get
            {
                string workingFolder = GlobalVariables._workingFolder;
                if (workingFolder == null)
                    GlobalVariables._workingFolder = workingFolder =
                        ((_appHost?.GetKnownPath(BdoHostPathKind.RuntimeFolder) ?? AppDomain.CurrentDomain.BaseDirectory).GetEndedString(@"\") + @"bdo\temp\").ToPath();

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
                            //.SetRootFolder(q => q.HostSettings.Environment != "Development", @"..\..\..")
                            //.SetRootFolder(q => q.HostSettings.Environment == "Development", @"..\..")
                            .AddDefaultFileLogger()
                            .ThrowExceptionOnStartFailure()
                            .AddLoggers(
                                BdoLoggerFactory.Create<BdoSnapLogger>(null, BdoLoggerMode.Auto).AddConsoleOutput())
                        ));
            }
        }

        public static IConfiguration NetCoreConfiguration
        {
            get
            {
                if (_netCoreConfiguration != null)
                {
                    return _netCoreConfiguration;
                }

                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppHost?.GetKnownPath(BdoHostPathKind.RootFolder))
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                builder.AddEnvironmentVariables();
                return _netCoreConfiguration = builder.Build();
            }
        }
    }

}
