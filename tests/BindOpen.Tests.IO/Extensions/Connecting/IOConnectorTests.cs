using BindOpen.Data;
using BindOpen.Data.Configuration;
using BindOpen.Data.Meta;
using BindOpen.Extensions;
using BindOpen.Extensions.Connecting;
using BindOpen.Tests.Extensions;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.IO.Extensions
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

            var config = XmlHelper.LoadXml<ConfigurationDto>(BdoConnectorFaker.XmlFilePath).ToPoco(null);
            ConnectorFake connector = Bdo.NewConnector<ConnectorFake>(config);

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

            var config = JsonHelper.LoadJson<ConfigurationDto>(BdoConnectorFaker.JsonFilePath).ToPoco(null);
            ConnectorFake connector = Bdo.NewConnector<ConnectorFake>(config);

            BdoConnectorFaker.AssertFake(connector, _testData);
        }
    }
}
