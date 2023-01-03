using BindOpen.Data;
using BindOpen.Data.Elements;
using NUnit.Framework;
using System;
using System.Linq;

namespace BindOpen.Runtime.Tests.MetaData.Elements.Scalar
{
    [TestFixture, Order(202)]
    public class ScalarElementSetTests
    {
        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = ScalarElementFaker.Fake();
        }

        [Test, Order(1)]
        public void TestCreateElementWithNullValue()
        {
            var el1 = BdoElements.NewScalar("null1", null);

            Assert.That(
                el1 != null, "Bad scalar el creation");
        }

        [Test, Order(2)]
        public void CreateElementSetTest()
        {
            double[] arrayNumber = _testData.arrayNumber;
            string[] arrayString = _testData.arrayString;
            int[] arrayInteger = _testData.arrayInteger;
            byte[][] arrayArrayByte = _testData.arrayArrayByte;

            var el1 = BdoElements.NewScalar("number1", arrayNumber);
            var el2 = BdoElements.NewScalar("text2", arrayString);
            var el3 = BdoElements.NewScalar("integer3", arrayInteger);
            var el4 = BdoElements.NewScalar("byteArray4", arrayArrayByte);

            var elSet = BdoElements.NewSet(el1, el2, el3, el4);

            var itemList1 = elSet.GetItemList<double>("number1");
            Assert.That(
                itemList1?.Intersect(arrayNumber).Any() ?? false, "Bad scalar el - Number");

            var itemList2 = elSet.GetItemList<string>("text2");
            Assert.That(
                itemList2?.Intersect(arrayString).Any() ?? false, "Bad scalar el - String");

            var itemList3 = elSet.GetItemList<int>("integer3");
            Assert.That(
                itemList3?.Intersect(arrayInteger).Any() ?? false, "Bad scalar el - Integer");

            var item4 = elSet.GetItemList<byte[]>("byteArray4");
            Assert.That(
                item4[0]?.SequenceEqual(arrayArrayByte[0]) == true
                , "Bad scalar el - Byte array");
        }

        [Test, Order(3)]
        public void UpdateCheckRepairTest()
        {
            var elAA = BdoElements.NewScalar("name1", null);
            var elAB = BdoElements.NewScalar("name1", "Test1");
            elAA.Repair(elAB);

            var elSetA = BdoElements.NewSet(elAA, elAB);

            var elBA = BdoElements.NewScalar("name1", "Test1");
            var elBB = BdoElements.NewScalar("name1", null);
            elBA.Repair(elBB);

            var elSetB = BdoElements.NewSet(elBA, elBB);

            elSetB.Add(elBB);
            elSetA.Add(elAB);
            elSetB.Update(elSetA);

            elSetA.Add(null);
            elSetB.Add(null);
            elSetB.Add(BdoElements.NewElement("name1", null));
            elSetB.Add(BdoElements.NewElement("name3", null));
            elSetB.Add(BdoElements.NewElement("name4", null));
            elSetB.Add(BdoElements.NewElement("name5", DataValueTypes.Text));
            elSetA.Add(BdoElements.NewElement("name1", null));
            elSetA.Add(BdoElements.NewElement("name2", null));
            elSetA.Add(BdoElements.NewScalar("name4", DataValueTypes.Text, null));
            elSetA.Add(BdoElements.NewElement("name5", null));
            elSetB.Repair(elSetA);
            elSetB.Update(elSetA);
        }

        [Test, Order(4)]
        public void ElementToStringTest()
        {
            string[] arrayString = _testData.arrayString;

            var el = BdoElements.NewScalar(DataValueTypes.Text, arrayString[0]);
            var st = el.ToString();
            Assert.That(st == arrayString[0], "Bad scalar el - ToString");

            int[] arrayInteger = _testData.arrayInteger;

            el = BdoElements.NewScalar(DataValueTypes.Text, _testData.arrayInteger[0]);
            st = el.ToString();
            Assert.That(st == arrayInteger[0].ToString(), "Bad scalar el - ToString");
        }
    }
}
