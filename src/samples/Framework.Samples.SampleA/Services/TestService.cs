using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.MSSqlServer.Data.Queries.Builders;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Samples.SampleA.Settings;
using System;

namespace BindOpen.Framework.Samples.SampleA.Services
{
    public class TestService : THostedAppService<TestServiceSettings>
    {
        public override IAppService Start(ILog log = null)
        {
            base.Start(log);

            var element = ElementFactory.CreateScalar(Core.Data.Common.DataValueType.Boolean, false);

            Host.Log.Append(
                new DbQueryBuilder_MSSqlServer(Host.Scope).BuildQuery(
                    Queries.UpdateMyTable(new DbMyTable() { Name = "nameA" }, null), null, out string sql4));
            Console.WriteLine(sql4);

            Host.Log.Append(
                new DbQueryBuilder_MSSqlServer(Host.Scope).BuildQuery(
                    Queries.GetMyTables("", null), null, out string sql1));

            Console.WriteLine(sql1);

            Host.Log.Append(
            new DbQueryBuilder_MSSqlServer(Host.Scope).BuildQuery(
                Queries.GetMyTable("name", null), null, out string sql2));

            Console.WriteLine(sql2);

            Host.Log.Append(
                new DbQueryBuilder_MSSqlServer(Host.Scope).BuildQuery(
                    Queries.DeleteMyTable("", null), null, out string sql3));

            Console.WriteLine(sql3);

            return this;
        }
    }
}