using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Extensions.References;
using BindOpen.System.Diagnostics;
using BindOpen.System.Diagnostics.Loggers;
using BindOpen.Tests.UnitTest.Settings;
using System;

namespace BindOpen.Tests.UnitTest
{
    public static class SetupVariables
    {
        static string _workingFolder = null;
        static IBdoHost _appHost = null;

        public static string WorkingFolder
        {
            get
            {
                String workingFolder = SetupVariables._workingFolder;
                if (workingFolder == null)
                    SetupVariables._workingFolder = workingFolder = ((_appHost?.GetKnownPath(BdoHostPathKind.RuntimeFolder) ?? AppDomain.CurrentDomain.BaseDirectory.GetEndedString(@"\")) + @"temp\").ToPath();

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
                            .SetRootFolder(@"..\..")
                            .AddExtensions(p => p.AddMSSqlServer())
                            .AddDefaultFileLogger()
                            .ThrowExceptionOnStartFailure()
                            .AddLoggers(
                                BdoLoggerFactory.Create<BdoSnapLogger>(null, BdoLoggerMode.Auto).AddConsoleOutput())
                        ));
            }
        }
    }

}
