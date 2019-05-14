using System;
using System.Threading.Tasks;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.System.Diagnostics.Loggers;
using BindOpen.Framework.Tests.TestConsole;
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
        private static async Task Main(string[] args)
        {
            Test_It.Start();

            ILogger[] loggers = new []
            {
                LoggerFactory.Create<SnapLogger>(null, LoggerMode.Auto, DataSourceKind.Console)
            };

            await new HostBuilder()
               .ConfigureServices((services) =>
               {
                   services
                    .AddBindOpenHost<TestAppSettings>(
                        (options) => options
                            .DefineSettings()
                            .SetRuntimeFolder(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\run")
                            //.SetModule(new AppModule("app.test"))
                            .SetExtensions(
                                new AppExtensionConfiguration()
                                    .AddFilter("BindOpen.Framework.Databases")
                                    .AddFilter("BindOpen.Framework.Databases.MSSqlServer"))
                            .SetLibraryFolder(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\lib")
                            .AddDefaultLogger()
                            .SetLoggers(loggers)
                    )
                    .AddBindOpenService<TestService>(loggers, p =>
                        {
                            TestAppSettings appSettings = p as TestAppSettings;
                            return new TestServiceSettings()
                            {
                                TestFolderPath = appSettings.TestFolderPath
                            };
                        });
               })
               .RunConsoleAsync().ConfigureAwait(false);
        }
    }
}
