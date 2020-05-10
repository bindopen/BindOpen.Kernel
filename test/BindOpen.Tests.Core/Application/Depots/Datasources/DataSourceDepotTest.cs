using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Data.Items;
using BindOpen.Data.Stores;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using Bogus;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.Core.Application.Depots.Datasources
{
    [TestFixture, Order(101)]
    public class DatasourceDepotTests
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "DatasourceDepot.xml";

        private BdoDatasourceDepot _datasourceDepot = null;

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();
            _testData = new
            {
                host = f.Internet.IpAddress().ToString(),
                port = f.Random.Int(1000),
                isDefaultCredentialsUsed = f.Random.Bool(),
                isSslEnabled = f.Random.Bool(),
                timeout = f.Random.Int(8000),
                login = f.Person.Email,
                password = f.Lorem.Word()
            };
        }

        private void TestDatasourceDataStore(BdoDatasourceDepot depot)
        {
            Assert.That(depot.HasItem("smtp_default"), "Error with item existence check");
            Assert.That(depot.Get("smtp_default")?.Configurations?.Count == 1, "Bad configuration count");
            Assert.That(depot.Get("smtp_default")?.Configurations[0]?.GetValue<string>("host") == _testData.host, "Bad string");
            Assert.That(depot.Get("smtp_default")?.Configurations[0]?.GetValue<int>("port") == _testData.port, "Bad integer");
            Assert.That(depot.Get("smtp_default")?.Configurations[0]?.GetValue<bool>("isDefaultCredentialsUsed") == _testData.isDefaultCredentialsUsed, "Bad boolean");
            Assert.That(depot.Get("smtp_default")?.Configurations[0]?.GetValue<bool>("isSslEnabled") == _testData.isSslEnabled, "Bad boolean");
            Assert.That(depot.Get("smtp_default")?.Configurations[0]?.GetValue<int>("timeout") == _testData.timeout, "Bad integer");
            Assert.That(depot.Get("smtp_default")?.Configurations[0]?.GetValue<string>("login") == _testData.login, "Bad string");
            Assert.That(depot.Get("smtp_default")?.Configurations[0]?.GetValue<string>("password") == _testData.password, "Bad string");
        }

        [Test, Order(1)]
        public void CreateDatasourceDataStoreTest()
        {
            var config =
                BdoExtensionFactory.CreateConnectorConfiguration("messages$smtp")
                    .WithItems(
                        ElementFactory.CreateScalar("host", _testData.host),
                        ElementFactory.CreateScalar("port", DataValueTypes.Integer, _testData.port),
                        ElementFactory.CreateScalar("isDefaultCredentialsUsed", DataValueTypes.Boolean, _testData.isDefaultCredentialsUsed),
                        ElementFactory.CreateScalar("isSslEnabled", DataValueTypes.Boolean, _testData.isSslEnabled),
                        ElementFactory.CreateScalar("timeout", DataValueTypes.Integer, _testData.timeout),
                        ElementFactory.CreateScalar("login", _testData.login),
                        ElementFactory.CreateScalar("password", _testData.password));

            _datasourceDepot = ItemFactory.CreateSet<BdoDatasourceDepot, IDatasource>(
                ItemFactory.CreateDatasource("smtp_default", DatasourceKind.EmailServer)
                    .WithConfiguration(config));

            TestDatasourceDataStore(_datasourceDepot);
        }

        [Test, Order(2)]
        public void SaveDatasourceDepotTest()
        {
            var log = new BdoLog();

            if (_datasourceDepot == null)
            {
                CreateDatasourceDataStoreTest();
            }

            _datasourceDepot.SaveXml(_filePath, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Data source depots saving failed. Result was '" + xml);
        }

        [Test, Order(3)]
        public void LoadDatasourceDepotTest()
        {
            var log = new BdoLog();

            if (_datasourceDepot == null || !File.Exists(_filePath))
            {
                SaveDatasourceDepotTest();
            }

            var datasourceDepot = XmlHelper.Load<BdoDatasourceDepot>(_filePath, log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Data source depot loading failed. Result was '" + xml);

            TestDatasourceDataStore(datasourceDepot);
        }
    }
}
