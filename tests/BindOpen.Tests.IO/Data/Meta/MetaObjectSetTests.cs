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

        private BdoMetaList _metaList;

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

        private void Test(IBdoMetaList metaList)
        {
            var obj1 = metaList.GetData("object1");
            var obj2 = metaList.GetData("object2");
            var obj3 = metaList["object3"].GetData();

            Assert.That(obj1.IsDeepEqual(_obj1) == true, "Bad obj element set - Count");
            Assert.That(obj2.IsDeepEqual(_obj2) == true, "Bad obj element set - Count");
            Assert.That(obj3.IsDeepEqual(_obj3) == true, "Bad obj element set - Count");
            Assert.That(metaList?.Count == 3, "Bad object element set - Count");
        }

        [Test, Order(1)]
        public void CreateTest()
        {
            //        var metaEntity1 = BdoMeta.NewEntity(
            //"entity1",
            //        BdoConfig.New(
            //    "tests.core$testEntity",
            //    BdoMeta.NewScalar("path", _testData.path1)));

            //        var metaEntity2 = BdoMeta.NewEntity("entity2", "tests.core$testEntity")
            //            .With(new { path = _testData.path2 }).ToMetaList<BdoConfiguration>());
            //        var metaEntity3 = new EntityFake(_testData.path3, _testData.folderPath3)?.ToMeta();

            //        var metaEntity4 = BdoExt.NewEntity<EntityFake>(
            //        BdoConfig.New("tests.core$testEntity")
            //                .With(new { path = _testData.path4 }).ToMetaArray()))?.ToMeta();

            var meta1 = BdoMeta.NewObject("object1", _obj1);
            var meta2 = BdoMeta.NewObject("object2", _obj2);
            var meta3 = BdoMeta.NewObject("object3", _obj3);

            _metaList = BdoMeta.NewList(meta1, meta2, meta3);

            Test(_metaList);
        }

        // Xml

        [Test, Order(5)]
        public void SaveXmlTest()
        {
            if (_metaList == null)
            {
                CreateTest();
            }

            var isSaved = _metaList.ToDto().SaveXml(_filePath_xml);
            Assert.That(isSaved, "Meta list saving failed. ");
        }

        [Test, Order(6)]
        public void LoadXmlTest()
        {
            if (_metaList == null || !File.Exists(_filePath_xml))
            {
                SaveXmlTest();
            }

            var metaList = XmlHelper.LoadXml<MetaListDto>(_filePath_xml).ToPoco();
            Equals(metaList, _metaList);
        }

        // Json

        [Test, Order(7)]
        public void SaveJsonTest()
        {
            if (_metaList == null)
            {
                CreateTest();
            }

            var isSaved = _metaList.ToDto().SaveJson(_filePath_json);
            Assert.That(isSaved, "Meta list saving failed. ");
        }

        [Test, Order(8)]
        public void LoadJsonTest()
        {
            if (_metaList == null || !File.Exists(_filePath_json))
            {
                SaveJsonTest();
            }

            var metaList = JsonHelper.LoadJson<MetaListDto>(_filePath_json).ToPoco();
            Equals(metaList, _metaList);
        }
    }
}
