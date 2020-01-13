using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.AspNetCore.Data.Resolvers;
using BindOpen.Framework.Data.Depots;
using BindOpen.Framework.System.Diagnostics;
using Newtonsoft.Json;
using System;

namespace BindOpen.Framework.Samples.SampleA.Services
{
    public static class Service_Command
    {
        public static void Process(IBdoHost host, IBdoLog log)
        {
            var configuration = host.Scope.DataStore.GetDatasourceDepot()?.GetConnectorConfiguration("sphere.identity", "database.mssqlserver$client");
            var st = JsonConvert.SerializeObject(host.HostOptions.AppSettings.AppConfiguration, Formatting.Indented, SerializerSettingsFactory.CreateSettings());

            var connector = host.CreateMSSqlServerConnector();

            var sql1 = "1- " + connector.CreateCommandText(Queries.UpdateMyTable(new DbMyTable() { Name = "fdfdfd] --' OR 1=1 --" }, null), true, null, log);
            Console.WriteLine(sql1);

            var sql2 = "2- " + connector.CreateCommandText(Queries.GetMyTables("", null), true, null, log);
            Console.WriteLine(sql2);

            var sql3 = "3- " + connector.CreateCommandText(Queries.GetMyTable("1' OR '1'='1' ", null), true, null, log);
            Console.WriteLine(sql3);

            var depot = host.Scope.GetDbQueryDepot();

            string sql4 = "4- " + connector.CreateCommandText(depot?.GetQuery("delete_table"), true, null, log);
            Console.WriteLine(sql4);

            string sql12 = "5- " + connector.CreateCommandText(depot?.GetQuery("delete_table"), true, null, log);
            Console.WriteLine(sql12);

            connector?.Dispose();
        }
    }
}