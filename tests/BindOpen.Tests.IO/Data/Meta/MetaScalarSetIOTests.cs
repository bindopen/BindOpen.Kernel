using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Script;
using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BindOpen.Tests.IO.Data
{
    [TestFixture, Order(202)]
    public class MetaScalarListIOTests
    {
        private readonly string _filePath_xml = Tests.WorkingFolder + "MetaScalarList.xml";
        private readonly string _filePath_json = Tests.WorkingFolder + "MetaScalarList.json";

        private dynamic _testData;

        private IBdoMetaSet _metaSet = null;

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
            IBdoMetaSet list1,
            IBdoMetaSet list2)
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
                BdoMeta.NewScalar("float1", DataValueTypes.Number, _testData.arrayNumber1 as double[]),
                BdoMeta.NewScalar("text2", DataValueTypes.Text, _testData.arrayString2 as string[]),
                BdoMeta.NewScalar("integer3", DataValueTypes.Integer, _testData.arrayInteger3 as int[]),
                BdoMeta.NewScalar("byteArray4", DataValueTypes.ByteArray, _testData.arrayArrayByte4 as byte[][]),
                BdoMeta.NewScalar("float2", DataValueTypes.Number, (_testData.arrayNumber1 as double[])[0]),
                BdoMeta.NewScalar("float2", DataValueTypes.Number, (_testData.arrayNumber1 as double[])[1])
                    .WithDataReference(BdoScript.Var("klkl"))
                    //.WithSpecs(BdoMeta.NewSpec(), BdoMeta.NewSpec("spec1"))
            };

            _metaSet = BdoMeta.NewSet(metas.ToArray());
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
            Assert.That(isSaved, "Meta list saving failed. ");
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
