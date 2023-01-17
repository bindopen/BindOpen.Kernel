using BindOpen.Extensions;
using BindOpen.Extensions.Modeling;
using BindOpen.MetaData;
using BindOpen.MetaData.Elements;
using BindOpen.Tests.Extensions;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Tests.MetaData
{
    [TestFixture, Order(200)]
    public class BdoMetaCarrierSetTests
    {
        private dynamic _testData;

        private IBdoMetaElementSet _metaCarrierSet = null;

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
            var metaCarrier1 = elemSet.Get<IBdoMetaCarrier>("carrier1");
            var metaCarrier2 = elemSet["carrier2"] as IBdoMetaCarrier;
            var metaCarrier3 = elemSet.Get<IBdoMetaCarrier>(2);
            var metaCarrier4 = elemSet.Get<IBdoMetaCarrier>("carrier4");

            Assert.That(elemSet?.Count == 4, "Bad carrier element set - Count");

            var path1 = metaCarrier1?.Item().GetItem<string>("path");
            Assert.That(
                path1 == _testData.path1
                , "Bad carrier element - Set1");

            Assert.That(
                metaCarrier2?.SubItem<string>("path") == _testData.path2
                , "Bad carrier element - Set2");

            Assert.That(
                metaCarrier3?.Item()?.GetItem<string>("path") == _testData.path3
                , "Bad carrier element - Set3");

            Assert.That(
                metaCarrier4?.SubItem<string>("path") == _testData.path4
                , "Bad carrier element - Set4");
        }

        [Test, Order(1)]
        public void CreateCarrierElementSetTest()
        {
            var metaCarrier1 = BdoMeta.NewCarrier(
                "carrier1",
                BdoExt.NewCarrierConfig(
                    "tests.core$testCarrier",
                    BdoMeta.NewScalar("path", _testData.path1)));

            var metaCarrier2 = BdoMeta.NewCarrier("carrier2", "tests.core$testCarrier")
                .WithItems((new { path = _testData.path2 }).AsElementSet<BdoCarrierConfiguration>());

            var metaCarrier3 = new CarrierFake(_testData.path3, _testData.folderPath3)?.AsMeta();

            var metaCarrier4 = BdoExt.NewCarrier<CarrierFake>(
                BdoExt.NewCarrierConfig("tests.core$testCarrier")
                    .WithItems((new { path = _testData.path4 }).AsElementSet()?.ToArray()))
                ?.AsMeta();

            _metaCarrierSet = BdoMeta.NewSet(
                metaCarrier1,
                metaCarrier2,
                metaCarrier3,
                metaCarrier4);

            Test(_metaCarrierSet);
        }
    }
}
