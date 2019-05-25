using System.IO;
using BindOpen.Framework.Core.Application.Depots.Datasources;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using NUnit.Framework;

namespace BindOpen.Framework.Tests.UnitTest.Application.Depots.Datasources
{
    [TestFixture, Order(21)]
    public class DataSourceDepotTest
    {
        private readonly string _filePath = SetupVariables.WorkingFolder + "DataSourceDepot.xml";

        private DataSourceDepot _dataSourceDepot = null;

        [SetUp]
        public void Setup()
        {
            Log log = new Log();

            _dataSourceDepot = new DataSourceDepot(
                new DataSource(
                    "smtp_default",
                    DataSourceKind.EmailServer,
                    new ConnectorConfiguration(
                        "messages$smtp",
                        ElementFactory.CreateScalar("host", "smtp.googlemail.com"),
                        ElementFactory.CreateScalar("port", DataValueType.Integer, "587"),
                        ElementFactory.CreateScalar("isDefaultCredentialsUsed", DataValueType.Boolean, true),
                        ElementFactory.CreateScalar("isSslEnabled", DataValueType.Boolean, true),
                        ElementFactory.CreateScalar("timeout", DataValueType.Integer, 60000),
                        ElementFactory.CreateScalar("login", "login@meltingsoft.com"),
                        ElementFactory.CreateScalar("password", "passwordA"))));
        }

        public void TestDataSourceDepotSet(DataSourceDepot depot)
        {
            Assert.That(
                depot.HasItem("smtp_default")
                && depot.GetItem("smtp_default")?.Configurations?.Count==1
                && depot.GetItem("smtp_default")?.Configurations[0]?.GetElementObject<int>("timeout") == 60000
                , "Bad data source depot");
        }

        [Test]
        public void TestCreateDataSourceDepotSet()
        {
            TestDataSourceDepotSet(_dataSourceDepot);
        }

        [Test]
        public void TestSaveDataSourceDepot()
        {
            ILog log = new Log();

            _dataSourceDepot.SaveXml(_filePath, log);

            Assert.That(!log.HasErrorsOrExceptions(), "Data source depots saving failed. Result was '" + log.ToXml());
        }

        [Test]
        public void TestLoadDataSourceDepot()
        {
            ILog log = new Log();

            if (_dataSourceDepot == null || !File.Exists(_filePath))
                TestSaveDataSourceDepot();

            var dataSourceDepot = XmlHelper.Load<DataSourceDepot>(_filePath, null, null, log);

            Assert.That(!log.HasErrorsOrExceptions(), "Data source depot loading failed. Result was '" + log.ToXml());

            TestDataSourceDepotSet(dataSourceDepot);
        }
    }
}
