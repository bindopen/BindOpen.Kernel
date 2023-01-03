using BindOpen.Data.Items;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Runtime.Tests.MetaData.Items
{
    [TestFixture, Order(210)]
    public class BdoDictionaryTests
    {
        dynamic _valueSet;
        private BdoDictionary _dictionary = null;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();
            _valueSet = new
            {
                valueStar = f.Random.Word(),
                value1 = f.Random.Word(),
                value2 = f.Random.Word(),
                value3 = f.Random.Word()
            };
        }

        public void Test(IBdoDictionary dictionary)
        {
            Assert.That(
                dictionary["value1"] == _valueSet.value1
                && dictionary["value2"] == _valueSet.value2
                && dictionary["value3"] == _valueSet.value3, "Bad dictionary creation");
        }

        [Test, Order(1)]
        public void Create1Test()
        {
            _dictionary = BdoItems.NewDictionary(
                ("value1", _valueSet.value1),
                ("value1", _valueSet.value1),
                ("value2", _valueSet.value2),
                ("value3", _valueSet.value3));

            Test(_dictionary);
        }

        [Test, Order(2)]
        public void Create2Test()
        {
            _dictionary = new[] {
                ("value1", _valueSet.value1 as string),
                ("value2", _valueSet.value2 as string),
                ("value3", _valueSet.value3 as string) };

            Test(_dictionary);
        }

        [Test, Order(3)]
        public void Create3Test()
        {
            _dictionary = _valueSet.valueStar as string;

            Assert.That(
                _dictionary["*"] == _valueSet.valueStar as string, "Bad dictionary creation");
            Assert.That(
                _dictionary.Get() == _valueSet.valueStar as string, "Bad dictionary creation");
        }

        [Test, Order(4)]
        public void EqualsTest()
        {
            var dictionary1 = BdoItems.NewDictionary(
                ("value1", _valueSet.value1),
                ("value2", _valueSet.value2),
                ("value3", _valueSet.value3));

            Assert.That(
                dictionary1.Equals(dictionary1), "Bad dictionary equal funtion");
        }

        [Test, Order(5)]
        public void Create5Test()
        {
            _dictionary = new[] {
                ("*", _valueSet.valueStar as string),
                ("value1", _valueSet.value1 as string),
                ("value2", _valueSet.value2 as string),
                ("value3", _valueSet.value3 as string) };

            var valueStar = _dictionary["*"];
            var value1 = _dictionary["value1"];
            var value2 = _dictionary["value2"];
            var value3 = _dictionary["value3"];
            var value4 = _dictionary["value4"];
            var value5 = _dictionary["value5", "*"];

            Assert.That(
                valueStar == _valueSet.valueStar as string, "Bad dictionary creation");
            Assert.That(
                value1 == _valueSet.value1 as string, "Bad dictionary creation");
            Assert.That(
                value2 == _valueSet.value2 as string, "Bad dictionary creation");
            Assert.That(
                value3 == _valueSet.value3 as string, "Bad dictionary creation");
            Assert.That(
                value4 == null, "Bad dictionary creation");
            Assert.That(
                value5 == _valueSet.valueStar as string, "Bad dictionary creation");
        }
    }
}
