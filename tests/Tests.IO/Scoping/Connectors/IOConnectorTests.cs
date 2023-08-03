using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.System.Scoping
{
    [TestFixture, Order(301)]
    public class IOConnectorTests
    {
        private ConnectorFake _connector = null;

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = BdoConnectorFaker.Fake();
        }

        // Xml

        [Test, Order(2)]
        public void SaveXmlConnectorTest()
        {
            if (_connector == null)
            {
                IBdoConfiguration config = BdoConnectorTests.CreateConnectorConfig(_testData);
                _connector = SystemData.Scope.CreateConnector<ConnectorFake>(config);
            }

            var isSaved = _connector.ToConfig(SystemData.Scope).ToDto().SaveXml(BdoConnectorFaker.XmlFilePath);

            Assert.That(isSaved, "Connector saving failed");
        }

        [Test, Order(3)]
        public void LoadXmlConnectorTest()
        {
            if (_connector == null || !File.Exists(BdoConnectorFaker.XmlFilePath))
            {
                SaveXmlConnectorTest();
            }

            var config = XmlHelper.LoadXml<ConfigurationDto>(BdoConnectorFaker.XmlFilePath).ToPoco();
            var connector = SystemData.Scope.CreateConnector<ConnectorFake>(config);

            BdoConnectorFaker.AssertFake(connector, _testData);
        }

        // Json

        [Test, Order(4)]
        public void SaveJsonConnectorTest()
        {
            if (_connector == null)
            {
                IBdoConfiguration config = BdoConnectorTests.CreateConnectorConfig(_testData);
                _connector = SystemData.Scope.CreateConnector<ConnectorFake>(config);
            }

            var isSaved = _connector.ToConfig(SystemData.Scope).ToDto().SaveJson(BdoConnectorFaker.JsonFilePath);

            Assert.That(isSaved, "Connector saving failed");
        }

        [Test, Order(5)]
        public void LoadJsonConnectorTest()
        {
            if (_connector == null || !File.Exists(BdoConnectorFaker.JsonFilePath))
            {
                SaveJsonConnectorTest();
            }

            var config = JsonHelper.LoadJson<ConfigurationDto>(BdoConnectorFaker.JsonFilePath).ToPoco();
            var connector = SystemData.Scope.CreateConnector<ConnectorFake>(config);

            BdoConnectorFaker.AssertFake(connector, _testData);
        }
    }
}
