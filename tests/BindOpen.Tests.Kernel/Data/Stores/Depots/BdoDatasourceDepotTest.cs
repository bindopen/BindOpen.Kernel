using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Stores;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Tests.Data
{
    [TestFixture, Order(101)]
    public class BdoDatasourceDepotTest
    {
        private readonly string _filePath_xml = Tests.WorkingFolder + "DatasourceDepot.xml";
        private readonly string _filePath_json = Tests.WorkingFolder + "DatasourceDepot.json";

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
            Assert.That(depot["smtp_default"]?.Get()?.GetData<int>("port") == _testData.port, "Bad integer");
            Assert.That(depot.Descendant<IBdoConfiguration>("smtp_default", null)?.GetData<bool>("isDefaultCredentialsUsed") == _testData.isDefaultCredentialsUsed, "Bad boolean");
            Assert.That(depot["smtp_default"]["messages$smtp"]?.GetData<bool>("isSslEnabled") == _testData.isSslEnabled, "Bad boolean");
            Assert.That(depot.Get("smtp_default")?.Get()?.GetData<int>("timeout") == _testData.timeout, "Bad integer");
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
                    BdoConfig.New(
                        "messages$smtp",
                        BdoMeta.NewScalar("host", _testData.host),
                        BdoMeta.NewScalar("port", DataValueTypes.Integer, _testData.port),
                        BdoMeta.NewScalar("isDefaultCredentialsUsed", DataValueTypes.Boolean, _testData.isDefaultCredentialsUsed),
                        BdoMeta.NewScalar("isSslEnabled", DataValueTypes.Boolean, _testData.isSslEnabled),
                        BdoMeta.NewScalar("timeout", DataValueTypes.Integer, _testData.timeout),
                        BdoMeta.NewScalar("login", _testData.login),
                        BdoMeta.NewScalar("password", _testData.password))));

            TestBdoDatasourceDepot(_datasourceDepot);
        }
    }
}
