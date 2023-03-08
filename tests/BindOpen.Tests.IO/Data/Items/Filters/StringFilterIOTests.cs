using BindOpen.Data;
using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace BindOpen.Tests.IO.Data
{
    [TestFixture, Order(210)]
    public class StringFilterIOTests
    {
        private readonly string _filePath_xml = Kernel.Tests.WorkingFolder + "StringFilter.xml";
        private readonly string _filePath_json = Kernel.Tests.WorkingFolder + "StringFilter.json";
        dynamic _valueSet;
        private IBdoStringFilter _filter = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();
            _valueSet = new
            {
                AddedValues = f.Random.WordsArray(5, 10),
                RemovedValues = f.Random.WordsArray(5, 10)
            };
        }

        public static bool Equals(
            IBdoStringFilter filter1,
            IBdoStringFilter filter2)
        {
            var b = filter1 != null && filter2 != null
                && filter1.IsDeepEqual(filter2);
            return b;
        }

        [Test, Order(1)]
        public void CreateTest()
        {
            _filter = BdoData.NewStringFilter(
                (_valueSet.AddedValues as string[]).ToList(),
                (_valueSet.RemovedValues as string[]).ToList());
        }

        // Xml

        [Test, Order(3)]
        public void SaveXmlTest()
        {
            if (_filter == null)
            {
                CreateTest();
            }

            var isSaved = _filter.ToDto().SaveXml(_filePath_xml);
            Assert.That(isSaved, "String filter saving failed");
        }

        [Test, Order(4)]
        public void LoadXmlTest()
        {
            if (_filter == null || !File.Exists(_filePath_xml))
            {
                SaveXmlTest();
            }

            var filter = XmlHelper.LoadXml<StringFilterDto>(_filePath_xml).ToPoco();
            Assert.That(Equals(filter, _filter), "Error while loading");
        }

        // Json

        [Test, Order(5)]
        public void SaveJsonTest()
        {
            if (_filter == null)
            {
                CreateTest();
            }

            var isSaved = _filter.ToDto().SaveJson(_filePath_json);
            Assert.That(isSaved, "String filter saving failed");
        }

        [Test, Order(6)]
        public void LoadJsonTest()
        {
            if (_filter == null || !File.Exists(_filePath_json))
            {
                SaveJsonTest();
            }

            var filter = JsonHelper.LoadJson<StringFilterDto>(_filePath_json).ToPoco();
            Assert.That(Equals(filter, _filter), "Error while loading");
        }
    }
}
