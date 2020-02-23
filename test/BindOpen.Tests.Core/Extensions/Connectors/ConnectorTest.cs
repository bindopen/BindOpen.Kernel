using BindOpen.Application.Scopes;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.Core.Extensions.Connectors
{
    [TestFixture, Order(11)]
    public class ConnectorTest
    {
        private TestConnector _connector = null;

        private readonly string _filePath = GlobalVariables.WorkingFolder + "Connector.xml";

        private readonly string _host = "myhost.com";
        private readonly int _port = 587;
        private readonly bool _isSslEnabled = false;

        [SetUp]
        public void Setup()
        {
            _connector = GlobalVariables.Scope.CreateConnector<TestConnector>(
                new BdoConnectorConfiguration(
                    "tests.core$test",
                    ElementFactory.CreateScalar("host", _host),
                    ElementFactory.CreateScalar("port", _port),
                    ElementFactory.CreateScalar("isSslEnabled", _isSslEnabled)),
                "connector");
        }

        [Test, Order(1)]
        public void TestCreateConnector()
        {
            Test(_connector);
        }

        [Test, Order(2)]
        public void TestSaveConnector()
        {
            var log = new BdoLog();

            _connector.SaveXml(_filePath, log);

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

            if (_connector == null || !File.Exists(_filePath))
            {
                TestSaveConnector();
            }

            string xml = "";
            BdoConnectorConfiguration configuration = XmlHelper.Load<BdoConnectorConfiguration>(_filePath, null, null, log);
            TestConnector connector = GlobalVariables.Scope.CreateConnector<TestConnector>(configuration, null, log);
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Connector loading failed. Result was '" + xml);
            Test(connector);
        }

        [Test, Order(3)]
        public void TestCreateOpenCloseConnection()
        {
            BdoLog log = new BdoLog();

            using (var connection = _connector?.CreateConnection(log))
            {
                connection.Disconnect();
            }

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Connection creation failed. Result was '" + xml);
        }

        private void Test(BdoConnector connector)
        {
            TestConnector testConnector = connector as TestConnector;

            Assert.That(testConnector != null, "Connector missing");
            if (testConnector != null)
            {
                Assert.That(testConnector.Host == _host, "Bad connector");
                Assert.That(testConnector.Port == _port, "Bad connector");
                Assert.That(testConnector.IsSslEnabled == _isSslEnabled, "Bad connector");
            }
        }
    }
}
