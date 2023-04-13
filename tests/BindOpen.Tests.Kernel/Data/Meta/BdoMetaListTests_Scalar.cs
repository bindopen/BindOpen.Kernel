﻿using BindOpen.Data;
using BindOpen.Data.Meta;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Tests.Data
{
    [TestFixture, Order(202)]
    public class BdoMetaSetTests_Scalar
    {
        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = BdoMetaScalarFaker.Fake();
        }

        [Test, Order(1)]
        public void NewTest_Number()
        {
            double[] items = _testData.arrayNumber;

            var el1 = BdoMeta.NewScalar("number1", DataValueTypes.Number, items);
            var itemList = el1.GetDataList<double>();
            Assert.That(
                itemList?.Intersect(items).Any() ?? false
                && el1.GetSpec().DataType.ValueType == DataValueTypes.Number, "Bad scalar element - Number");

            var el2 = BdoMeta.NewScalar<double>("number1", items);
            itemList = el2.GetDataList<double>();
            Assert.That(
                itemList?.Intersect(items).Any() ?? false
                && el2.GetSpec().DataType.ValueType == DataValueTypes.Number, "Bad scalar element - Number");
        }

        [Test, Order(2)]
        public void NewTest_String()
        {
            string[] items = _testData.arrayString;
            var el = BdoMeta.NewScalar("text2", items);

            var itemList = el.GetDataList<string>();

            Assert.That(
                itemList?.Intersect(items).Any() ?? false
                && el.GetSpec().DataType.ValueType == DataValueTypes.Text, "Bad scalar element - Text");
        }

        [Test, Order(3)]
        public void NewTest_Integer()
        {
            int[] items = _testData.arrayInteger;
            var el = BdoMeta.NewScalar("integer3", items);

            var itemList = el.GetDataList<int>();

            Assert.That(
                itemList?.Intersect(items).Any() ?? false
                && el.GetSpec().DataType.ValueType == DataValueTypes.Integer, "Bad scalar element - Integer");
        }

        [Test, Order(3)]
        public void NewTest_ArrayByte()
        {
            byte[][] items = _testData.arrayArrayByte;
            var el = BdoMeta.NewScalar("byteArray4", items);

            var itemList = el.GetDataList<byte[]>();

            Assert.That(
                itemList[0]?.SequenceEqual(items[0]) == true
                && itemList[1]?.SequenceEqual(items[1]) == true
                && el.GetSpec().DataType.ValueType == DataValueTypes.ByteArray
                , "Bad scalar element - Byte array");
        }

        [Test, Order(4)]
        public void ToStringTest()
        {
            int[] items_integer = _testData.arrayInteger;
            var el = BdoMeta.NewScalar()
                .WithData(items_integer);
            var st = el.ToString();
            Assert.That(st != null, "Bad scalar element - ToString");

            double[] items_number = _testData.arrayNumber;
            el = BdoMeta.NewScalar()
                .WithData(items_number);
            st = el.ToString();
            Assert.That(st != null, "Bad scalar element - ToString");
        }
    }
}
