using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Scoping.Connectors
{
    [TestFixture, Order(301)]
    public class BdoConnectorTests
    {
        private ConnectorFake _connector = null;

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
        public static IBdoConnector CreateConnector(dynamic data)
        {
            var config =
                BdoData.NewConfig()
                .WithDefinition("bindopen.system.tests$testConnector")
                .With(
                    BdoData.NewMetaScalar("host", data.host as string),
                    BdoData.NewMetaScalar("port", data.port as int?),
                    BdoData.NewMetaScalar("isSslEnabled", data.isSslEnabled as bool?));

            return SystemData.Scope.CreateConnector<ConnectorFake>(config);
        }

        [Test, Order(1)]
        public void CreateConnectorTest()
        {
            _connector = CreateConnector(_testData);

            BdoConnectorFaker.AssertFake(_connector, _testData);
        }
    }
}
