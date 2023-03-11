using BindOpen.Data.Meta;
using BindOpen.Extensions.Connectors;
using NUnit.Framework;

namespace BindOpen.Tests.Extensions
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
                BdoConfig.New("tests.core$testConnector")
                .With(
                    BdoMeta.NewScalar("host", data.host as string),
                    BdoMeta.NewScalar("port", data.port as string),
                    BdoMeta.NewScalar("isSslEnabled", data.isSslEnabled as bool?));

            return ScopingTests.Scope.CreateConnector<ConnectorFake>(config);
        }

        [Test, Order(1)]
        public void CreateConnectorTest()
        {
            _connector = CreateConnector(_testData);

            BdoConnectorFaker.AssertFake(_connector, _testData);
        }
    }
}
