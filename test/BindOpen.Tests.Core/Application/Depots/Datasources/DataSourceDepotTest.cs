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
    public class DatasourceDepotTest
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "DatasourceDepot.xml";

        private BdoDatasourceDepot _dataSourceDepot = null;

        [SetUp]
        public void Setup()
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

        public void TestDatasourceDataStore(BdoDatasourceDepot depot)
        {
            Assert.That(
                depot.HasItem("smtp_default")
                && depot.GetItem("smtp_default")?.Configurations?.Count == 1
                && depot.GetItem("smtp_default")?.Configurations[0]?.GetElementObject<int>("timeout") == 60000
                , "Bad data source depot");
        }

        [Test]
        public void TestCreateDatasourceDataStore()
        {
            TestDatasourceDataStore(_dataSourceDepot);
        }

        [Test]
        public void TestSaveDatasourceDepot()
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
        public void TestLoadDatasourceDepot()
        {
            var log = new BdoLog();

            if (_dataSourceDepot == null || !File.Exists(_filePath))
                TestSaveDatasourceDepot();

            var dataSourceDepot = XmlHelper.Load<BdoDatasourceDepot>(_filePath, null, null, log);

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
