using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.IO;
using BindOpen.Kernel.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Kernel.Scoping.Connectors
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
        public void SaveXmlTest()
        {
            if (_connector == null)
            {
                IBdoMetaObject meta = BdoConnectorTests.CreateMetaConnector(_testData);
                _connector = SystemData.Scope.CreateConnector<ConnectorFake>(meta);
            }

            var isSaved = _connector.ToMeta(SystemData.Scope).ToDto().SaveXml(BdoConnectorFaker.XmlFilePath);

            Assert.That(isSaved, "Connector saving failed");
        }

        [Test, Order(3)]
        public void LoadXmlTest()
        {
            if (_connector == null || !File.Exists(BdoConnectorFaker.XmlFilePath))
            {
                SaveXmlTest();
            }

            var meta = XmlHelper.LoadXml<MetaObjectDto>(BdoConnectorFaker.XmlFilePath).ToPoco();
            var connector = SystemData.Scope.CreateConnector<ConnectorFake>(meta);

            BdoConnectorFaker.AssertFake(connector, _testData);
        }

        // Json

        [Test, Order(4)]
        public void SaveJsonTest()
        {
            if (_connector == null)
            {
                IBdoMetaObject meta = BdoConnectorTests.CreateMetaConnector(_testData);
                _connector = SystemData.Scope.CreateConnector<ConnectorFake>(meta);
            }

            var isSaved = _connector.ToMeta(SystemData.Scope).ToDto().SaveJson(BdoConnectorFaker.JsonFilePath);

            Assert.That(isSaved, "Connector saving failed");
        }

        [Test, Order(5)]
        public void LoadJsonTest()
        {
            if (_connector == null || !File.Exists(BdoConnectorFaker.JsonFilePath))
            {
                SaveJsonTest();
            }

            var meta = JsonHelper.LoadJson<MetaObjectDto>(BdoConnectorFaker.JsonFilePath).ToPoco();
            var connector = SystemData.Scope.CreateConnector<ConnectorFake>(meta);

            BdoConnectorFaker.AssertFake(connector, _testData);
        }
    }
}
