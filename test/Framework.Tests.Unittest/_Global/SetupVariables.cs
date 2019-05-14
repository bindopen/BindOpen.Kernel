using System;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Modules;

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
                return _appHost ?? (_appHost = AppHostFactory.CreateBindOpenDefaultHost(
                        options => options
                            .SetRuntimeFolder(AppDomain.CurrentDomain.BaseDirectory + @"..\..\run")
                            .SetModule(new AppModule("app.test"))
                            .DefineDefaultSettings()
                            .SetExtensions(
                                new AppExtensionConfiguration()
                                    .AddFilter("BindOpen.Framework.Databases")
                                    .AddFilter("BindOpen.Framework.Databases.MSSqlServer"))
                            .SetLibraryFolder(AppDomain.CurrentDomain.BaseDirectory + @"..\..\lib")
                            .AddDefaultLogger()));
            }
        }
    }

}
