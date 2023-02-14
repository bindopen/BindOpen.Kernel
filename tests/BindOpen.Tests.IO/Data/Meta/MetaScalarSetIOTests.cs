using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Scripting;
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
        private readonly string _filePath_xml = GlobalVariables.WorkingFolder + "MetaScalarList.xml";
        private readonly string _filePath_json = GlobalVariables.WorkingFolder + "MetaScalarList.json";

        private dynamic _testData;

        private IBdoMetaList _metaList = null;

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
            IBdoMetaList list1,
            IBdoMetaList list2)
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

            _metaList = BdoMeta.NewList(metas.ToArray());
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
