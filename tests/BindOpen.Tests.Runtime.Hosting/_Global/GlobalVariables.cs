using BindOpen.Meta;
using BindOpen.Runtime.Hosts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace BindOpen.Runtime.Hosting.Tests
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
                        ((_appHost?.GetKnownPath(BdoHostPathKind.RootFolder) ?? AppDomain.CurrentDomain.BaseDirectory).EndingWith(@"\") + @"bdo\temp\").ToPath();

                return workingFolder;
            }
        }

        public static IBdoHost AppHost
        {
            get
            {
                return _appHost ??= BdoHosting.NewHost(
                    options => options
                        .SetLogger(p =>
                        {
                            Log.Logger = new LoggerConfiguration()
                                .Enrich.FromLogContext()
                                .WriteTo.Console()
                                .CreateLogger();

                            var loggerFactory = new LoggerFactory();
                            loggerFactory.AddSerilog(Log.Logger);
                            return loggerFactory.CreateLogger<IBdoHost>();
                        })
                        .ThrowExceptionOnStartFailure());
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

                return _netCoreConfiguration = builder.Build();
            }
        }
    }

}
