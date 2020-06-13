using BindOpen.Application.Scopes;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.Tests.Core.Fakers;
using Bogus;
using NUnit.Framework;
using System;
using System.IO;

namespace BindOpen.Tests.Core.Extensions.Connectors
{
    [TestFixture, Order(301)]
    public class ConnectorTests
    {
        private ConnectorFake _connector = null;

        private readonly string _filePath = GlobalVariables.WorkingFolder + "Connector.xml";

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();
            _testData = new
            {
                host = f.Internet.IpAddress().ToString(),
                port = f.Random.Int(800),
                isSslEnabled = f.Random.Bool()
            };
        }

        private void Test(ConnectorFake connector)
        {
            Assert.That(connector != null, "Connector missing");
            if (connector != null)
            {
                Assert.That(connector.Host == _testData.host, "Bad connector");
                Assert.That(connector.Port == _testData.port, "Bad connector");
                Assert.That(connector.IsSslEnabled == _testData.isSslEnabled, "Bad connector");
            }
        }

        [Test, Order(1)]
        public void CreateConnectorTest()
        {
            IBdoConnectorConfiguration config =
                GlobalVariables.Scope.CreateConnectorConfiguration("tests.core$testConnector")
                .WithItems(
                    ElementFactory.CreateScalar("host", _testData.host),
                    ElementFactory.CreateScalar("port", _testData.port),
                    ElementFactory.CreateScalar("isSslEnabled", _testData.isSslEnabled));

            _connector = GlobalVariables.Scope.CreateConnector<ConnectorFake>(config, "connector");

            Test(_connector);
        }

        [Test, Order(2)]
        public void SaveConnectorTest()
        {
            if (_connector == null)
            {
                CreateConnectorTest();
            }

            var log = new BdoLog();
            _connector.SaveXml(_filePath, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Connector saving failed. Result was '" + xml);
        }

        [Test, Order(3)]
        public void LoadConfigurationTest()
        {
            var log = new BdoLog();

            if (_connector == null || !File.Exists(_filePath))
            {
                SaveConnectorTest();
            }

            string xml = "";
            BdoConnectorConfiguration configuration = XmlHelper.Load<BdoConnectorConfiguration>(_filePath, log: log);
            ConnectorFake connector = GlobalVariables.Scope.CreateConnector<ConnectorFake>(configuration, null, log);
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Connector loading failed. Result was '" + xml);

            Test(connector);
        }

        [Test, Order(4)]
        public void CreateOpenCloseConnectionTest()
        {
            BdoLog log = new BdoLog();

            // check bad connection

            using (var connection = _connector?.CreateConnection(log))
            {
                connection.Disconnect();
            }

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Connection creation failed. Result was '" + xml);
        }

        [Test, Order(4)]
        public void ConnectorUsingConnectionTest()
        {
            BdoLog log = new BdoLog();

            // check bad connection

            _connector?.UsingConnection((p, l) => { }, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Connection creation failed. Result was '" + xml);

            // check bad connection

            try
            {
                _connector?.UsingConnection((p, l) => { string toto = null; int i = toto.Length; }, log);
            }
            catch(NullReferenceException ex)
            {
                log.AddException(ex);
            }

            xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(log.HasErrorsOrExceptions(), "Connection creation failed. Result was '" + xml);
        }
    }
}
