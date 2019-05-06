using System.IO;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.Extensions.Items.Factories;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.Data.Connections;
using BindOpen.Framework.Databases.MSSqlServer.Extensions.Connectors;
using BindOpen.Framework.Tests.UnitTest;
using NUnit.Framework;

namespace BindOpen.Framework.Tests.UnitTest.Extensions.Runtime
{
    [TestFixture, Order(11)]
    public class ConnectorTest
    {
        private Connector _connector = null;
        private readonly string _filePath = SetupVariables.WorkingFolder + "Connector.xml";

        private readonly string _connectionString = "<connectionString>";

        [SetUp]
        public void Setup()
        {
            _connector = new DatabaseConnector_MSSqlServer("test", _connectionString);
        }

        [Test, Order(1)]
        public void TestCreateConnector()
        {
            Test(_connector);
        }

        [Test, Order(2)]
        public void TestSaveConnector()
        {
            ILog log = new Log();

            _connector.SaveXml(_filePath, log);

            Assert.That(!log.HasErrorsOrExceptions(), "Connector saving failed. Result was '" + log.ToXml());
        }

        [Test, Order(3)]
        public void TestLoadConnector()
        {
            ILog log = new Log();

            if (_connector == null || !File.Exists(_filePath))
                TestSaveConnector();

            ConnectorConfiguration configuration = XmlHelper.Load<ConnectorConfiguration>(_filePath, null,null, log);
            DatabaseConnector_MSSqlServer connector = SetupVariables.AppHost.Scope.CreateConnector<DatabaseConnector_MSSqlServer>(configuration, null, log);

            Assert.That(!log.HasErrorsOrExceptions(), "Connector loading failed. Result was '" + log.ToXml());

            Test(connector);
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

        private void Test(Connector connector)
        {
            Assert.That(connector != null, "Field missing");
            if (connector != null)
            {
                Assert.That(connector.ConnectionString == _connectionString, "Bad connector connection string");
            }
        }

    }

}
