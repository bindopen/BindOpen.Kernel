using BindOpen.Extensions;
using BindOpen.Extensions.Modeling;
using BindOpen.MetaData;
using BindOpen.MetaData.Elements;
using BindOpen.Tests.Extensions;
using Bogus;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.IO.MetaData
{
    [TestFixture, Order(200)]
    public class CarrierElementSetTests
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "CarrierElementSet.xml";

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

        private void Test(IBdoMetaElementSet elemSet)
        {
            var metaCarrier1 = elemSet.Get<IBdoMetaCarrier>("carrier1");
            var metaCarrier2 = elemSet.Get<IBdoMetaCarrier>("carrier2");
            var metaCarrier3 = elemSet.Get<IBdoMetaCarrier>(2);
            var metaCarrier4 = elemSet.GetCarrier("carrier4");

            Assert.That(elemSet?.Count == 4, "Bad carrier element set - Count");
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
                    .WithItems((new { path = _testData.path4 }).AsElementSet()?.ToArray()))?.AsMeta();

            _metaCarrierSet = BdoMeta.NewSet(
                metaCarrier1, metaCarrier2, metaCarrier3, metaCarrier4);

            Test(_metaCarrierSet);
        }

        [Test, Order(2)]
        public void SaveBdoElementSetTest()
        {
            if (_metaCarrierSet == null)
            {
                CreateCarrierElementSetTest();
            }

            var isSaved = _metaCarrierSet.ToDto().SaveXml(_filePath);
            Assert.That(isSaved, "Element set saving failed");

            Test(_metaCarrierSet);
        }

        [Test, Order(3)]
        public void LoadBdoElementSetTest()
        {
            if (_metaCarrierSet == null || !File.Exists(_filePath))
            {
                SaveBdoElementSetTest();
            }

            var elemSet = XmlHelper.LoadXml<BdoElementSetDto>(_filePath).ToPoco();

            Test(elemSet);
        }
    }
}
