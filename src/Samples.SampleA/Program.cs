using BindOpen.Framework.Application.Repositories;
using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Data.Depots;
using BindOpen.Framework.Extensions.References;
using BindOpen.Framework.Samples.SampleA.Services;
using BindOpen.Framework.Samples.SampleA.Settings;
using BindOpen.Framework.System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BindOpen.Framework.Samples.SampleA
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            await new HostBuilder()
               .ConfigureServices(services =>
               {
                   services
                    .AddBindOpenHost<TestAppSettings>(
                        (options) => options
                            .SetRootFolder(q => q.HostSettings.Environment != "Development", @".\..\..\..")
                            .SetRootFolder(q => q.HostSettings.Environment == "Development", @".\")
                            .AddDataStore(s => s
                                .RegisterDasourceDepot(options)
                                //.RegisterDatasourceDepotFromNative(service.Configuration)
                                .RegisterDbQueryDepot((m, l) => m.AddFromAssembly<TestService>(l)))
                            .AddExtensions(
                                q => q.AddPostgreSql(),
                                p => p.WithRemoteServerUri(""))
                            .SetHostSettingsFile(false)
                            .SetHostSettings(p => p.WithAppConfigFileRequired(false))
                            //.SetAppSettings(p => p.FromConfiguration(services.Configuration, "bindopen"))
                            .AddDefaultConsoleLogger()
                            .AddDefaultFileLogger("testA.txt")
                            .ExecuteOnStartSuccess(p => Trace.WriteLine("# events: " + p.Log.GetEventCount().ToString()))
                            .ThrowExceptionOnStartFailure()
                            .ExecuteOnStartSuccess(host =>
                            {
                                var log = new BdoLog();
                                Service_Command.Process(host, log);
                            })
                    )
                    .AddTransientRepository<IBdoDbRepository, TestDbRepository>(host => host.CreateMSSqlServerConnector().WithConnectionString("mlmlm"))
                    .AddBindOpenService<TestService, TestServiceSettings, TestAppSettings>(null, p =>
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