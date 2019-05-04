using System;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Tests.TestConsole.Settings;

namespace BindOpen.Framework.UnitTest
{
    public static class SetupVariables
    {
        static string _workingFolder = null;
        static TAppHost<AppConfiguration> _appHost = null;

        public static string WorkingFolder
        {
            get
            {
                String workingFolder = SetupVariables._workingFolder;
                if (workingFolder == null)
                    SetupVariables._workingFolder = workingFolder = (AppDomain.CurrentDomain.BaseDirectory.GetEndedString(@"\") + @"temp\").ToPath();

                return workingFolder;
            }
        }

        public static TAppHost<AppConfiguration> AppHost
        {
            get
            {
                if (_appHost == null)
                {
                    _appHost = AppHostFactory.CreateBindOpenDefault(
                        options => options
                            .SetRuntimeFolder(AppDomain.CurrentDomain.BaseDirectory + @"..\..\run")
                            .SetModule(new AppModule("app.test"))
                            .DefineSettings<TestAppSettings>()
                            .SetExtensions(
                                new AppExtensionConfiguration()
                                    .AddFilter("BindOpen.Framework.Databases")
                                    .AddFilter("BindOpen.Framework.Databases.MSSqlServer"))
                            .SetLibraryFolder(AppDomain.CurrentDomain.BaseDirectory + @"..\..\lib")
                            .AddDefaultLogger());
                }

                return _appHost;
            }
        }
    }

}
