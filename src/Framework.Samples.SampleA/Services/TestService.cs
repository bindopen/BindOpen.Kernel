using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.Data.Depots.DbQueries;
using BindOpen.Framework.Databases.MSSqlServer.Data.Queries.Builders;
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

            Log.Append(
                new DbQueryBuilder_MSSqlServer(Host.Scope).BuildQuery(
                    Queries.UpdateMyTable(new DbMyTable() { Name = "nameA" }, null), null, out string sql4));
            Console.WriteLine(sql4);

            Log.Append(
                new DbQueryBuilder_MSSqlServer(Host.Scope).BuildQuery(
                    Queries.GetMyTables("", null), null, out string sql1));

            Console.WriteLine(sql1);

            Log.Append(
            new DbQueryBuilder_MSSqlServer(Host.Scope).BuildQuery(
                Queries.GetMyTable("name", null), null, out string sql2));

            Console.WriteLine(sql2);

            var depot = Host.Scope.GetDbQueryDepot();

            string sql11 = Host.Scope.CreateConnector<DatabaseConnector_MSSqlServer>().GetSqlText(
                depot.GetQuery("delete_table"), ElementSetFactory.Create("mymodule", "myid", "myname"), null, Log);
            Console.WriteLine(sql11);

            string sql12 = Host.Scope.CreateConnector<DatabaseConnector_MSSqlServer>().GetSqlText(
                depot.GetQuery("delete_table"), ElementSetFactory.Create("mymodule", "myid", "myname"), null, Log);
            Console.WriteLine(sql12);

            return this;
        }
    }
}