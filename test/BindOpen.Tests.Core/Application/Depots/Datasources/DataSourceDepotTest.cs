using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Data.Items;
using BindOpen.Data.Stores;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.Core.Application.Depots.Datasources
{
    [TestFixture, Order(21)]
    public class DatasourceDepotTests
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "DatasourceDepot.xml";

        private BdoDatasourceDepot _dataSourceDepot = null;

        [SetUp]
        public void OneTimeSetUp()
        {
            _dataSourceDepot = DataItemFactory.CreateSet<BdoDatasourceDepot, IDatasource>(
                DataItemFactory.CreateDatasource("smtp_default", DatasourceKind.EmailServer)
                    .WithConfiguration(
                        new BdoConnectorConfiguration(
                            "messages$smtp",
                            ElementFactory.CreateScalar("host", "smtp.test.com"),
                            ElementFactory.CreateScalar("port", DataValueType.Integer, "587"),
                            ElementFactory.CreateScalar("isDefaultCredentialsUsed", DataValueType.Boolean, true),
                            ElementFactory.CreateScalar("isSslEnabled", DataValueType.Boolean, true),
                            ElementFactory.CreateScalar("timeout", DataValueType.Integer, 60000),
                            ElementFactory.CreateScalar("login", "login@test.com"),
                            ElementFactory.CreateScalar("password", "passwordA"))));
        }

        public static void TestDatasourceDataStore(BdoDatasourceDepot depot)
        {
            Assert.That(
                depot.HasItem("smtp_default")
                && depot.Get("smtp_default")?.Configurations?.Count == 1
                && depot.Get("smtp_default")?.Configurations[0]?.GetValue<int>("timeout") == 60000
                , "Bad data source depot");
        }

        [Test]
        [Order(1)]
        public void CreateDatasourceDataStoreTest()
        {
            TestDatasourceDataStore(_dataSourceDepot);
        }

        [Test]
        [Order(2)]
        public void SaveDatasourceDepotTest()
        {
            var log = new BdoLog();

            _dataSourceDepot.SaveXml(_filePath, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Data source depots saving failed. Result was '" + xml);
        }

        [Test]
        [Order(3)]
        public void LoadDatasourceDepotTest()
        {
            var log = new BdoLog();

            if (_dataSourceDepot == null || !File.Exists(_filePath))
            {
                SaveDatasourceDepotTest();
            }

            var dataSourceDepot = XmlHelper.Load<BdoDatasourceDepot>(_filePath, log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Data source depot loading failed. Result was '" + xml);

            TestDatasourceDataStore(dataSourceDepot);
        }
    }
}
