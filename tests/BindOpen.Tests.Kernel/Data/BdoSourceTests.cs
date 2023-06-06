using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Connectors;
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
                .With(
                    BdoMeta.NewConfig("bindopen.tests.kernel$test")
                        .WithConnectionString("connectionString"));

            Assert.That(
                _datasource != null, "Bad data source creation");
        }

        public static void Test(IBdoDatasource source)
        {
            Assert.That(
                source.Get("bindopen.tests.kernel$test") != null, "Datasource - Configuration not found");

            Assert.That(
                source.Get("bindopen.tests.kernel$test").GetConnectionString() == "connectionString", "Datasource - Configuration not found");
        }
    }
}
