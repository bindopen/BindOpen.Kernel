using BindOpen.Extensions;
using BindOpen.Extensions.Modeling;
using BindOpen.Meta;
using BindOpen.Meta.Elements;
using BindOpen.Runtime.Tests.Extensions.Data;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Runtime.Tests.MetaData.Elements.Carrier
{
    [TestFixture, Order(200)]
    public class CarrierElementSetTests
    {
        private dynamic _testData;

        private IBdoElementSet _metaCarrierSet = null;

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

        public void Test(IBdoElementSet elementSet)
        {
            var metaCarrier1 = elementSet.GetItem<IBdoMetaCarrier>("carrier1");
            var metaCarrier2 = elementSet.GetItem<IBdoMetaCarrier>("carrier2");
            var metaCarrier3 = elementSet.Get<IBdoMetaCarrier>(2);
            var metaCarrier4 = elementSet.GetItem<IBdoMetaCarrier>("carrier4");

            Assert.That(elementSet?.Count == 4, "Bad carrier element set - Count");

            Assert.That(
                metaCarrier1?.GetFirstItem().GetItem<string>("path") == _testData.path1
                , "Bad carrier element - Set1");

            Assert.That(
                metaCarrier2?.GetFirstItem()?.GetItem<string>("path") == _testData.path2
                , "Bad carrier element - Set2");

            Assert.That(
                metaCarrier3?.GetFirstItem()?.GetItem<string>("path") == _testData.path3
                , "Bad carrier element - Set3");

            Assert.That(
                metaCarrier4?.GetFirstItem()?.GetItem<string>("path") == _testData.path4
                , "Bad carrier element - Set4");
        }

        [Test, Order(1)]
        public void CreateCarrierElementSetTest()
        {
            var metaCarrier1 = BdoMeta.NewCarrier(
                "carrier1",
                BdoExtensions.NewCarrierConfiguration(
                    "tests.core$testCarrier",
                    BdoMeta.NewScalar("path", _testData.path1)));

            var metaCarrier2 = BdoMeta.NewCarrier("carrier2", "tests.core$testCarrier")
                .WithItem((new { path = _testData.path2 }).AsElementSet<BdoCarrierConfiguration>());

            var metaCarrier3 = new CarrierFake(_testData.path3, _testData.folderPath3)?.AsMeta();

            var metaCarrier4 = BdoExtensions.NewCarrier<CarrierFake>(
                BdoExtensions.NewCarrierConfiguration("tests.core$testCarrier")
                    .WithItems((new { path = _testData.path4 }).AsElementSet()?.ToArray()))?.AsMeta();

            _metaCarrierSet = BdoMeta.NewSet(
                metaCarrier1, metaCarrier2, metaCarrier3, metaCarrier4);

            Test(_metaCarrierSet);
        }
    }
}
