using BindOpen.System.Tests;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.System.Data.Meta
{
    [TestFixture, Order(201)]
    public class SpecTests
    {
        private readonly string _filePath_xml = SystemData.WorkingFolder + "Spec.xml";
        private readonly string _filePath_json = SystemData.WorkingFolder + "Spec.json";

        private BdoSpec _spec;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _spec = BdoData.NewSpec("object1", DataValueTypes.Date);
        }

        private void Test(IBdoSpec spec)
        {
            Assert.That(spec.IsDeepEqual(_spec) == true, "Bad obj element set - Count");
        }

        // Xml

        [Test, Order(5)]
        public void SaveXmlTest()
        {
            var isSaved = _spec.ToDto().SaveXml(_filePath_xml);
            Assert.That(isSaved, "Meta list saving failed. ");
        }

        [Test, Order(6)]
        public void LoadXmlTest()
        {
            if (_spec == null || !File.Exists(_filePath_xml))
            {
                SaveXmlTest();
            }

            var metaSet = XmlHelper.LoadXml<SpecDto>(_filePath_xml).ToPoco();
            Equals(metaSet, _spec);
        }

        // Json

        [Test, Order(7)]
        public void SaveJsonTest()
        {
            var isSaved = _spec.ToDto().SaveJson(_filePath_json);
            Assert.That(isSaved, "Meta list saving failed. ");
        }

        [Test, Order(8)]
        public void LoadJsonTest()
        {
            if (_spec == null || !File.Exists(_filePath_json))
            {
                SaveJsonTest();
            }

            var metaSet = JsonHelper.LoadJson<SpecDto>(_filePath_json).ToPoco();
            Equals(metaSet, _spec);
        }
    }
}
