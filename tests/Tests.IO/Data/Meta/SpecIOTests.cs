using BindOpen.System.IO.Dtos;
using BindOpen.System.Tests;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.System.Data.Meta
{
    [TestFixture, Order(201)]
    public class SpecIOTests
    {
        private readonly string _filePath_xml = SystemData.WorkingFolder + "Spec.xml";
        private readonly string _filePath_json = SystemData.WorkingFolder + "Spec.json";

        private IBdoSpec _spec;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _spec = BdoSpecFaker.CreateSpec();
        }

        public static bool Equals(
            IBdoSpec spec1,
            IBdoSpec spec2)
        {
            var b = spec1 != null && spec2 != null
                && spec1.IsDeepEqual(spec2);
            return b;
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

            var spec = XmlHelper.LoadXml<AggregateSpecDto>(_filePath_xml).ToPoco();
            Equals(spec, _spec);
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

            var spec = JsonHelper.LoadJson<SpecDto>(_filePath_json).ToPoco();
            Equals(spec, _spec);
        }
    }
}
