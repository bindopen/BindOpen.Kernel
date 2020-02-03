using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Data.Depots;
using BindOpen.Framework.Samples.SampleA.Services.Databases;
using BindOpen.Framework.System.Diagnostics;

namespace BindOpen.Framework.Samples.SampleA.Services
{
    public static class Service_Command
    {
        public static void Process(IBdoHost host, IBdoLog log)
        {
            var configuration = host.Scope.DataStore.GetDatasourceDepot()?.GetConnectorConfiguration("sphere.identity", "database.mssqlserver$client");

            var repo = new TestDbRepository(host?.GetModel<MyDbModel>(), host.CreatePostgreSqlConnector(""));
            repo.Test();
        }
    }
}