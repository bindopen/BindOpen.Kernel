using BindOpen.Meta;
using BindOpen.Meta.Elements;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Runtime.Tests.MetaData.Elements.Scalar
{
    [TestFixture, Order(202)]
    public class ScalarElementTests
    {
        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = ScalarElementFaker.Fake();
        }

        [Test, Order(1)]
        public void CreateElementTest_Number()
        {
            double[] items = _testData.arrayNumber;

            var el = BdoMeta.NewScalar("number1", DataValueTypes.Number, items);
            var itemList = el.GetItemList<double>();
            Assert.That(
                itemList?.Intersect(items).Any() ?? false, "Bad scalar element - Number");

            el = BdoMeta.NewScalar<double>("number1", items);
            itemList = el.GetItemList<double>();
            Assert.That(
                itemList?.Intersect(items).Any() ?? false, "Bad scalar element - Number");
        }

        [Test, Order(2)]
        public void CreateElementTest_String()
        {
            string[] items = _testData.arrayString;
            var el = BdoMeta.NewScalar("text2", items);

            var itemList = el.GetItemList<string>();

            Assert.That(
                itemList?.Intersect(items).Any() ?? false, "Bad scalar element - Text");
        }

        [Test, Order(3)]
        public void CreateElementTest_Integer()
        {
            int[] items = _testData.arrayInteger;
            var el = BdoMeta.NewScalar("integer3", items);

            var itemList = el.GetItemList<int>();

            Assert.That(
                itemList?.Intersect(items).Any() ?? false, "Bad scalar element - Integer");
        }

        [Test, Order(3)]
        public void CreateElementTest_ArrayByte()
        {
            byte[][] items = _testData.arrayArrayByte;
            var el = BdoMeta.NewScalar("byteArray4", items);

            var itemList = el.GetItemList<byte[]>();

            Assert.That(
                itemList[0]?.SequenceEqual(items[0]) == true
                && itemList[1]?.SequenceEqual(items[1]) == true
                , "Bad scalar element - Byte array");
        }

        [Test, Order(4)]
        public void ElementToStringTest()
        {
            int[] items_integer = _testData.arrayInteger;
            var el = BdoMeta.NewScalar().WithItem(items_integer);
            var st = el.ToString();
            Assert.That(st != null, "Bad scalar element - ToString");

            double[] items_number = _testData.arrayNumber;
            el = BdoMeta.NewScalar().WithItem(items_number);
            st = el.ToString();
            Assert.That(st != null, "Bad scalar element - ToString");
        }
    }
}
