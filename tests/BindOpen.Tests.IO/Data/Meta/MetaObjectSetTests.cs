using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Modeling;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.IO.Data
{
    [TestFixture, Order(201)]
    public class MetaObjectSetTests
    {
        private readonly string _filePath_xml = GlobalVariables.WorkingFolder + "EntityElementSet.xml";
        private readonly string _filePath_json = GlobalVariables.WorkingFolder + "EntityElementSet.json";

        private BdoMetaSet _metaSet;

        private object _obj1 = null;
        private object _obj2 = null;
        private object _obj3 = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _obj1 = ClassObjectFaker.Fake();
            _obj2 = ClassObjectFaker.Fake();
            _obj3 = ClassObjectFaker.Fake();
        }

        private void Test(IBdoMetaSet metaSet)
        {
            var obj1 = metaSet.GetItem("object1");
            var obj2 = metaSet.GetItem("object2");
            var obj3 = metaSet["object3"].Item();

            Assert.That(obj1.IsDeepEqual(_obj1) == true, "Bad obj element set - Count");
            Assert.That(obj2.IsDeepEqual(_obj2) == true, "Bad obj element set - Count");
            Assert.That(obj3.IsDeepEqual(_obj3) == true, "Bad obj element set - Count");
            Assert.That(metaSet?.Count == 3, "Bad object element set - Count");
        }

        [Test, Order(1)]
        public void CreateTest()
        {
            //        var metaEntity1 = BdoData.NewMetaEntity(
            //"entity1",
            //        BdoExt.NewEntityConfig(
            //    "tests.core$testEntity",
            //    BdoData.NewMetaScalar("path", _testData.path1)));

            //        var metaEntity2 = BdoData.NewMetaEntity("entity2", "tests.core$testEntity")
            //            .WithItems((new { path = _testData.path2 }).ToMetaSet<BdoEntityConfiguration>());
            //        var metaEntity3 = new EntityFake(_testData.path3, _testData.folderPath3)?.ToMeta();

            //        var metaEntity4 = BdoExt.NewEntity<EntityFake>(
            //        BdoExt.NewEntityConfig("tests.core$testEntity")
            //                .WithItems((new { path = _testData.path4 }).ToMetaArray()))?.ToMeta();

            var meta1 = BdoData.NewMetaObject("object1", _obj1);
            var meta2 = BdoData.NewMetaObject("object2", _obj2);
            var meta3 = BdoData.NewMetaObject("object3", _obj3);

            _metaSet = BdoData.NewMetaSet(meta1, meta2, meta3);

            Test(_metaSet);
        }

        // Xml

        [Test, Order(5)]
        public void SaveXmlTest()
        {
            if (_metaSet == null)
            {
                CreateTest();
            }

            var isSaved = _metaSet.ToDto().SaveXml(_filePath_xml);
            Assert.That(isSaved, "Element set saving failed. ");
        }

        [Test, Order(6)]
        public void LoadXmlTest()
        {
            if (_metaSet == null || !File.Exists(_filePath_xml))
            {
                SaveXmlTest();
            }

            var metaSet = XmlHelper.LoadXml<MetaSetDto>(_filePath_xml).ToPoco();
            Equals(metaSet, _metaSet);
        }

        // Json

        [Test, Order(7)]
        public void SaveJsonTest()
        {
            if (_metaSet == null)
            {
                CreateTest();
            }

            var isSaved = _metaSet.ToDto().SaveJson(_filePath_json);
            Assert.That(isSaved, "Element set saving failed. ");
        }

        [Test, Order(8)]
        public void LoadJsonTest()
        {
            if (_metaSet == null || !File.Exists(_filePath_json))
            {
                SaveJsonTest();
            }

            var metaSet = JsonHelper.LoadJson<MetaSetDto>(_filePath_json).ToPoco();
            Equals(metaSet, _metaSet);
        }
    }
}
