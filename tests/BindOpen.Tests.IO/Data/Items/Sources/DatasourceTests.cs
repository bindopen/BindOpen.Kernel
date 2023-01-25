using BindOpen.Extensions;
using BindOpen.Data;
using BindOpen.Data.Items;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.IO.Meta
{
    [TestFixture, Order(210)]
    public class DatasourceTests
    {
        private IBdoDatasource _datasource;
        private readonly string _filePath_xml = GlobalVariables.WorkingFolder + "Datasource.xml";
        private readonly string _filePath_json = GlobalVariables.WorkingFolder + "Datasource.json";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        public static bool Equals(
            IBdoDatasource source1,
            IBdoDatasource source2)
        {
            var b = source1 != null && source2 != null
                && source1.IsDeepEqual(source2);
            return b;
        }

        [Test, Order(1)]
        public void CreateDatasourceTest()
        {
            _datasource = BdoData.NewDatasource("name", DatasourceKind.Database)
                .WithConfig(
                    BdoExt.NewConnectorConfig("tests.core$test")
                        ?.WithConnectionString("connectionString"));

            Assert.That(
                _datasource != null, "Bad data source creation");
        }

        // Xml

        [Test, Order(2)]
        public void SaveXmlBdoElementSetTest()
        {
            if (_datasource == null)
            {
                CreateDatasourceTest();
            }

            var isSaved = _datasource.ToDto().SaveXml(_filePath_xml);
            Assert.That(isSaved, "Element set saving failed. ");
        }

        [Test, Order(3)]
        public void LoadXmlBdoElementSetTest()
        {
            if (_datasource == null || !File.Exists(_filePath_xml))
            {
                SaveXmlBdoElementSetTest();
            }

            var datasource = XmlHelper.LoadXml<DatasourceDto>(_filePath_xml).ToPoco();
            Assert.That(Equals(datasource, _datasource), "Error while loading");
        }

        // Json

        [Test, Order(4)]
        public void SaveJsonBdoElementSetTest()
        {
            if (_datasource == null)
            {
                CreateDatasourceTest();
            }

            var isSaved = _datasource.ToDto().SaveJson(_filePath_json);
            Assert.That(isSaved, "Element set saving failed. ");
        }

        [Test, Order(5)]
        public void LoadJsonBdoElementSetTest()
        {
            if (_datasource == null || !File.Exists(_filePath_json))
            {
                SaveJsonBdoElementSetTest();
            }

            var datasource = JsonHelper.LoadJson<DatasourceDto>(_filePath_json).ToPoco();
            Assert.That(Equals(datasource, _datasource), "Error while loading");
        }
    }
}
