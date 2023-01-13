using BindOpen.Extensions;
using BindOpen.MetaData;
using BindOpen.MetaData.Items;
using NUnit.Framework;

namespace BindOpen.Tests.MetaData
{
    [TestFixture, Order(210)]
    public class BdoSourceTests
    {
        private IBdoDataSource _datasource;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void CreateDatasourceTest()
        {
            _datasource = BdoMeta.NewDatasource("name", DatasourceKind.Database)
                .WithConfig(
                    BdoExt.NewConnectorConfig("tests.core$test")
                        .WithConnectionString("connectionString"));

            Assert.That(
                _datasource != null, "Bad data source creation");
        }

        public static void Test(IBdoDataSource source)
        {
            Assert.That(
                source.GetConfig("tests.core$test") != null, "Datasource - Configuration not found");

            Assert.That(
                source.GetConfig("tests.core$test").GetConnectionString() == "connectionString", "Datasource - Configuration not found");
        }
    }
}
