using BindOpen.Application.Scopes;
using BindOpen.Data.Stores;
using BindOpen.System.Diagnostics;

namespace Samples.SampleA.Services
{
    public static class Service_Command
    {
        public static void Process(IBdoHost host, IBdoLog log)
        {
            _ = host.DataStore.GetDatasourceDepot()?.GetConnectorConfiguration("db.test", "database.mssqlserver$client");

            //var repo = new TestDbRepository(host?.GetModel<MyDbModel>(), host.CreatePostgreSqlConnector(string.Empty));
            //repo.Test();
        }
    }
}