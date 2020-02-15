using BindOpen.Application.Scopes;
using BindOpen.Data.Connections;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Extensions.Connectors;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.UnitTest.Extensions.Runtime
{
    [TestFixture, Order(11)]
    public class ConnectorTest
    {
        private BdoConnector _connector1 = null;
        private TestConnector _connector2 = null;

        private readonly string _filePath1 = SetupVariables.WorkingFolder + "Connector1.xml";
        private readonly string _filePath2 = SetupVariables.WorkingFolder + "Connector2.xml";

        private readonly string _connectionString1 = "<connectionString>";

        private readonly string _host2 = "myhost.com";
        private readonly int _port2 = 587;
        private readonly bool _isSslEnabled2 = false;

        [SetUp]
        public void Setup()
        {
            _connector1 = new BdoDbConnector_MSSqlServer("test", _connectionString1);
            _connector2 = SetupVariables.AppHost.CreateConnector<TestConnector>(
                new BdoConnectorConfiguration(
                    "runtime$test",
                    ElementFactory.CreateScalar("host", _host2),
                    ElementFactory.CreateScalar("port", _port2),
                    ElementFactory.CreateScalar("isSslEnabled", _isSslEnabled2)),
                "connector2");
        }

        [Test, Order(1)]
        public void TestCreateConnector()
        {
            Test1(_connector1);
            Test2(_connector2);
        }

        [Test, Order(2)]
        public void TestSaveConnector()
        {
            var log = new BdoLog();

            _connector1.SaveXml(_filePath1, log);
            _connector2.SaveXml(_filePath2, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Connector saving failed. Result was '" + xml);
        }

        [Test, Order(3)]
        public void TestLoadConnector()
        {
            var log = new BdoLog();

            if (_connector1 == null || !File.Exists(_filePath1)
                || _connector2 == null || !File.Exists(_filePath2))
            {
                TestSaveConnector();
            }

            BdoConnectorConfiguration configuration1 = XmlHelper.Load<BdoConnectorConfiguration>(_filePath1, null, null, log);
            BdoDbConnector_MSSqlServer connector1 = SetupVariables.AppHost.CreateConnector<BdoDbConnector_MSSqlServer>(configuration1, null, log);
            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Connector loading failed. Result was '" + xml);
            Test1(connector1);

            BdoConnectorConfiguration configuration2 = XmlHelper.Load<BdoConnectorConfiguration>(_filePath2, null, null, log);
            TestConnector connector2 = SetupVariables.AppHost.CreateConnector<TestConnector>(configuration2, null, log);
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Connector loading failed. Result was '" + xml);
            Test2(connector2);
        }

        [Test, Order(3)]
        public void TestCreateOpenCloseConnection()
        {
            BdoLog log = new BdoLog();

            using (var connection = SetupVariables.AppHost.Scope.Open<BdoDbConnection>("bdd1", null, log))
            {
            }
        }

        private void Test1(BdoConnector connector)
        {
            Assert.That(connector != null, "Connector missing");
            if (connector != null)
            {
                Assert.That(connector.ConnectionString == _connectionString1, "Bad connector connection string");
            }
        }

        private void Test2(BdoConnector connector)
        {
            TestConnector testConnector = connector as TestConnector;

            Assert.That(testConnector != null, "Connector missing");
            if (testConnector != null)
            {
                Assert.That(testConnector.Host == _host2, "Bad connector");
                Assert.That(testConnector.Port == _port2, "Bad connector");
                Assert.That(testConnector.IsSslEnabled == _isSslEnabled2, "Bad connector");
            }
        }
    }
}
