using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.Extensions.Carriers;
using BindOpen.Framework.UnitTest.Setup;
using NUnit.Framework;

namespace BindOpen.Framework.UnitTest.Extensions.Runtime
{
    [TestFixture, Order(11)]
    public class CarrierTest
    {
        DbField _field = null;
        string _filePath = SetupVariables.WorkingFolder + @"carrier.xml";

        string _fieldAlias = "alias";
        string _fieldDataModule = "dataModule";
        string _fieldDataTable = "dataTable";
        string _fieldDataTableAlias = "dataTableAlias";
        bool _fieldIsAll = true;
        bool _fieldIsForeignKey = true;
        bool _fieldIsKey = true;
        bool _fieldIsNameAsScript = true;
        bool _fieldIsReadonly = true;
        int _fieldSize = 50;
        string _fieldValueText = "=$bidule()";
        DataValueType _fieldValueType = DataValueType.Boolean;

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void TestCreateCarrier()
        {
            _field = new DbField();
            _field.Name = "test";
            _field.Alias = _fieldAlias;
            _field.DataModule = _fieldDataModule;
            _field.DataTable = _fieldDataTable;
            _field.DataTableAlias = _fieldDataTableAlias;
            _field.IsAll = _fieldIsAll;
            _field.IsForeignKey = _fieldIsForeignKey;
            _field.IsKey = _fieldIsKey;
            _field.IsNameAsScript = _fieldIsNameAsScript;
            _field.IsReadonly = _fieldIsReadonly;
            _field.Size = _fieldSize;
            _field.Value = _fieldValueText.CreateScript();
            _field.ValueType = _fieldValueType;

            Test(_field);
        }

        [Test, Order(2)]
        public void TestSaveCarrier()
        {
            ILog log = new Log();

            if (_field == null)
                TestCreateCarrier();

            _field.SaveXml(_filePath, log);
        }

        [Test, Order(3)]
        public void TestLoadCarrier()
        {
            ILog log = new Log();

            DbField field = DbField.Load<DbField>(_filePath, log, SetupVariables.AppScope);
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
                Assert.That(field.Value?.Text == _fieldValueText, "Bad field value");
                Assert.That(field.ValueType == _fieldValueType, "Bad field value type");
            }
        }

    }

}
