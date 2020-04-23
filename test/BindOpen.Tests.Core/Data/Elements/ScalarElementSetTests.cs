using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.System.Diagnostics;
using Bogus;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace BindOpen.Tests.Core.Data.Elements
{
    [TestFixture, Order(202)]
    public class ScalarElementSetTests
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "ScalarElementSet.xml";

        private dynamic _testData;

        private IDataElementSet _scalarElementSet = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();
            _testData = new
            {
                arrayNumber1 = Enumerable.Range(0, 10).Select(p => f.Random.Double()).ToArray(),
                arrayString2 = Enumerable.Range(0, 10).Select(p => f.Random.Word()).ToArray(),
                arrayInteger3 = Enumerable.Range(0, 10).Select(p => f.Random.Int()).ToArray(),
                arrayNumber4 = Enumerable.Range(0, 10).Select(p => f.Random.Double()).ToArray()
            };
        }

        private void Test(IDataElementSet elementSet)
        {
            var scalarElement1 = elementSet.Get<IScalarElement>("float1");
            var scalarElement2 = elementSet.Get<IScalarElement>("text1");
            var scalarElement3 = elementSet.Get<IScalarElement>(2);
            var scalarElement4 = elementSet.Get<IScalarElement>("float2");

            Assert.That(
                _scalarElementSet.Count == 4, "Bad scalar element set - Count");

            Assert.That(
                scalarElement1.Items.Cast<double>().Intersect(_testData.arrayNumber1 as double[]).Count() > 0
                , "Bad scalar element - Set1");

            Assert.That(
                scalarElement2.Items.Cast<string>().Intersect(_testData.arrayString2 as string[]).Count() > 0
                , "Bad scalar element - Set2");

            Assert.That(
                scalarElement3.Items.Cast<int>().Intersect(_testData.arrayInteger3 as int[]).Count() > 0
                , "Bad scalar element - Set3");

            Assert.That(
                scalarElement4.Items.Cast<double>().Intersect(_testData.arrayNumber4 as double[]).Count() > 0
                , "Bad scalar element - Set4");
        }

        [Test, Order(1)]
        public void TestCreateElementWithNullValue()
        {
            var element1 = ElementFactory.CreateScalar("null1", null);

            Assert.That(
                element1 != null, "Bad scalar element creation");
        }

        [Test, Order(2)]
        public void CreateElementSetTest()
        {
            _scalarElementSet = ElementFactory.CreateSet(
                ElementFactory.CreateScalar("float1", DataValueType.Number, _testData.arrayNumber1),
                ElementFactory.CreateScalar("text1", DataValueType.Text, _testData.arrayString2),
                ElementFactory.CreateScalar("integer1", DataValueType.Integer, _testData.arrayInteger3),
                ElementFactory.CreateScalar("float2", DataValueType.Number, _testData.arrayNumber4)
            );

            Test(_scalarElementSet);
        }

        [Test, Order(3)]
        public void SaveDataElementSetTest()
        {
            if (_scalarElementSet == null)
            {
                CreateElementSetTest();
            }

            var log = new BdoLog();
            _scalarElementSet.SaveXml(_filePath, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Element set saving failed. Result was '" + xml);
        }

        [Test, Order(4)]
        public void LoadDataElementSetTest()
        {
            if (_scalarElementSet == null || !File.Exists(_filePath))
            {
                SaveDataElementSetTest();
            }

            var log = new BdoLog();
            var elementSet = XmlHelper.Load<DataElementSet>(_filePath, log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Element set loading failed. Result was '" + xml);

            Test(elementSet);
        }

        [Test, Order(5)]
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

            elementSetA.Add((IDataElement[])null);
            elementSetB.Add((IDataElement[])null);
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
    }
}
