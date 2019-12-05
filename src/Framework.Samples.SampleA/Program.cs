using BindOpen.Framework.Databases.Data.Depots.DbQueries;
using BindOpen.Framework.Databases.MSSqlServer.Extensions;
using BindOpen.Framework.Databases.PostgreSql.Extensions;
using BindOpen.Framework.Core.Data.Depots.Datasources;
using BindOpen.Framework.Samples.SampleA.Services;
using BindOpen.Framework.Samples.SampleA.Settings;
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
               .ConfigureServices((services) =>
               {
                   services
                    .AddBindOpenHost<TestAppSettings>(
                        (options) => options
                            .SetAppFolder(@".\..\..\..")
                            .AddDataStore(s=>
                                s.RegisterDasourceDepot(options)
                                .RegisterDbQueryDepot(m => m.AddFromAssembly<TestService>()))
                            .AddExtensions(p =>
                                p.AddMSSqlServer()
                                .AddPostgreSql())
                            .AddDefaultFileLogger("testA.txt")
                            .ThrowExceptionOnStartFailure())

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