using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.Data.Depots.DbQueries;
using BindOpen.Framework.Databases.MSSqlServer.Extensions.Connectors;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Samples.SampleA.Settings;
using System;

namespace BindOpen.Framework.Samples.SampleA.Services
{
    public class TestService : TBdoHostedService<TestServiceSettings>
    {
        protected override ITBdoService<TestServiceSettings> Process(IBdoLog log)
        {
            base.Process(log);

            var connector = Host.Scope.CreateConnector<DatabaseConnector_MSSqlServer>();

            var sql1 = connector.GetSqlText(Queries.UpdateMyTable(new DbMyTable() { Name = "nameA" }, null), null, log);
            Console.WriteLine(sql1);

            var sql2 = connector.GetSqlText(Queries.GetMyTables("", null), null, log);
            Console.WriteLine(sql2);

            var sql3 = connector.GetSqlText(Queries.GetMyTable("name", null), null, log);
            Console.WriteLine(sql3);

            var depot = Host.Scope.GetDbQueryDepot();

            string sql4 = connector.GetSqlText(
                depot.GetQuery("delete_table"), ElementSetFactory.Create("mymodule", "myid", "myname"), null, Log);
            Console.WriteLine(sql4);

            string sql12 = connector.GetSqlText(
                depot.GetQuery("delete_table"), ElementSetFactory.Create("mymodule", "myid", "myname"), null, Log);
            Console.WriteLine(sql12);

            return this;
        }
    }
}