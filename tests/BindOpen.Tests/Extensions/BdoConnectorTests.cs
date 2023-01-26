using BindOpen.Data;
using BindOpen.Extensions;
using BindOpen.Extensions.Connecting;
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
        /// <param name="data"></param>
        /// <returns></returns>
        public static IBdoConnector CreateConnector(dynamic data)
        {
            IBdoConnectorConfiguration config =
                BdoExt.NewConnectorConfig("tests.core$testConnector")
                .WithItems(
                    BdoMeta.NewScalar("host", data.host),
                    BdoMeta.NewScalar("port", data.port),
                    BdoMeta.NewScalar("isSslEnabled", data.isSslEnabled));

            return BdoExt.NewConnector<ConnectorFake>(config);
        }

        [Test, Order(1)]
        public void CreateConnectorTest()
        {
            _connector = CreateConnector(_testData);

            BdoConnectorFaker.AssertFake(_connector, _testData);
        }
    }
}
