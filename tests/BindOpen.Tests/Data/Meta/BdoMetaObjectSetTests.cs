using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Extensions;
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

        public void Test(IBdoMetaSet metaSet)
        {
            var metaObj1 = metaSet.Get<IBdoMetaObject>("object1");
            var metaObj2 = metaSet["object2"] as IBdoMetaObject;
            var metaObj3 = metaSet.Get<IBdoMetaObject>(2);
            var metaObj4 = metaSet.Get<IBdoMetaObject>("object4");

            Assert.That(metaSet?.Count == 4, "Bad object element set - Count");

            var path1 = metaObj1?.GetSubItem<string>("path");
            Assert.That(
                path1 == _testData.path1
                , "Bad object element - Set1");

            Assert.That(
                metaObj2?.GetSubItem<string>("path") == _testData.path2
                , "Bad object element - Set2");

            Assert.That(
                metaObj3?.GetSubItem<string>("path") == _testData.path3
                , "Bad object element - Set3");

            Assert.That(
                metaObj4?.GetSubItem<string>("path") == _testData.path4
                , "Bad object element - Set4");
        }

        [Test, Order(1)]
        public void NewTest()
        {
            var metaObj1 = BdoMeta.NewObject("object1")
                .WithItems(
                    BdoConfig.NewExtension(
                        "tests.core$testEntity",
                        BdoMeta.NewScalar("path", _testData.path1)));

            var metaObj2 = BdoMeta.NewObject("object2", "tests.core$testEntity")
                .WithData(new EntityFake()
                {
                    Path = _testData.path2
                });
            metaObj2.UpdateMetaTree();

            var obj3 = new EntityFake(
                _testData.path3,
                _testData.folderPath3,
                new EntityFake(_testData.path1, _testData.folderPath3));
            var metaObj3 = obj3?.ToMetaItem();

            var obj4 = Bdo.NewEntity<EntityFake>(
                BdoConfig.New("tests.core$testEntity")
                    .FromObject(new { path = _testData.path4 }));
            var metaObj4 = obj4?.ToMetaItem("object4");

            _metaObjSet = BdoMeta.NewSet(
                metaObj1,
                metaObj2,
                metaObj3,
                metaObj4);

            Test(_metaObjSet);
        }
    }
}
