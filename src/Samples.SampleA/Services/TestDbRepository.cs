using BindOpen.Framework.Application.Services;

namespace BindOpen.Framework.Samples.SampleA.Services
{
    public class TestDbRepository : BdoDbService
    {
        public void Test()
        {
            this.UsingConnection(connection =>
            {
                string query = Connector.BuildSqlText(Queries.GetMyTables());
            });
        }
    }
}