using BindOpen.Data;
using BindOpen.Data.Elements;
using BindOpen.Runtime.Tests;
using Bogus;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace BindOpen.Runtime.IO.Tests.MasterData.Elements
{
    [TestFixture, Order(202)]
    public class ScalarElementSetTests
    {
        private readonly string _filePath_xml = GlobalVariables.WorkingFolder + "ScalarElementSet.xml";
        private readonly string _filePath_json = GlobalVariables.WorkingFolder + "ScalarElementSet.json";

        private dynamic _testData;

        private IBdoElementSet _elementSet = null;

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

        private void Test(IBdoElementSet elementSet)
        {
            var scalarElement1 = elementSet.Get<IScalarElement>("float1");
            var scalarElement2 = elementSet.Get<IScalarElement>("text2");
            var scalarElement3 = elementSet.Get<IScalarElement>(2);
            var scalarElement4 = elementSet.Get<IScalarElement>("byteArray4");

            Assert.That(
                _elementSet.Count == 4, "Bad scalar element set - Count");

            var scalar1 = scalarElement1.GetItemList<double>();
            var scalar2 = scalarElement2.GetItemList<string>();
            var scalar3 = scalarElement3.GetItemList<int>();
            var scalar4 = scalarElement4.GetItemList<byte[]>();

            Assert.That(
                scalar1?.Intersect(_testData.arrayNumber1 as double[]).Any() ?? false
                , "Bad scalar element - Set1");

            Assert.That(
                scalar2?.Intersect(_testData.arrayString2 as string[]).Any() ?? false
                , "Bad scalar element - Set2");

            Assert.That(
                scalar3?.Intersect(_testData.arrayInteger3 as int[]).Any() ?? false
                , "Bad scalar element - Set3");

            Assert.That(
                scalar4[0]?.SequenceEqual(_testData.arrayArrayByte4[0] as byte[]) == true
                && scalar4[1]?.SequenceEqual(_testData.arrayArrayByte4[1] as byte[]) == true
                , "Bad scalar element - Set4");
        }

        [Test, Order(1)]
        public void TestCreateElementWithNullValue()
        {
            var element1 = BdoElements.NewScalar("null1", null);

            Assert.That(
                element1 != null, "Bad scalar element creation");
        }

        [Test, Order(2)]
        public void CreateElementSetTest()
        {
            var element1 = BdoElements.NewScalar("float1", DataValueTypes.Number, _testData.arrayNumber1);
            var element2 = BdoElements.NewScalar("text2", DataValueTypes.Text, _testData.arrayString2);
            var element3 = BdoElements.NewScalar("integer3", DataValueTypes.Integer, _testData.arrayInteger3);
            var element4 = BdoElements.NewScalar("byteArray4", DataValueTypes.ByteArray, _testData.arrayArrayByte4);

            _elementSet = BdoElements.NewSet(element1, element2, element3, element4);

            Test(_elementSet);
        }

        [Test, Order(3)]
        public void UpdateCheckRepairTest()
        {
            var elementAA = BdoElements.NewScalar("name1", null);
            var elementAB = BdoElements.NewScalar("name1", "Test1");
            elementAA.Repair(elementAB);

            var elementSetA = BdoElements.NewSet(elementAA, elementAB);


            var elementBA = BdoElements.NewScalar("name1", "Test1");
            var elementBB = BdoElements.NewScalar("name1", null);
            elementBA.Repair(elementBB);

            var elementSetB = BdoElements.NewSet(elementBA, elementBB);

            elementSetB.Add(elementBB);
            elementSetA.Add(elementAB);
            elementSetB.Update(elementSetA);

            elementSetA.Add(null);
            elementSetB.Add(null);
            elementSetB.Add(BdoElements.NewElement("name1", null));
            elementSetB.Add(BdoElements.NewElement("name3", null));
            elementSetB.Add(BdoElements.NewElement("name4", null));
            elementSetB.Add(BdoElements.NewElement("name5", DataValueTypes.Text));
            elementSetA.Add(BdoElements.NewElement("name1", null));
            elementSetA.Add(BdoElements.NewElement("name2", null));
            elementSetA.Add(BdoElements.NewScalar("name4", DataValueTypes.Text, null));
            elementSetA.Add(BdoElements.NewElement("name5", null));
            elementSetB.Repair(elementSetA);
            elementSetB.Update(elementSetA);
        }

        [Test, Order(4)]
        public void ElementToStringTest()
        {
            var el = BdoElements.NewScalar(DataValueTypes.Text, _testData.arrayString2[0]);
            var st = el.ToString();
            Assert.That(st == _testData.arrayString2[0], "Bad scalar element - ToString");

            el = BdoElements.NewScalar(DataValueTypes.Text, _testData.arrayInteger3[0]);
            st = el.ToString();
            Assert.That(st == _testData.arrayInteger3[0].ToString(), "Bad scalar element - ToString");
        }

        // Xml

        [Test, Order(5)]
        public void SaveXmlBdoElementSetTest()
        {
            if (_elementSet == null)
            {
                CreateElementSetTest();
            }

            var isSaved = _elementSet.ToDto().SaveXml(_filePath_xml);

            Assert.That(isSaved, "Element set saving failed. ");
        }

        [Test, Order(6)]
        public void LoadXmlBdoElementSetTest()
        {
            if (_elementSet == null || !File.Exists(_filePath_xml))
            {
                SaveXmlBdoElementSetTest();
            }

            var elementSet = XmlHelper.LoadXml<BdoElementSetDto>(_filePath_xml).ToPoco();

            Test(elementSet);
        }

        // Json

        [Test, Order(7)]
        public void SaveJsonBdoElementSetTest()
        {
            if (_elementSet == null)
            {
                CreateElementSetTest();
            }

            var isSaved = _elementSet.ToDto().SaveJson(_filePath_json);

            Assert.That(isSaved, "Element set saving failed. ");
        }

        [Test, Order(8)]
        public void LoadJsonBdoElementSetTest()
        {
            if (_elementSet == null || !File.Exists(_filePath_json))
            {
                SaveJsonBdoElementSetTest();
            }

            var elementSet = JsonHelper.LoadJson<BdoElementSetDto>(_filePath_json).ToPoco();

            Test(elementSet);
        }
    }
}
