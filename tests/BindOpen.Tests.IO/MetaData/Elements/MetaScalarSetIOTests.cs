using BindOpen.MetaData;
using BindOpen.MetaData.Elements;
using BindOpen.MetaData.Items;
using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace BindOpen.Tests.IO.MetaData
{
    [TestFixture, Order(202)]
    public class MetaScalarSetIOTests
    {
        private readonly string _filePath_xml = GlobalVariables.WorkingFolder + "ScalarElementSet.xml";
        private readonly string _filePath_json = GlobalVariables.WorkingFolder + "ScalarElementSet.json";

        private dynamic _testData;

        private IBdoMetaElementSet _elemSet = null;

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
            IBdoMetaElementSet set1,
            IBdoMetaElementSet set2)
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

            _elemSet = BdoMeta.NewSet(el1, el2, el3, el4);
        }

        // Xml

        [Test, Order(5)]
        public void SaveXmlTest()
        {
            if (_elemSet == null)
            {
                CreateTest();
            }

            var isSaved = _elemSet.ToDto().SaveXml(_filePath_xml);
            Assert.That(isSaved, "Element set saving failed. ");
        }

        [Test, Order(6)]
        public void LoadXmlTest()
        {
            if (_elemSet == null || !File.Exists(_filePath_xml))
            {
                SaveXmlTest();
            }

            var elemSet = XmlHelper.LoadXml<BdoElementSetDto>(_filePath_xml).ToPoco();
            Equals(elemSet, _elemSet);
        }

        // Json

        [Test, Order(7)]
        public void SaveJsonTest()
        {
            if (_elemSet == null)
            {
                CreateTest();
            }

            var isSaved = _elemSet.ToDto().SaveJson(_filePath_json);
            Assert.That(isSaved, "Element set saving failed. ");
        }

        [Test, Order(8)]
        public void LoadJsonTest()
        {
            if (_elemSet == null || !File.Exists(_filePath_json))
            {
                SaveJsonTest();
            }

            var elemSet = JsonHelper.LoadJson<BdoElementSetDto>(_filePath_json).ToPoco();
            Equals(elemSet, _elemSet);
        }
    }
}
