﻿using BindOpen.Data;
using NUnit.Framework;
using System;
using System.Linq;

namespace BindOpen.Tests.Data
{
    [TestFixture, Order(202)]
    public class BdoMetaScalarListTests
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
            var el1 = BdoMeta.NewScalar("null1", null);

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

            var el1 = BdoMeta.NewScalar("number1", arrayNumber);
            var el2 = BdoMeta.NewScalar("text2", arrayString);
            var el3 = BdoMeta.NewScalar("integer3", arrayInteger);
            var el4 = BdoMeta.NewScalar("byteArray4", arrayArrayByte);

            var elSet = BdoMeta.NewSet(el1, el2, el3, el4);

            var itemList1 = elSet.GetDataList<double>("number1");
            Assert.That(
                itemList1?.Intersect(arrayNumber).Any() ?? false, "Bad scalar el - Number");

            var itemList2 = elSet.GetDataList<string>("text2");
            Assert.That(
                itemList2?.Intersect(arrayString).Any() ?? false, "Bad scalar el - String");

            var itemList3 = elSet.GetDataList<int>("integer3");
            Assert.That(
                itemList3?.Intersect(arrayInteger).Any() ?? false, "Bad scalar el - Integer");

            var item4 = elSet.GetDataList<byte[]>("byteArray4");
            Assert.That(
                item4[0]?.SequenceEqual(arrayArrayByte[0]) == true
                , "Bad scalar el - Byte array");
        }

        [Test, Order(3)]
        public void UpdateCheckRepairTest()
        {
            var elAA = BdoMeta.NewScalar("name1", null);
            var elAB = BdoMeta.NewScalar("name1", "Test1");
            elAA.Update(elAB);

            var elSetA = BdoMeta.NewSet(elAA, elAB);

            var elBA = BdoMeta.NewScalar("name1", "Test1");
            var elBB = BdoMeta.NewScalar("name1", null);
            elBA.Update(elBB);

            var elSetB = BdoMeta.NewSet(elBA, elBB);

            elSetB.Add(elBB);
            elSetA.Add(elAB);
            elSetB.Update(elSetA);

            elSetA.Add(null);
            elSetB
                .Add(null)
                .Add(("name1", typeof(string)))
                .Add(BdoMeta.New("name3", typeof(int)))
                .Add(("name4", typeof(double)))
                .Add(BdoMeta.New("name5", DataValueTypes.Text));
            elSetA
                .Add(BdoMeta.New("name1", typeof(string)))
                .Add(BdoMeta.New("name2", null as EntityFake))
                .Add(BdoMeta.NewScalar("name4", DataValueTypes.Text, null))
                .Add(BdoMeta.New("name5", null as string));
            elSetB.Update(elSetA);
        }

        [Test, Order(4)]
        public void ToStringTest()
        {
            string[] arrayString = _testData.arrayString;

            var el = BdoMeta.NewScalar(DataValueTypes.Text, arrayString[0]);
            var st = el.ToString();
            Assert.That(st == arrayString[0], "Bad scalar el - ToString");

            int[] arrayInteger = _testData.arrayInteger;

            el = BdoMeta.NewScalar(DataValueTypes.Text, _testData.arrayInteger[0]);
            st = el.ToString();
            Assert.That(st == arrayInteger[0].ToString(), "Bad scalar el - ToString");
        }
    }
}