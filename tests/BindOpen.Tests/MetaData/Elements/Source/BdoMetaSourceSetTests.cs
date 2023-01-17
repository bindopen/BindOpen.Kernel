using BindOpen.Extensions;
using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.MetaData;
using BindOpen.MetaData.Elements;
using BindOpen.Tests.Extensions;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Tests.MetaData
{
    [TestFixture, Order(200)]
    public class BdoMetaSourceSetTests
    {
        private dynamic _testData;

        private IBdoMetaElementSet _metaSourceSet = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();
            _testData = new
            {
                path1 = f.Random.Word() + "_1.txt",
                path2 = f.Random.Word() + "_2.txt",
                path3 = f.Random.Word() + "_3.txt",
                folderPath3 = f.Random.Word() + "_3.txt",
                path4 = f.Random.Word() + "_4.txt"
            };
        }

        public void Test(IBdoMetaElementSet elemSet)
        {
            var metaSource1 = elemSet.Get<IBdoMetaSource>("source1");
            var metaSource2 = elemSet.Get<IBdoMetaSource>("source2");
            var metaSource3 = elemSet.Get<IBdoMetaSource>(2);
            var metaSource4 = elemSet.GetSource("source4");

            Assert.That(elemSet?.Count == 4, "Bad source element set - Count");

            Assert.That(
                metaSource1?.Item().GetItem<string>("path") == _testData.path1
                , "Bad source element - Set1");

            Assert.That(
                metaSource2?.Item()?.GetItem<string>("path") == _testData.path2
                , "Bad source element - Set2");

            Assert.That(
                metaSource3?.Item()?.GetItem<string>("path") == _testData.path3
                , "Bad source element - Set3");

            Assert.That(
                metaSource4?.Item()?.GetItem<string>("path") == _testData.path4
                , "Bad source element - Set4");
        }

        [Test, Order(1)]
        public void CreateSourceElementSetTest()
        {
            var metaSource1 = BdoMeta.NewSource(
                "source1",
                BdoExt.NewConnectorConfig(
                    "tests.core$testSource",
                    BdoMeta.NewScalar("path", _testData.path1)));

            var metaSource2 = BdoMeta.NewSource("source2", "tests.core$testConnector")
                .WithItem((new { path = _testData.path2 }).AsElementSet<BdoConnectorConfiguration>());

            var metaSource3 = new ConnectorFake()?.AsMeta();

            var metaSource4 = BdoExt.NewConnector<ConnectorFake>(
                BdoExt.NewConnectorConfig("tests.core$testSource")
                    .WithItems((new { path = _testData.path4 }).AsElementSet()?.ToArray()))
                ?.AsMeta();

            _metaSourceSet = BdoMeta.NewSet(
                metaSource1,
                metaSource2,
                metaSource3,
                metaSource4);

            Test(_metaSourceSet);
        }
    }
}
