using BindOpen.Application.Scopes;
using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using NUnit.Framework;

namespace BindOpen.Tests.Core.Data.Items
{
    [TestFixture, Order(210)]
    public class DatasourceTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void CreateDatasourceTest()
        {
            IBdoLog log = new BdoLog();
            var datasource = ItemFactory.CreateDatasource("name", DatasourceKind.Database)
                .WithConfiguration(
                    (GlobalVariables.Scope?.CreateConnectorConfiguration("tests.core$test", log)
                        as BdoConnectorConfiguration)?.WithConnectionString("connectionString"));

            Assert.That(
                datasource != null, "Bad data source creation");
        }
    }
}
