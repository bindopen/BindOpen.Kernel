using System.IO;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Extensions.Items;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.Data.Connections;
using BindOpen.Framework.Databases.MSSqlServer.Extensions.Connectors;
using BindOpen.Framework.Runtime.Extensions.Connectors;
using NUnit.Framework;

namespace BindOpen.Framework.Tests.UnitTest.Extensions.Runtime
{
    [TestFixture, Order(11)]
    public class ConnectorTest
    {
        private Connector _connector1 = null;
        private Connector _connector2 = null;

        private readonly string _filePath1 = SetupVariables.WorkingFolder + "Connector1.xml";
        private readonly string _filePath2 = SetupVariables.WorkingFolder + "Connector2.xml";

        private readonly string _connectionString1 = "<connectionString>";

        private readonly string _host2 = "myhost.com";
        private readonly int _port2 = 587;
        private readonly bool _isSslEnabled2 = false;

        [SetUp]
        public void Setup()
        {
            _connector1 = new DatabaseConnector_MSSqlServer("test", _connectionString1);
            _connector2 = SetupVariables.AppHost.Scope.CreateConnector<TestConnector>(
                new ConnectorConfiguration(
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
            ILog log = new Log();

            _connector1.SaveXml(_filePath1, log);
            _connector2.SaveXml(_filePath2, log);

            Assert.That(!log.HasErrorsOrExceptions(), "Connector saving failed. Result was '" + log.ToXml());
        }

        [Test, Order(3)]
        public void TestLoadConnector()
        {
            ILog log = new Log();

            if (_connector1 == null || !File.Exists(_filePath1)
                || _connector2 == null || !File.Exists(_filePath2))
            {
                TestSaveConnector();
            }

            ConnectorConfiguration configuration1 = XmlHelper.Load<ConnectorConfiguration>(_filePath1, null,null, log);
            DatabaseConnector_MSSqlServer connector1 = SetupVariables.AppHost.Scope.CreateConnector<DatabaseConnector_MSSqlServer>(configuration1, null, log);
            Assert.That(!log.HasErrorsOrExceptions(), "Connector loading failed. Result was '" + log.ToXml());
            Test1(connector1);

            ConnectorConfiguration configuration2 = XmlHelper.Load<ConnectorConfiguration>(_filePath2, null, null, log);
            TestConnector connector2 = SetupVariables.AppHost.Scope.CreateConnector<TestConnector>(configuration2, null, log);
            Assert.That(!log.HasErrorsOrExceptions(), "Connector loading failed. Result was '" + log.ToXml());
            Test2(connector2);
        }

        [Test, Order(3)]
        public void TestCreateOpenCloseConnection()
        {
            Log log = new Log();

            using (DatabaseConnection connection =
                SetupVariables.AppHost.ConnectionService.Open<DatabaseConnection>("bdd1", null, log))
            {
            }
        }

        private void Test1(Connector connector)
        {
            Assert.That(connector != null, "Connector missing");
            if (connector != null)
            {
                Assert.That(connector.ConnectionString == _connectionString1, "Bad connector connection string");
            }
        }

        private void Test2(Connector connector)
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
