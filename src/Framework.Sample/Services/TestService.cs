using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.Data.Connections;
using BindOpen.Framework.Databases.MSSqlServer.Data.Queries.Builders;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Sample.Settings;

namespace BindOpen.Framework.Sample.Services
{
    public class TestService : THostedAppService<TestServiceSettings>
    {
        public override IAppService Start(ILog log = null)
        {
            base.Start(log);

            using (DatabaseConnection connection = Host.ConnectionService.Open<DatabaseConnection>("test.db", null, Host.Log))
            {
                if (connection != null)
                {
                    Host.Log.Append(
                        new DbQueryBuilder_MSSqlServer(Host.Scope).BuildQuery(
                            Queries_Tenants.GetOrganizations("MonTenant", "test.db"), null, out string sql1));
                }
            }

            return this;
        }
    }
}