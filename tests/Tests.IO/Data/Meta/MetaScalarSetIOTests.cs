using BindOpen.System.Scoping.Script;
using BindOpen.System.Tests;
using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    [TestFixture, Order(202)]
    public class MetaScalarListIOTests
    {
        private readonly string _filePath_xml = SystemData.WorkingFolder + "MetaScalarList.xml";
        private readonly string _filePath_json = SystemData.WorkingFolder + "MetaScalarList.json";

        private dynamic _testData;

        private IBdoMetaComposite _metaSet = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();
            _testData = new
            {
                arrayNumber1 = Enumerable.Range(0, 10).Select(p => f.Random.Double()).ToArray(),
                arrayString2 = Enumerable.Range(0, 10).Select(p => f.Random.Word()).ToArray(),
                arrayInteger3 = Enumerable.Range(0, 10).Select(p => f.Random.Int()).ToArray(),
                arrayArrayByte4 = Enumerable.Range(1, 2).Select(p => f.Random.Bytes(5000)).ToArray()
            };
        }

        public static bool Equals(
            IBdoMetaComposite list1,
            IBdoMetaComposite list2)
        {
            var b = list1 != null && list2 != null
                && list1.IsDeepEqual(list2);
            return b;
        }

        [Test, Order(2)]
        public void CreateTest()
        {
            var metas = new List<IBdoMetaData>
            {
                BdoData.NewMetaScalar("float1", DataValueTypes.Number, _testData.arrayNumber1 as double[]),
                BdoData.NewMetaScalar("text2", DataValueTypes.Text, _testData.arrayString2 as string[]),
                BdoData.NewMetaScalar("integer3", DataValueTypes.Integer, _testData.arrayInteger3 as int[]),
                BdoData.NewMetaScalar("byteArray4", DataValueTypes.Binary, _testData.arrayArrayByte4 as byte[][]),
                BdoData.NewMetaScalar("float2", DataValueTypes.Number, (_testData.arrayNumber1 as double[])[0]),
                BdoData.NewMetaScalar("float2", DataValueTypes.Number, (_testData.arrayNumber1 as double[])[1])
                    .WithDataReference(BdoScript.Var("klkl"))
                    //.WithSpecs(BdoMeta.NewSpec(), BdoMeta.NewSpec("spec1"))
            };

            _metaSet = BdoData.NewMetaComposite(metas.ToArray());
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
            Assert.That(isSaved, "Meta list saving failed. ");
        }

        [Test, Order(6)]
        public void LoadXmlTest()
        {
            if (_metaSet == null || !File.Exists(_filePath_xml))
            {
                SaveXmlTest();
            }

            var metaSet = XmlHelper.LoadXml<MetaCompositeDto>(_filePath_xml).ToPoco();
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
            Assert.That(isSaved, "Meta list saving failed. ");
        }

        [Test, Order(8)]
        public void LoadJsonTest()
        {
            if (_metaSet == null || !File.Exists(_filePath_json))
            {
                SaveJsonTest();
            }

            var metaSet = JsonHelper.LoadJson<MetaCompositeDto>(_filePath_json).ToPoco();
            Equals(metaSet, _metaSet);
        }
    }
}
