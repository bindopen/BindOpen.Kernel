using BindOpen.Framework.Application.Repositories;

namespace BindOpen.Framework.Samples.SampleA.Services
{
    public class TestDbRepository : BdoDbRepository
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