using BindOpen.MetaData;
using BindOpen.MetaData.Items;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.IO.MetaData
{
    [TestFixture, Order(210)]
    public class DictionaryTests
    {
        private readonly string _filePath_xml = GlobalVariables.WorkingFolder + "Dictionary.xml";
        private readonly string _filePath_json = GlobalVariables.WorkingFolder + "Dictionary.json";
        dynamic _valueSet;
        private IBdoDictionary _dico = null;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _valueSet = new { value1 = "toto", value2 = "totomax" };
        }

        private void Test(IBdoDictionary dico)
        {
            Assert.That(
                dico["value1"] == _valueSet.value1
                && dico["value2"] == _valueSet.value2, "Bad dictionary creation");
        }

        [Test, Order(1)]
        public void CreateTest()
        {
            _dico = BdoMeta.NewDictionary(
                ("value1", _valueSet.value1),
                ("value2", _valueSet.value2));

            Test(_dico);
        }

        [Test, Order(2)]
        public void EqualsTest()
        {
            var dico1 = BdoMeta.NewDictionary(
                ("value1", _valueSet.value1),
                ("value2", _valueSet.value2));
            var dico2 = BdoMeta.NewDictionary(
                BdoMeta.NewKeyPair("value1", _valueSet.value1),
                BdoMeta.NewKeyPair("value2", _valueSet.value2));

            Assert.That(
                dico1.Equals(dico2), "Bad dictionary equal funtion");
        }

        // Xml

        [Test, Order(3)]
        public void SaveXmlTest()
        {
            if (_dico == null)
            {
                CreateTest();
            }

            var isSaved = _dico.ToDto().SaveXml(_filePath_xml);

            Assert.That(isSaved, "Element set saving failed");
        }

        [Test, Order(4)]
        public void LoadXmlTest()
        {
            if (_dico == null || !File.Exists(_filePath_xml))
            {
                SaveXmlTest();
            }

            var elemSet = XmlHelper.LoadXml<DictionaryDataItemDto>(_filePath_xml).ToPoco();

            Test(elemSet);
        }

        // Json

        [Test, Order(5)]
        public void SaveJsonTest()
        {
            if (_dico == null)
            {
                CreateTest();
            }

            var isSaved = _dico.ToDto().SaveJson(_filePath_json);

            Assert.That(isSaved, "Element set saving failed");
        }

        [Test, Order(6)]
        public void LoadJsonTest()
        {
            if (_dico == null || !File.Exists(_filePath_json))
            {
                SaveJsonTest();
            }

            var elemSet = JsonHelper.LoadJson<DictionaryDataItemDto>(_filePath_json).ToPoco();

            Test(elemSet);
        }

    }
}
