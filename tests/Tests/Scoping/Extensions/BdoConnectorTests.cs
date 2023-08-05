using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Scoping
{
    [TestFixture, Order(301)]
    public class BdoConnectorTests
    {
        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = BdoConnectorFaker.Fake();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="data"></param>
        /// <returns></returns>
        public static IBdoMetaObject CreateMetaConnector(dynamic data)
        {
            var config =
                BdoData.NewMetaObject()
                .WithDataType(BdoExtensionKind.Connector, "bindopen.system.tests$testConnector")
                .With(
                    BdoData.NewMetaScalar("host", data.host as string),
                    BdoData.NewMetaScalar("port", data.port as int?),
                    BdoData.NewMetaScalar("isSslEnabled", data.isSslEnabled as bool?));

            return config;
        }

        [Test, Order(1)]
        public void CreateConnectorTest_FromMetaSet()
        {
            IBdoMetaObject meta = CreateMetaConnector(_testData);
            var connector = SystemData.Scope.CreateConnector<ConnectorFake>(meta);

            BdoConnectorFaker.AssertFake(connector, _testData);
        }

        [Test, Order(2)]
        public void CreateConnectorTest_FromConfig()
        {
            IBdoMetaObject meta = CreateMetaConnector(_testData);
            var connector = SystemData.Scope.CreateConnector(meta) as ConnectorFake;

            BdoConnectorFaker.AssertFake(connector, _testData);
        }

        [Test, Order(3)]
        public void CreateConnectorTest_FromObject()
        {
            var connector = new ConnectorFake
            {
                ConnectionString = _testData.connecString,
                Host = _testData.host,
                IsSslEnabled = _testData.isSslEnabled,
                Port = BdoData.NewMetaScalar<int?>(_testData.port as int?)
            };

            var meta = connector.ToMeta(SystemData.Scope);
            connector = SystemData.Scope.CreateConnector(meta) as ConnectorFake;

            BdoConnectorFaker.AssertFake(connector, _testData);
        }
    }
}
