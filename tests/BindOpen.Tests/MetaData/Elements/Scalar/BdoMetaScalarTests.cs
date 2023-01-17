using BindOpen.MetaData;
using BindOpen.MetaData.Elements;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Tests.MetaData
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
        public void CreateElementTest_Number()
        {
            double[] items = _testData.arrayNumber;

            var el = BdoMeta.NewScalar("number1", DataValueTypes.Number, items);
            var itemList = el.Items<double>();
            Assert.That(
                itemList?.Intersect(items).Any() ?? false
                && el.ValueType == DataValueTypes.Number, "Bad scalar element - Number");

            el = BdoMeta.NewScalar<double>("number1", items);
            itemList = el.Items<double>();
            Assert.That(
                itemList?.Intersect(items).Any() ?? false
                && el.ValueType == DataValueTypes.Number, "Bad scalar element - Number");
        }

        [Test, Order(2)]
        public void CreateElementTest_String()
        {
            string[] items = _testData.arrayString;
            var el = BdoMeta.NewScalar("text2", items);

            var itemList = el.Items<string>();

            Assert.That(
                itemList?.Intersect(items).Any() ?? false
                && el.ValueType == DataValueTypes.Text, "Bad scalar element - Text");
        }

        [Test, Order(3)]
        public void CreateElementTest_Integer()
        {
            int[] items = _testData.arrayInteger;
            var el = BdoMeta.NewScalar("integer3", items);

            var itemList = el.Items<int>();

            Assert.That(
                itemList?.Intersect(items).Any() ?? false
                && el.ValueType == DataValueTypes.Integer, "Bad scalar element - Integer");
        }

        [Test, Order(3)]
        public void CreateElementTest_ArrayByte()
        {
            byte[][] items = _testData.arrayArrayByte;
            var el = BdoMeta.NewScalar("byteArray4", items);

            var itemList = el.Items<byte[]>();

            Assert.That(
                itemList[0]?.SequenceEqual(items[0]) == true
                && itemList[1]?.SequenceEqual(items[1]) == true
                && el.ValueType == DataValueTypes.ByteArray
                , "Bad scalar element - Byte array");
        }

        [Test, Order(4)]
        public void ElementToStringTest()
        {
            int[] items_integer = _testData.arrayInteger;
            var el = BdoMeta.NewScalar().WithItems(items_integer);
            var st = el.ToString();
            Assert.That(st != null, "Bad scalar element - ToString");

            double[] items_number = _testData.arrayNumber;
            el = BdoMeta.NewScalar().WithItems(items_number);
            st = el.ToString();
            Assert.That(st != null, "Bad scalar element - ToString");
        }
    }
}
