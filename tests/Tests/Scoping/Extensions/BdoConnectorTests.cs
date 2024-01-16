using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Kernel.Tests;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Scoping.Connectors
{
    [TestFixture, Order(301)]
    public class BdoConnectorTests
    {
        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = BdoConnectorFaker.NewData();
        }

        [Test, Order(1)]
        public void CreateConnectorTest_FromMetaSet()
        {
            IBdoMetaObject meta = BdoConnectorFaker.NewMetaObject(_testData);
            var connector = SystemData.Scope.CreateConnector<ConnectorFake>(meta);

            BdoConnectorFaker.AssertFake(connector, _testData);
        }

        [Test, Order(2)]
        public void CreateConnectorTest_FromConfig()
        {
            IBdoMetaObject meta = BdoConnectorFaker.NewMetaObject(_testData);
            var connector = SystemData.Scope.CreateConnector(meta) as ConnectorFake;

            BdoConnectorFaker.AssertFake(connector, _testData);
        }

        [Test, Order(3)]
        public void CreateConnectorTest_FromObject()
        {
            var connector = new ConnectorFake
            {
                ConnectionString = _testData.connectionString,
                Host = _testData.host,
                IsSslEnabled = _testData.isSslEnabled,
                Port = BdoData.NewScalar<int?>(_testData.port as int?)
            };

            var meta = connector.ToMeta(SystemData.Scope);
            connector = SystemData.Scope.CreateConnector(meta) as ConnectorFake;

            BdoConnectorFaker.AssertFake(connector, _testData);
        }

        [Test, Order(4)]
        public void CreateConnectorTest_Pull_Push()
        {
            var connector = new ConnectorFake
            {
                ConnectionString = _testData.connectionString,
                Host = _testData.host,
                IsSslEnabled = _testData.isSslEnabled,
                Port = BdoData.NewScalar<int?>(_testData.port as int?)
            };

            connector.UsingConnection((conn, log) =>
            {
                var paramSet = BdoData.NewSet(
                    BdoData.NewObject(nameof(BdoSpec.GroupId)));
                var entities = conn.Pull(paramSet, log);

                conn.Push(null, entities?.ToArray());
            });
        }
    }
}
