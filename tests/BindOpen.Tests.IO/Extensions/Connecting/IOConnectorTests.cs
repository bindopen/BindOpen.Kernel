using BindOpen.Extensions;
using BindOpen.Extensions.Connecting;
using BindOpen.Data;
using BindOpen.Data.Elements;
using BindOpen.Runtime.Tests.Extensions;
using BindOpen.Runtime.Tests.Extensions.Connecting;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Runtime.IO.Tests.Extensions.Connecting
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
                _connector = BdoConnectorTests.CreateConnector(_testData);
            }

            var isSaved = _connector.ToDto().SaveXml(BdoConnectorFaker.XmlFilePath);

            Assert.That(isSaved, "Connector saving failed");
        }

        [Test, Order(3)]
        public void LoadXmlConnectorTest()
        {
            if (_connector == null || !File.Exists(BdoConnectorFaker.XmlFilePath))
            {
                SaveXmlConnectorTest();
            }

            var configuration = XmlHelper.LoadXml<BdoConnectorConfigurationDto>(BdoConnectorFaker.XmlFilePath).ToPoco();
            ConnectorFake connector = BdoExtensions.NewConnector<ConnectorFake>(configuration);

            BdoConnectorFaker.AssertFake(connector, _testData);
        }

        // Json

        [Test, Order(4)]
        public void SaveJsonConnectorTest()
        {
            if (_connector == null)
            {
                _connector = BdoConnectorTests.CreateConnector(_testData);
            }

            var isSaved = _connector.ToDto().SaveJson(BdoConnectorFaker.JsonFilePath);

            Assert.That(isSaved, "Connector saving failed");
        }

        [Test, Order(5)]
        public void LoadJsonConnectorTest()
        {
            if (_connector == null || !File.Exists(BdoConnectorFaker.JsonFilePath))
            {
                SaveJsonConnectorTest();
            }

            var configuration = JsonHelper.LoadJson<BdoConnectorConfigurationDto>(BdoConnectorFaker.JsonFilePath).ToPoco();
            ConnectorFake connector = BdoExtensions.NewConnector<ConnectorFake>(configuration);

            BdoConnectorFaker.AssertFake(connector, _testData);
        }
    }
}
