using BindOpen.Data.Items;
using NUnit.Framework;

namespace BindOpen.Tests.Core.Data.Items
{
    [TestFixture, Order(210)]
    public class DictionaryTests
    {
        dynamic valueSet;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            valueSet = new { value1 = "toto", value2 = "totomax" };
        }

        [Test, Order(1)]
        public void CreateDictionaryTest()
        {
            var dictionary = ItemFactory.CreateDictionary(("value1", valueSet.value1), ("value2", valueSet.value2));

            Assert.That(
                dictionary["value1"] == valueSet.value1
                && dictionary["value2"] == valueSet.value2, "Bad dictionary creation");
        }

        [Test, Order(2)]
        public void EqualsDictionaryTest()
        {
            var dictionary1 = ItemFactory.CreateDictionary(("value1", valueSet.value1), ("value2", valueSet.value2));
            var dictionary2 = ItemFactory.CreateDictionary(
                new DataKeyValue("value1", valueSet.value1),
                new DataKeyValue("value2", valueSet.value2));

            Assert.That(
                dictionary1.Equals(dictionary2), "Bad dictionary equal funtion");
        }
    }
}
