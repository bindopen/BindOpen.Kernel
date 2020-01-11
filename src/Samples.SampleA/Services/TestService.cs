using BindOpen.Framework.AspNetCore.Data.Resolvers;
using BindOpen.Framework.Data.Depots;
using BindOpen.Framework.Data.Depots;
using BindOpen.Framework.Databases.MSSqlServer.Extensions.Connectors;
using BindOpen.Framework.Extensions.Runtime;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Samples.SampleA.Settings;
using BindOpen.Framework.System.Diagnostics;
using Newtonsoft.Json;
using System;

namespace BindOpen.Framework.Samples.SampleA.Services
{
    public class TestService : TBdoHostedService<TestServiceSettings>
    {
        protected override ITBdoService<TestServiceSettings> Process(IBdoLog log)
        {
            base.Process(log);

            var configuration = Host.Scope.DataStore.GetDatasourceDepot()?.GetConnectorConfiguration("sphere.identity", "database.mssqlserver$client");
            var st = JsonConvert.SerializeObject(Host.HostOptions.AppSettings.AppConfiguration, Formatting.Indented, SerializerSettingsFactory.CreateSettings());

            var connector = Host.Scope.CreateConnector<DatabaseConnector_MSSqlServer>();

            var sql1 = "1- " + connector.CreateCommandText(Queries.UpdateMyTable(new DbMyTable() { Name = "fdfdfd] --' OR 1=1 --" }, null), true, null, log);
            Console.WriteLine(sql1);

            var sql2 = "2- " + connector.CreateCommandText(Queries.GetMyTables("", null), true, null, log);
            Console.WriteLine(sql2);

            var sql3 = "3- " + connector.CreateCommandText(Queries.GetMyTable("1' OR '1'='1' ", null), true, null, log);
            Console.WriteLine(sql3);

            var depot = Host.Scope.GetDbQueryDepot();

            string sql4 = "4- " + connector.CreateCommandText(depot?.GetQuery("delete_table"), true, null, Log);
            Console.WriteLine(sql4);

            string sql12 = "5- " + connector.CreateCommandText(depot?.GetQuery("delete_table"), true, null, Log);
            Console.WriteLine(sql12);

            return this;
        }
    }
}