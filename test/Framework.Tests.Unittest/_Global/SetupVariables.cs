using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.MSSqlServer.Extensions;
using BindOpen.Framework.Runtime.Application.Bots;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.System.Diagnostics.Loggers;
using BindOpen.Framework.Tests.UnitTest.Settings;
using System;

namespace BindOpen.Framework.Tests.UnitTest
{
    public static class SetupVariables
    {
        static string _workingFolder = null;
        static IBot _appHost = null;

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

        public static IBot AppHost
        {
            get
            {
                return _appHost ?? (_appHost = BotFactory.CreateBindOpenBot<TestAppSettings>(
                        options => options
                            .SetRuntimeFolder(@"..\..\run")
                            .SetLibraryFolder(@"..\..\lib")
                            .SetModule(new AppModule("app.test"))
                            .DefineDefaultSettings()
                            .AddMSSqlServerExtension()
                            .AddDefaultLogger()
                            .AddLoggers(
                                LoggerFactory.Create<SnapLogger>(null, LoggerMode.Auto, DataSourceKind.Console))));
            }
        }
    }

}
