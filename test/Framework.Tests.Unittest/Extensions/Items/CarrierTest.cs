using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.Extensions.Carriers;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Framework.Tests.UnitTest.Extensions.Runtime
{
    [TestFixture, Order(11)]
    public class CarrierTest
    {
        private DbField _field = null;
        private readonly string _filePath = SetupVariables.WorkingFolder + "Carrier.xml";

        private readonly string _fieldAlias = "alias";
        private readonly string _fieldDataModule = "dataModule";
        private readonly string _fieldDataTable = "dataTable";
        private readonly string _fieldDataTableAlias = "dataTableAlias";
        private readonly bool _fieldIsAll = true;
        private readonly bool _fieldIsForeignKey = true;
        private readonly bool _fieldIsKey = true;
        private readonly bool _fieldIsNameAsScript = true;
        private readonly bool _fieldIsReadonly = true;
        private readonly int _fieldSize = 50;
        private readonly string _fieldValueText = "=$bidule()";
        private readonly DataValueType _fieldValueType = DataValueType.Boolean;

        [SetUp]
        public void Setup()
        {
            _field = new DbField
            {
                Name = "test",
                Alias = _fieldAlias,
                DataModule = _fieldDataModule,
                DataTable = _fieldDataTable,
                DataTableAlias = _fieldDataTableAlias,
                IsAll = _fieldIsAll,
                IsForeignKey = _fieldIsForeignKey,
                IsKey = _fieldIsKey,
                IsNameAsScript = _fieldIsNameAsScript,
                IsReadonly = _fieldIsReadonly,
                Size = _fieldSize,
                Value = _fieldValueText.CreateScript(),
                ValueType = _fieldValueType
            };
        }

        [Test, Order(1)]
        public void TestCreateCarrier()
        {
            Test(_field);
        }

        [Test, Order(2)]
        public void TestSaveCarrier()
        {
            IBdoLog log = new BdoLog();

            _field.SaveXml(_filePath, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Carrier saving failed. Result was '" + xml);
        }

        [Test, Order(3)]
        public void TestLoadCarrier()
        {
            IBdoLog log = new BdoLog();

            if (_field == null || !File.Exists(_filePath))
                TestSaveCarrier();

            BdoCarrierConfiguration configuration = XmlHelper.Load<BdoCarrierConfiguration>(_filePath, null, null, log);
            var field = SetupVariables.BdoHost.Scope.CreateCarrier<DbField>(configuration, null, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Carrier loading failed. Result was '" + xml);

            Test(field);
        }

        private void Test(DbField field)
        {
            Assert.That(field != null, "Field missing");
            if (field != null)
            {
                Assert.That(field.Alias == _fieldAlias, "Bad field alias");
                Assert.That(field.DataModule == _fieldDataModule, "Bad field data module");
                Assert.That(field.DataTable == _fieldDataTable, "Bad field data table");
                Assert.That(field.DataTableAlias == _fieldDataTableAlias, "Bad field data table alias");
                Assert.That(field.IsForeignKey == _fieldIsForeignKey, "Bad field foreign key indicator");
                Assert.That(field.IsKey == _fieldIsKey, "Bad field key indicator");
                Assert.That(field.IsNameAsScript == _fieldIsNameAsScript, "Bad field name-as-script indicator");
                Assert.That(field.IsReadonly == _fieldIsReadonly, "Bad field read-only indicator");
                Assert.That(field.Size == _fieldSize, "Bad field size");
                Assert.That(field.ValueType == _fieldValueType, "Bad field value type");
                Assert.That(field.Value?.Text == _fieldValueText, "Bad field value");
            }
        }

    }

}
