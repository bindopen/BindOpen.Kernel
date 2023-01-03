using BindOpen.Extensions;
using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Runtime.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Runtime.IO.Tests.MasterData.Items
{
    [TestFixture, Order(210)]
    public class DatasourceTests
    {
        private IBdoSource _datasource;
        private readonly string _filePath_xml = GlobalVariables.WorkingFolder + "Datasource.xml";
        private readonly string _filePath_json = GlobalVariables.WorkingFolder + "Datasource.json";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void CreateDatasourceTest()
        {
            _datasource = BdoItems.NewDatasource("name", DatasourceKind.Database)
                .WithConfiguration(
                    BdoExtensions.NewConnectorConfiguration("tests.core$test")
                        ?.WithConnectionString("connectionString"));

            Assert.That(
                _datasource != null, "Bad data source creation");
        }

        private void Test(IBdoSource source)
        {
            Assert.That(
                source.GetConfiguration("tests.core$test") != null, "Datasource - Configuration not found");

            Assert.That(
                source.GetConfiguration("tests.core$test").GetConnectionString() == "connectionString", "Datasource - Configuration not found");
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

            Test(datasource);
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

            Test(datasource);
        }
    }
}
