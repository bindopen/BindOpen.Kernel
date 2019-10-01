using System;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.System.Diagnostics.Loggers;
using BindOpen.Framework.Tests.UnitTest.Settings;

namespace BindOpen.Framework.Tests.UnitTest
{
    public static class SetupVariables
    {
        static string _workingFolder = null;
        static IAppHost _appHost = null;

        public static string WorkingFolder
        {
            get
            {
                String workingFolder = SetupVariables._workingFolder;
                if (workingFolder == null)
                    SetupVariables._workingFolder = workingFolder = ((_appHost?.Options?.RuntimeFolderPath ?? AppDomain.CurrentDomain.BaseDirectory.GetEndedString(@"\")) + @"temp\").ToPath();

                return workingFolder;
            }
        }

        public static IAppHost AppHost
        {
            get
            {
                return _appHost ?? (_appHost = AppHostFactory.CreateBindOpenHost<TestAppSettings>(
                        options => options
                            .SetRuntimeFolder(AppDomain.CurrentDomain.BaseDirectory + @"..\..\run")
                            .SetLibraryFolder(AppDomain.CurrentDomain.BaseDirectory + @"..\..\lib")
                            .SetModule(new AppModule("app.test"))
                            .DefineDefaultSettings()
                            .SetExtensions(
                                new AppExtensionFilter("BindOpen.Framework.Databases"),
                                new AppExtensionFilter("BindOpen.Framework.Databases.MSSqlServer"))
                            .AddDefaultLogger()
                            .AddLoggers(
                                LoggerFactory.Create<SnapLogger>(null, LoggerMode.Auto, DataSourceKind.Console))));
            }
        }
    }

}
