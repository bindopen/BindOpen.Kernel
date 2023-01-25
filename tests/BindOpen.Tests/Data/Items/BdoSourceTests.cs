using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Extensions;
using NUnit.Framework;

namespace BindOpen.Tests.Data
{
    [TestFixture, Order(210)]
    public class BdoSourceTests
    {
        private IBdoDatasource _datasource;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void CreateDatasourceTest()
        {
            _datasource = BdoData.NewDatasource("name", DatasourceKind.Database)
                .WithConfig(
                    BdoExt.NewConnectorConfig("tests.core$test")
                        .WithConnectionString("connectionString"));

            Assert.That(
                _datasource != null, "Bad data source creation");
        }

        public static void Test(IBdoDatasource source)
        {
            Assert.That(
                source.GetConfig("tests.core$test") != null, "Datasource - Configuration not found");

            Assert.That(
                source.GetConfig("tests.core$test").GetConnectionString() == "connectionString", "Datasource - Configuration not found");
        }
    }
}
