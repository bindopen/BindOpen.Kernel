using BindOpen.Data.Helpers.Serialization;
using BindOpen.Data.Items;
using BindOpen.System.Diagnostics;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.Core.Data.Items
{
    [TestFixture, Order(210)]
    public class DictionaryTests
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "Dicionary.xml";
        dynamic valueSet;
        private IDictionaryDataItem _dictionary = null;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            valueSet = new { value1 = "toto", value2 = "totomax" };
        }

        private void Test(IDictionaryDataItem dictionary)
        {
            Assert.That(
                dictionary["value1"] == valueSet.value1
                && dictionary["value2"] == valueSet.value2, "Bad dictionary creation");
        }

        [Test, Order(1)]
        public void CreateDictionaryTest()
        {
            _dictionary = ItemFactory.CreateDictionary(("value1", valueSet.value1), ("value2", valueSet.value2));

            Test(_dictionary);
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

        [Test, Order(3)]
        public void SaveDictionaryTest()
        {
            if (_dictionary == null)
            {
                CreateDictionaryTest();
            }

            var log = new BdoLog();
            _dictionary.SaveXml(_filePath, log);

            string xml = string.Empty;
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Element set saving failed" + xml);
        }

        [Test, Order(4)]
        public void LoadDictionaryTest()
        {
            var log = new BdoLog();

            if (_dictionary == null || !File.Exists(_filePath))
            {
                SaveDictionaryTest();
            }

            var elementSet = XmlHelper.Load<DictionaryDataItem>(_filePath, log: log);

            string xml = string.Empty;
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Element set loading failed" + xml);

            Test(elementSet);
        }
    }
}
