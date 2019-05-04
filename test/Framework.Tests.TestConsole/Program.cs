using System;
using System.Threading.Tasks;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Diagnostics.Loggers.Factories;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Tests.TestConsole.Services;
using BindOpen.Framework.Tests.TestConsole.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BindOpen.TestConsole
{
    /// <summary>
    /// This class represents the test console program.
    /// </summary>
    /// <remarks>This class like the whole project is temporary. It allows to implement tests before inserting them in Unit test project.</remarks>
    internal static class Program
    {
        public static ITAppHost<AppConfiguration> _AppHost = null;

        private static async Task Main(string[] args)
        {
            await new HostBuilder()
               .ConfigureServices((services) =>
               {
                   services
                    .AddBindOpenDefault(
                        (options) => options
                            .SetRuntimeFolder(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\run")
                            .SetModule(new AppModule("app.test"))
                            .DefineSettings<TestAppSettings>()
                            .SetExtensions(
                                new AppExtensionConfiguration()
                                    .AddFilter("BindOpen.Framework.Databases")
                                    .AddFilter("BindOpen.Framework.Databases.MSSqlServer"))
                            .SetLibraryFolder(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\lib")
                            .AddDefaultLogger()
                            .SetLoggers(
                                LoggerFactory.Create<SnapLogger>(null, LoggerMode.Auto, DataSourceKind.Console))
                    )
                    .AddBindOpenService<TestService, AppConfiguration>();
               })
               .RunConsoleAsync().ConfigureAwait(false);
        }
    }
}
