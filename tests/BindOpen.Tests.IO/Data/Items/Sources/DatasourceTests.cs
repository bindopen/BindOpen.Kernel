using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Connectors;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.IO.Data
{
    [TestFixture, Order(210)]
    public class DatasourceTests
    {
        private IBdoDatasource _datasource;
        private readonly string _filePath_xml = Tests.WorkingFolder + "Datasource.xml";
        private readonly string _filePath_json = Tests.WorkingFolder + "Datasource.json";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        public static bool Equals(
            IBdoDatasource source1,
            IBdoDatasource source2)
        {
            var b = source1 != null && source2 != null;

            source1.WithDeepEqual(source2)
                .IgnoreProperty<BdoMetaData>(x => x.Id).Assert();

            return b;
        }

        [Test, Order(1)]
        public void CreateTest()
        {
            _datasource = BdoData.NewDatasource("name", DatasourceKind.Database)
                .With(
                    BdoConfig.New("bindopen.tests.kernel$test")
                        .WithConnectionString("connectionString")
                        .With(
                            BdoMeta.New("name1", "value1")));

            Assert.That(
                _datasource != null, "Bad data source creation");
        }

        // Xml

        [Test, Order(2)]
        public void SaveXmlTest()
        {
            if (_datasource == null)
            {
                CreateTest();
            }

            var isSaved = _datasource.ToDto().SaveXml(_filePath_xml);
            Assert.That(isSaved, "Data source saving failed. ");
        }

        [Test, Order(3)]
        public void LoadXmlTest()
        {
            if (_datasource == null || !File.Exists(_filePath_xml))
            {
                SaveXmlTest();
            }

            var datasource = XmlHelper.LoadXml<DatasourceDto>(_filePath_xml).ToPoco();
            Assert.That(Equals(datasource, _datasource), "Error while loading");
        }

        // Json

        [Test, Order(4)]
        public void SaveJsonTest()
        {
            if (_datasource == null)
            {
                CreateTest();
            }

            var isSaved = _datasource.ToDto().SaveJson(_filePath_json);
            Assert.That(isSaved, "Data source saving failed. ");
        }

        [Test, Order(5)]
        public void LoadJsonTest()
        {
            if (_datasource == null || !File.Exists(_filePath_json))
            {
                SaveJsonTest();
            }

            var datasource = JsonHelper.LoadJson<DatasourceDto>(_filePath_json).ToPoco();
            Assert.That(Equals(datasource, _datasource), "Error while loading");
        }
    }
}
