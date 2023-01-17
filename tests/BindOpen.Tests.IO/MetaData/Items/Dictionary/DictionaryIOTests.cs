using BindOpen.MetaData;
using BindOpen.MetaData.Items;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace BindOpen.Tests.IO.MetaData
{
    [TestFixture, Order(210)]
    public class DictionaryIOTests
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

        public static bool Equals(
            IBdoDictionary dico1,
            IBdoDictionary dico2)
        {
            var b = dico1 != null && dico2 != null
                && dico1.Count == dico2.Count
                && dico1.Keys.All(q => dico1[q] == dico2[q]);
            return b;
        }

        [Test, Order(1)]
        public void CreateTest()
        {
            _dico = BdoMeta.NewDictionary(
                ("value1", _valueSet.value1),
                ("value2", _valueSet.value2));
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

            var dico = XmlHelper.LoadXml<DictionaryDto>(_filePath_xml).ToPoco();
            Assert.That(Equals(dico, _dico), "Error while loading");
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

            var dico = JsonHelper.LoadJson<DictionaryDto>(_filePath_json).ToPoco();
            Assert.That(Equals(dico, _dico), "Error while loading");
        }
    }
}
