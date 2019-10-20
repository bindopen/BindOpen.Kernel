using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.MSSqlServer.Extensions;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.System.Diagnostics.Loggers;
using BindOpen.Framework.Sample.Services;
using BindOpen.Framework.Sample.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace BindOpen.Framework.Sample
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            await new HostBuilder()
               .ConfigureServices((services) =>
               {
                   services
                    .AddBindOpenHost<TestAppSettings>(
                        (options) => options
                            .SetRuntimeFolder(@"..\..\..\run")
                            .SetLibraryFolder(@"..\..\..\lib")
                            .SetModule(new AppModule("app.test"))
                            .AddPostgreSqlExtension()
                            .AddMSSqlServerExtension()
                            .AddDefaultLogger()
                            .AddLoggers(
                                LoggerFactory.Create<SnapLogger>(null, LoggerMode.Auto, DataSourceKind.Console)))

                    .AddBindOpenService<TestService>(null, p =>
                        {
                            TestAppSettings appSettings = p as TestAppSettings;
                            return new TestServiceSettings()
                            {
                                TestFolderPath = appSettings?.TestFolderPath
                            };
                        });
               })
               .RunConsoleAsync().ConfigureAwait(false);
        }
    }
}