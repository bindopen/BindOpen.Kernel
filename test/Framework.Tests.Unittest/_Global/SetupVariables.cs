using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Data.Helpers.Strings;
using BindOpen.Framework.Extensions.References;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Diagnostics.Loggers;
using BindOpen.Framework.Tests.UnitTest.Settings;
using System;

namespace BindOpen.Framework.Tests.UnitTest
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
