using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace BindOpen.Tests.IO.Meta
{
    [TestFixture, Order(202)]
    public class MetaScalarSetIOTests
    {
        private readonly string _filePath_xml = GlobalVariables.WorkingFolder + "ScalarElementSet.xml";
        private readonly string _filePath_json = GlobalVariables.WorkingFolder + "ScalarElementSet.json";

        private dynamic _testData;

        private IBdoMetaList _metaSet = null;

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
            IBdoMetaList set1,
            IBdoMetaList set2)
        {
            var b = set1 != null && set2 != null
                && set1.IsDeepEqual(set2);
            return b;
        }

        [Test, Order(2)]
        public void CreateTest()
        {
            var el1 = BdoMeta.NewScalar("float1", DataValueTypes.Number, _testData.arrayNumber1 as double[]);
            var el2 = BdoMeta.NewScalar("text2", DataValueTypes.Text, _testData.arrayString2 as string[]);
            var el3 = BdoMeta.NewScalar("integer3", DataValueTypes.Integer, _testData.arrayInteger3 as int[]);
            var el4 = BdoMeta.NewScalar("byteArray4", DataValueTypes.ByteArray, _testData.arrayArrayByte4 as byte[][]);

            _metaSet = BdoMeta.NewList(el1, el2, el3, el4);
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

            var metaSet = XmlHelper.LoadXml<MetaListDto>(_filePath_xml).ToPoco();
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

            var metaSet = JsonHelper.LoadJson<MetaListDto>(_filePath_json).ToPoco();
            Equals(metaSet, _metaSet);
        }
    }
}
