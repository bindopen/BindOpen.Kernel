using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.Extensions.Carriers;
using BindOpen.Framework.UnitTest.Setup;
using NUnit.Framework;

namespace BindOpen.Framework.UnitTest.Extensions.Runtime
{
    [TestFixture, Order(11)]
    public class CarrierTest
    {
        DbField _Field = null;
        string _FilePath = SetupVariables.WorkingFolder + @"carrier.xml";

        string _FieldAlias = "alias";
        string _FieldDataModule = "dataModule";
        string _FieldDataTable = "dataTable";
        string _FieldDataTableAlias = "dataTableAlias";
        bool _FieldIsAll = true;
        bool _FieldIsForeignKey = true;
        bool _FieldIsKey = true;
        bool _FieldIsNameAsScript = true;
        bool _FieldIsReadonly = true;
        int _FieldSize = 50;
        string _FieldValueText = "=$bidule()";
        Core.Data.Common.DataValueType _FieldValueType = Core.Data.Common.DataValueType.Boolean;

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void TestCreateCarrier()
        {
            this._Field = new DbField();
            this._Field.Name = "test";
            this._Field.Alias = this._FieldAlias;
            this._Field.DataModule = this._FieldDataModule;
            this._Field.DataTable = this._FieldDataTable;
            this._Field.DataTableAlias = this._FieldDataTableAlias;
            this._Field.IsAll = this._FieldIsAll;
            this._Field.IsForeignKey = this._FieldIsForeignKey;
            this._Field.IsKey = this._FieldIsKey;
            this._Field.IsNameAsScript = this._FieldIsNameAsScript;
            this._Field.IsReadonly = this._FieldIsReadonly;
            this._Field.Size = this._FieldSize;
            this._Field.Value = new Core.Data.Expression.DataExpression(this._FieldValueText);
            this._Field.ValueType = this._FieldValueType;

            this.Test(this._Field);
        }

        [Test, Order(2)]
        public void TestSaveCarrier()
        {
            ILog log = new Log();

            if (this._Field == null)
                this.TestCreateCarrier();

            this._Field.SaveXml(this._FilePath, log);
        }

        [Test, Order(3)]
        public void TestLoadCarrier()
        {
            ILog log = new Log();

            DbField field = DbField.Load<DbField>(this._FilePath, log, SetupVariables.AppScope);
            this.Test(field);
        }

        private void Test(DbField field)
        {
            Assert.That(field != null, "Field missing");
            if (field != null)
            {
                Assert.That(field.Alias == this._FieldAlias, "Bad field alias");
                Assert.That(field.DataModule == this._FieldDataModule, "Bad field data module");
                Assert.That(field.DataTable == this._FieldDataTable, "Bad field data table");
                Assert.That(field.DataTableAlias == this._FieldDataTableAlias, "Bad field data table alias");
                Assert.That(field.IsForeignKey == this._FieldIsForeignKey, "Bad field foreign key indicator");
                Assert.That(field.IsKey == this._FieldIsKey, "Bad field key indicator");
                Assert.That(field.IsNameAsScript == this._FieldIsNameAsScript, "Bad field name-as-script indicator");
                Assert.That(field.IsReadonly == this._FieldIsReadonly, "Bad field read-only indicator");
                Assert.That(field.Size == this._FieldSize, "Bad field size");
                Assert.That(field.Value?.Text == this._FieldValueText, "Bad field value");
                Assert.That(field.ValueType == this._FieldValueType, "Bad field value type");
            }
        }

    }

}
