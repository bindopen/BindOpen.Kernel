using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Extensions;
using BindOpen.Extensions.Modeling;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Tests.Data
{
    [TestFixture, Order(200)]
    public class BdoMetaObjectSetTests
    {
        private dynamic _testData;

        private IBdoMetaSet _metaObjSet = null;

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

        public void Test(IBdoMetaSet elemSet)
        {
            var metaObj1 = elemSet.Get<IBdoMetaObject>("object1");
            var metaObj2 = elemSet["object2"] as IBdoMetaObject;
            var metaObj3 = elemSet.Get<IBdoMetaObject>(2);
            var metaObj4 = elemSet.Get<IBdoMetaObject>("object4");

            Assert.That(elemSet?.Count == 4, "Bad object element set - Count");

            var path1 = metaObj1?.SubSet.GetItem<string>("path");
            Assert.That(
                path1 == _testData.path1
                , "Bad object element - Set1");

            Assert.That(
                metaObj2?.SubSet.GetItem<string>("path") == _testData.path2
                , "Bad object element - Set2");

            Assert.That(
                metaObj3?.SubSet.GetItem<string?>("path") == _testData.path3
                , "Bad object element - Set3");

            Assert.That(
                metaObj4?.SubSet.GetItem<string>("path") == _testData.path4
                , "Bad object element - Set4");
        }

        [Test, Order(1)]
        public void NewTest()
        {
            var metaObj1 = BdoMeta.NewObject("object1")
                .WithSubSet(
                    BdoConfig.NewEntity(
                        "tests.core$testEntity",
                        BdoMeta.NewScalar("path", _testData.path1)));

            var metaObj2 = BdoMeta.NewObject("object2", "tests.core$testEntity")
                .WithItems((new { path = _testData.path2 })
                .ToMetaSet<BdoEntityConfiguration>());

            var metaObj3 = new EntityFake(_testData.path3, _testData.folderPath3)
                ?.ToMeta();

            var metaObj4 = BdoExtension.NewEntity<EntityFake>(
                BdoConfig.NewEntity("tests.core$testEntity")
                    .FromObject(new { path = _testData.path4 }))
                ?.ToMeta();

            _metaObjSet = BdoMeta.NewSet(
                metaObj1,
                metaObj2,
                metaObj3,
                metaObj4);

            Test(_metaObjSet);
        }
    }
}
