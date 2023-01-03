using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Runtime.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Runtime.IO.Tests.MasterData.Items
{
    [TestFixture, Order(210)]
    public class DictionaryTests
    {
        private readonly string _filePath_xml = GlobalVariables.WorkingFolder + "Dictionary.xml";
        private readonly string _filePath_json = GlobalVariables.WorkingFolder + "Dictionary.json";
        dynamic _valueSet;
        private IBdoDictionary _dictionary = null;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _valueSet = new { value1 = "toto", value2 = "totomax" };
        }

        private void Test(IBdoDictionary dictionary)
        {
            Assert.That(
                dictionary["value1"] == _valueSet.value1
                && dictionary["value2"] == _valueSet.value2, "Bad dictionary creation");
        }

        [Test, Order(1)]
        public void CreateTest()
        {
            _dictionary = BdoItems.NewDictionary(
                ("value1", _valueSet.value1),
                ("value2", _valueSet.value2));

            Test(_dictionary);
        }

        [Test, Order(2)]
        public void EqualsTest()
        {
            var dictionary1 = BdoItems.NewDictionary(
                ("value1", _valueSet.value1),
                ("value2", _valueSet.value2));
            var dictionary2 = BdoItems.NewDictionary(
                BdoItems.NewKeyPair("value1", _valueSet.value1),
                BdoItems.NewKeyPair("value2", _valueSet.value2));

            Assert.That(
                dictionary1.Equals(dictionary2), "Bad dictionary equal funtion");
        }

        // Xml

        [Test, Order(3)]
        public void SaveXmlTest()
        {
            if (_dictionary == null)
            {
                CreateTest();
            }

            var isSaved = _dictionary.ToDto().SaveXml(_filePath_xml);

            Assert.That(isSaved, "Element set saving failed");
        }

        [Test, Order(4)]
        public void LoadXmlTest()
        {
            if (_dictionary == null || !File.Exists(_filePath_xml))
            {
                SaveXmlTest();
            }

            var elementSet = XmlHelper.LoadXml<DictionaryDataItemDto>(_filePath_xml).ToPoco();

            Test(elementSet);
        }

        // Json

        [Test, Order(5)]
        public void SaveJsonTest()
        {
            if (_dictionary == null)
            {
                CreateTest();
            }

            var isSaved = _dictionary.ToDto().SaveJson(_filePath_json);

            Assert.That(isSaved, "Element set saving failed");
        }

        [Test, Order(6)]
        public void LoadJsonTest()
        {
            if (_dictionary == null || !File.Exists(_filePath_json))
            {
                SaveJsonTest();
            }

            var elementSet = JsonHelper.LoadJson<DictionaryDataItemDto>(_filePath_json).ToPoco();

            Test(elementSet);
        }

    }
}
