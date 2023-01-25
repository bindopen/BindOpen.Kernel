using BindOpen.Data;
using BindOpen.Data.Meta;
using NUnit.Framework;
using System;
using System.Linq;

namespace BindOpen.Tests.Data
{
    [TestFixture, Order(202)]
    public class BdoMetaScalarSetTests
    {
        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = BdoMetaScalarFaker.Fake();
        }

        [Test, Order(1)]
        public void NewTest_WithNull()
        {
            var el1 = BdoData.NewMetaScalar("null1", null);

            Assert.That(
                el1 != null, "Bad scalar el creation");
        }

        [Test, Order(2)]
        public void NewTest()
        {
            double[] arrayNumber = _testData.arrayNumber;
            string[] arrayString = _testData.arrayString;
            int[] arrayInteger = _testData.arrayInteger;
            byte[][] arrayArrayByte = _testData.arrayArrayByte;

            var el1 = BdoData.NewMetaScalar("number1", arrayNumber);
            var el2 = BdoData.NewMetaScalar("text2", arrayString);
            var el3 = BdoData.NewMetaScalar("integer3", arrayInteger);
            var el4 = BdoData.NewMetaScalar("byteArray4", arrayArrayByte);

            var elSet = BdoData.NewMetaSet(el1, el2, el3, el4);

            var itemList1 = elSet.GetItems<double>("number1");
            Assert.That(
                itemList1?.Intersect(arrayNumber).Any() ?? false, "Bad scalar el - Number");

            var itemList2 = elSet.GetItems<string>("text2");
            Assert.That(
                itemList2?.Intersect(arrayString).Any() ?? false, "Bad scalar el - String");

            var itemList3 = elSet.GetItems<int>("integer3");
            Assert.That(
                itemList3?.Intersect(arrayInteger).Any() ?? false, "Bad scalar el - Integer");

            var item4 = elSet.GetItems<byte[]>("byteArray4");
            Assert.That(
                item4[0]?.SequenceEqual(arrayArrayByte[0]) == true
                , "Bad scalar el - Byte array");
        }

        [Test, Order(3)]
        public void UpdateCheckRepairTest()
        {
            var elAA = BdoData.NewMetaScalar("name1", null);
            var elAB = BdoData.NewMetaScalar("name1", "Test1");
            elAA.Repair(elAB);

            var elSetA = BdoData.NewMetaSet(elAA, elAB);

            var elBA = BdoData.NewMetaScalar("name1", "Test1");
            var elBB = BdoData.NewMetaScalar("name1", null);
            elBA.Repair(elBB);

            var elSetB = BdoData.NewMetaSet(elBA, elBB);

            elSetB.Add(elBB);
            elSetA.Add(elAB);
            elSetB.Update(elSetA);

            elSetA.Add(null);
            elSetB.Add(null);
            elSetB.Add(BdoData.NewMeta("name1", null));
            elSetB.Add(BdoData.NewMeta("name3", null));
            elSetB.Add(BdoData.NewMeta("name4", null));
            elSetB.Add(BdoData.NewMeta("name5", DataValueTypes.Text));
            elSetA.Add(BdoData.NewMeta("name1", null));
            elSetA.Add(BdoData.NewMeta("name2", null));
            elSetA.Add(BdoData.NewMetaScalar("name4", DataValueTypes.Text, null));
            elSetA.Add(BdoData.NewMeta("name5", null));
            elSetB.Repair(elSetA);
            elSetB.Update(elSetA);
        }

        [Test, Order(4)]
        public void ToStringTest()
        {
            string[] arrayString = _testData.arrayString;

            var el = BdoData.NewMetaScalar(DataValueTypes.Text, arrayString[0]);
            var st = el.ToString();
            Assert.That(st == arrayString[0], "Bad scalar el - ToString");

            int[] arrayInteger = _testData.arrayInteger;

            el = BdoData.NewMetaScalar(DataValueTypes.Text, _testData.arrayInteger[0]);
            st = el.ToString();
            Assert.That(st == arrayInteger[0].ToString(), "Bad scalar el - ToString");
        }
    }
}
