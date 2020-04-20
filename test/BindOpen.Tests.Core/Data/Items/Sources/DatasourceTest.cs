using BindOpen.Application.Scopes;
using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using NUnit.Framework;

namespace BindOpen.Tests.Core.Data.Items
{
    [TestFixture]
    public class DatasourceTest
    {
        private IBdoScope _scope = null;

        [SetUp]
        public void OneTimeSetUp()
        {
            _scope = GlobalVariables.Scope;
        }

        [Test]
        public void TestDatasource()
        {
            IBdoLog log = new BdoLog();
            var datasource = DataItemFactory.CreateDatasource("name", DatasourceKind.Database)
                .WithConfiguration(
                    (GlobalVariables.Scope?.CreateConnectorConfiguration("tests.core$test", log)
                        as BdoConnectorConfiguration)?.WithConnectionString("connectionString"));

            Assert.That(
                datasource != null, "Bad data source creation");
        }
    }
}
