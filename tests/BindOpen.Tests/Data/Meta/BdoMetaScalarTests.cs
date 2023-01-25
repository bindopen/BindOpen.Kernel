using BindOpen.Data;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Tests.Data
{
    [TestFixture, Order(202)]
    public class BdoMetaScalarTests
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

            var el = BdoData.NewMetaScalar("number1", DataValueTypes.Number, items);
            var itemList = el.Items<double>();
            Assert.That(
                itemList?.Intersect(items).Any() ?? false
                && el.ValueType == DataValueTypes.Number, "Bad scalar element - Number");

            el = BdoData.NewMetaScalar<double>("number1", items);
            itemList = el.Items<double>();
            Assert.That(
                itemList?.Intersect(items).Any() ?? false
                && el.ValueType == DataValueTypes.Number, "Bad scalar element - Number");
        }

        [Test, Order(2)]
        public void NewTest_String()
        {
            string[] items = _testData.arrayString;
            var el = BdoData.NewMetaScalar("text2", items);

            var itemList = el.Items<string>();

            Assert.That(
                itemList?.Intersect(items).Any() ?? false
                && el.ValueType == DataValueTypes.Text, "Bad scalar element - Text");
        }

        [Test, Order(3)]
        public void NewTest_Integer()
        {
            int[] items = _testData.arrayInteger;
            var el = BdoData.NewMetaScalar("integer3", items);

            var itemList = el.Items<int>();

            Assert.That(
                itemList?.Intersect(items).Any() ?? false
                && el.ValueType == DataValueTypes.Integer, "Bad scalar element - Integer");
        }

        [Test, Order(3)]
        public void NewTest_ArrayByte()
        {
            byte[][] items = _testData.arrayArrayByte;
            var el = BdoData.NewMetaScalar("byteArray4", items);

            var itemList = el.Items<byte[]>();

            Assert.That(
                itemList[0]?.SequenceEqual(items[0]) == true
                && itemList[1]?.SequenceEqual(items[1]) == true
                && el.ValueType == DataValueTypes.ByteArray
                , "Bad scalar element - Byte array");
        }

        [Test, Order(4)]
        public void ToStringTest()
        {
            int[] items_integer = _testData.arrayInteger;
            var el = BdoData.NewMetaScalar().WithItems(items_integer);
            var st = el.ToString();
            Assert.That(st != null, "Bad scalar element - ToString");

            double[] items_number = _testData.arrayNumber;
            el = BdoData.NewMetaScalar().WithItems(items_number);
            st = el.ToString();
            Assert.That(st != null, "Bad scalar element - ToString");
        }
    }
}
