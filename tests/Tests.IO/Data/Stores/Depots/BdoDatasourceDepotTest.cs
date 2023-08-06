using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using Bogus;
using NUnit.Framework;
using System.IO;

namespace BindOpen.System.Data.Stores
{
    [TestFixture, Order(101)]
    public class DatasourceDepotTests
    {
        private readonly string _filePath_xml = SystemData.WorkingFolder + "DatasourceDepot.xml";
        private readonly string _filePath_json = SystemData.WorkingFolder + "DatasourceDepot.json";

        private IBdoSourceDepot _datasourceDepot = null;

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

        private void TestBdoDatasourceDepot(IBdoSourceDepot depot)
        {
            Assert.That(depot.Has("smtp_default"), "Error with item existence check");
            Assert.That(depot.Get("smtp_default")?.Count == 1, "Bad config count");
            Assert.That(depot.Get("smtp_default")?.Get()?.GetData<string>("host") == _testData.host, "Bad string");
            Assert.That(depot["smtp_default"]?.Descendant<IBdoMetaData>(0, "port")?.GetData<int>() == _testData.port, "Bad integer");
            Assert.That(depot.Descendant<IBdoMetaObject>("smtp_default", 0)?.GetData<bool>("isDefaultCredentialsUsed") == _testData.isDefaultCredentialsUsed, "Bad boolean");
            Assert.That(depot.Get("smtp_default")?.Get()?.GetData<bool>("isSslEnabled") == _testData.isSslEnabled, "Bad boolean");
            Assert.That(depot.Descendant<IBdoMetaData>("smtp_default", 0, "timeout")?.GetData<int>() == _testData.timeout, "Bad integer");
            Assert.That(depot.Get("smtp_default")?.Get()?.GetData<string>("login") == _testData.login, "Bad string");
            Assert.That(depot.Get("smtp_default")?.Get()?.GetData<string>("password") == _testData.password, "Bad string");
        }

        [Test, Order(1)]
        public void CreateBdoDatasourceDepotTest()
        {
            _datasourceDepot = BdoData.NewDatasourceDepot(
                BdoData.NewDatasource(
                    "smtp_default",
                    DatasourceKind.EmailServer,
                    BdoData.NewMetaObject("messages$smtp")
                        .With(
                            BdoData.NewMetaScalar("host", _testData.host as string),
                            BdoData.NewMetaScalar("port", DataValueTypes.Integer, (int)_testData.port),
                            BdoData.NewMetaScalar("isDefaultCredentialsUsed", DataValueTypes.Boolean, (bool)_testData.isDefaultCredentialsUsed),
                            BdoData.NewMetaScalar("isSslEnabled", DataValueTypes.Boolean, (bool)_testData.isSslEnabled),
                            BdoData.NewMetaScalar("timeout", DataValueTypes.Integer, (int)_testData.timeout),
                            BdoData.NewMetaScalar("login", _testData.login as string),
                            BdoData.NewMetaScalar("password", _testData.password as string))
                )
            );

            TestBdoDatasourceDepot(_datasourceDepot);
        }

        // Xml

        [Test, Order(2)]
        public void SaveXmlBdoDatasourceDepotTest()
        {
            if (_datasourceDepot == null)
            {
                CreateBdoDatasourceDepotTest();
            }

            var isSaved = _datasourceDepot.ToDto().SaveXml(_filePath_xml);
            Assert.That(isSaved, "Meta list saving failed");

            TestBdoDatasourceDepot(_datasourceDepot);
        }

        [Test, Order(3)]
        public void LoadXmlBdoDatasourceDepotTest()
        {
            if (_datasourceDepot == null || !File.Exists(_filePath_xml))
            {
                SaveXmlBdoDatasourceDepotTest();
            }

            var depot = XmlHelper.LoadXml<BdoDatasourceDepotDto>(_filePath_xml).ToPoco();

            TestBdoDatasourceDepot(depot);
        }

        // Json

        [Test, Order(4)]
        public void SaveJsonBdoDatasourceDepotTest()
        {
            if (_datasourceDepot == null)
            {
                CreateBdoDatasourceDepotTest();
            }

            var isSaved = _datasourceDepot.ToDto().SaveJson(_filePath_json);
            Assert.That(isSaved, "Meta list saving failed");

            TestBdoDatasourceDepot(_datasourceDepot);
        }

        [Test, Order(5)]
        public void LoadJsonBdoDatasourceDepotTest()
        {
            if (_datasourceDepot == null || !File.Exists(_filePath_json))
            {
                SaveJsonBdoDatasourceDepotTest();
            }

            var depot = JsonHelper.LoadJson<BdoDatasourceDepotDto>(_filePath_json).ToPoco();

            TestBdoDatasourceDepot(depot);
        }
    }
}
