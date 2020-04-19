using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.System.Diagnostics;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.Core.Data.Elements
{
    [TestFixture]
    public class ScalarElementSetTest
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "ScalarElementSet.xml";

        private DataElementSet _scalarElementSetA = null;

        [SetUp]
        public void OneTimeSetUp()
        {
            _scalarElementSetA = new[]
            {
                ElementFactory.CreateScalar("float1", DataValueType.Number, 10),
                ElementFactory.CreateScalar("text1", DataValueType.Text, "item1", "item2", "item3"),
                ElementFactory.CreateScalar("integer1", DataValueType.Integer, 1, 2, 3),
                ElementFactory.CreateScalar("float2", DataValueType.Number, 1.1, 1.2, 1.3)
            };
        }

        [Test]
        public void TestCreateElementWithNullValue()
        {
            var element1 = ElementFactory.CreateScalar("null1", null);

            Assert.That(
                element1 != null, "Bad scalar element creation");
        }

        [Test]
        public void CreateElementSetTest()
        {
            Assert.That(
                ((string)_scalarElementSetA["text1"]?[0] == "item1")
                && ((string)_scalarElementSetA["text1"]?[1] == "item2")
                && ((string)_scalarElementSetA["text1"]?[2] == "item3"), "Bad text scalar element creation");
            Assert.That(
                ((int)_scalarElementSetA["integer1"]?[0] == 1)
                && ((int)_scalarElementSetA["integer1"]?[1] == 2)
                && ((int)_scalarElementSetA["integer1"]?[2] == 3), "Bad integer scalar element creation");
            Assert.That(
                ((double)_scalarElementSetA["float2"]?[0] == 1.1)
                && ((double)_scalarElementSetA["float2"]?[1] == 1.2)
                && ((double)_scalarElementSetA["float2"]?[2] == 1.3), "Bad float scalar element creation");

            Assert.That(
                _scalarElementSetA.Count == 4, "Bad scalar element set creation");
        }

        [Test]
        public void UpdateCheckRepairTest()
        {
            var elementAA = ElementFactory.CreateScalar("name1", null);
            var elementAB = ElementFactory.CreateScalar("name1", "Test1");
            elementAA.Repair(elementAB);

            var elementSetA = ElementFactory.CreateSet(elementAA, elementAB);


            var elementBA = ElementFactory.CreateScalar("name1", "Test1");
            var elementBB = ElementFactory.CreateScalar("name1", null);
            elementBA.Repair(elementBB);

            var elementSetB = ElementFactory.CreateSet(elementBA, elementBB);

            elementSetB.Add(elementBB);
            elementSetA.Add(elementAB);
            elementSetB.Update(elementSetA);

            elementSetA.Add(null);
            elementSetB.Add(null);
            elementSetB.Add(ElementFactory.Create("name1", null));
            elementSetB.Add(ElementFactory.Create("name3", null));
            elementSetB.Add(ElementFactory.Create("name4", null));
            elementSetB.Add(ElementFactory.Create("name5", DataValueType.Text));
            elementSetA.Add(ElementFactory.Create("name1", null));
            elementSetA.Add(ElementFactory.Create("name2", null));
            elementSetA.Add(ElementFactory.CreateScalar("name4", DataValueType.Text, null));
            elementSetA.Add(ElementFactory.Create("name5", null));
            elementSetB.Repair(elementSetA);
            elementSetB.Update(elementSetA);
        }

        [Test]
        public void SaveDataElementSetTest()
        {
            var log = new BdoLog();

            _scalarElementSetA.SaveXml(_filePath, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Element set saving failed. Result was '" + xml);
        }

        [Test]
        public void LoadDataElementSetTest()
        {
            var log = new BdoLog();

            if (_scalarElementSetA == null || !File.Exists(_filePath))
                SaveDataElementSetTest();

            var elementSet = XmlHelper.Load<DataElementSet>(_filePath, log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Element set loading failed. Result was '" + xml);

            CreateElementSetTest();
        }
    }
}
