using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Application.Services;
using BindOpen.Framework.Data.Depots;
using BindOpen.Framework.Extensions.References;
using BindOpen.Framework.Samples.SampleA.Services;
using BindOpen.Framework.Samples.SampleA.Services.Databases;
using BindOpen.Framework.Samples.SampleA.Settings;
using BindOpen.Framework.System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                    .AddBindOpen<TestAppSettings>(
                        (options) => options
                            .SetRootFolder(q => q.HostSettings.Environment != "Development", @".\..\..\..")
                            .SetRootFolder(q => q.HostSettings.Environment == "Development", @".\")
                            .AddDataStore(s => s
                                .RegisterDatasources(m => m.AddFromConfiguration(options))
                                .RegisterDbModels((m, l) => m.AddFromAssembly<TestService>(l)))
                            .AddExtensions(q => q.AddPostgreSql())
                            .SetHostSettingsFile(false)
                            .SetHostSettings(p => p.WithAppConfigFileRequired(false))
                            .AddDefaultConsoleLogger()
                            .AddDefaultFileLogger("testA.txt")
                            //.ExecuteOnStartSuccess(p => Trace.WriteLine("# events: " + p.Log.GetEventCount().ToString()))
                            .ThrowExceptionOnStartFailure()
                            .ExecuteOnStartSuccess(host =>
                            {
                                var log = new BdoLog();
                                Service_Command.Process(host, log);
                            })
                    )
                   .AddTransientConnectedService<IBdoDbService, TestDbRepository>(host =>
                        new TestDbRepository(host.GetModel<MyDbModel>())
                            .WithConnector(host.CreateMSSqlServerConnector("mlmlm")) as TestDbRepository)
                   .AddBindOpenService<TestService, TestServiceSettings, TestAppSettings>(null, p =>
                       {
                           TestAppSettings appSettings = p as TestAppSettings;
                           return new TestServiceSettings()
                           {
                               TestFolderPath = appSettings?.TestFolderPath
                           };
                       })
                   ;
               })
               .RunConsoleAsync().ConfigureAwait(false);
        }
    }
}