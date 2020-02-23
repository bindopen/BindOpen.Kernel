using BindOpen.Data.Common;
using BindOpen.Data.Expression;
using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using System;
using System.Xml.Serialization;

namespace BindOpen.Tests.Core.Extensions.Carriers
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    [XmlType("DbField", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "dbField", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    [BdoCarrier(
        Name = "tests.core$dbField",
        DatasourceKind = DatasourceKind.Database,
        Description = "Database field.",
        CreationDate = "2016-09-14"
    )]
    public class TestCarrier : BdoCarrier
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Indicates wheteher this instance represents all the fields.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "isAll")]
        public Boolean IsAll { get; set; }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "dataModule")]
        public string DataModule { get; set; }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "schema")]
        public string Schema { get; set; }

        /// <summary>
        /// Data table of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "dataTable")]
        public string DataTable { get; set; }

        /// <summary>
        /// Alias of the data table of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "dataTableAlias")]
        public string DataTableAlias { get; set; }

        /// <summary>
        /// Alias of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "alias")]
        public string Alias { get; set; }

        /// <summary>
        /// Size of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "size")]
        public int Size { get; set; }

        /// <summary>
        /// Value of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "value")]
        public DataExpression Value { get; set; }

        /// <summary>
        /// Indicates wheteher this instance is a key.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "isKey")]
        public bool IsKey { get; set; }

        /// <summary>
        /// Indicates wheteher this instance is a foreign key.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "isForeignKey")]
        public bool IsForeignKey { get; set; }

        /// <summary>
        /// Indicates wheteher the name of this instance can be defined by a script.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "isNameAsScript")]
        public bool IsNameAsScript { get; set; }

        /// <summary>
        /// Type of value of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "valueType")]
        public DataValueType ValueType { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TestCarrier class.
        /// </summary>
        public TestCarrier() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TestCarrier class.
        /// </summary>
        /// <param name="path">The path of the instance.</param>
        public TestCarrier(string path) : base()
        {
            this.Path = path;
        }

        /// <summary>
        /// Instantiates a new instance of the TestCarrier class.
        /// </summary>
        /// <param name="fileName">The file name of the instance.</param>
        /// <param name="folderPath">The folder path of the instance.</param>
        public TestCarrier(string fileName, string folderPath) : base()
        {
            this.SetPath(fileName, folderPath);
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Get the name of this instance that is the alias if there is or the name otherwise.
        /// </summary>
        public string GetName()
        {
            string alias = Alias;
            if (!string.IsNullOrEmpty(alias))
                return alias;
            else
                return Name ?? "";
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the expression value of this instance.
        /// </summary>
        /// <param name="expression">Data expression value of the instance.</param>
        public void SetValue(DataExpression expression)
        {
            Value = expression;
        }

        /// <summary>
        /// Sets the literal value of this instance.
        /// </summary>
        /// <param name="text">The literal value.</param>
        public void SetLiteralValue(string text)
        {
            Value = text.CreateLiteral();
        }

        /// <summary>
        /// Sets the script value of this instance.
        /// </summary>
        /// <param name="text">The script value.</param>
        public void SetScriptValue(string text)
        {
            Value = text.CreateScript();
        }

        /// <summary>
        /// Sets the specified data module.
        /// </summary>
        /// <param name="dataModule">The data module to consider.</param>
        /// <returns>Returns this instance.</returns>
        public TestCarrier WithDataModule(string dataModule)
        {
            DataModule = dataModule;
            return this;
        }

        /// <summary>
        /// Sets the specified data table.
        /// </summary>
        /// <param name="dataTable">The data table to consider.</param>
        /// <returns>Returns this instance.</returns>
        public TestCarrier WithDataTable(string dataTable)
        {
            DataTable = dataTable;
            return this;
        }

        /// <summary>
        /// Sets the specified schema.
        /// </summary>
        /// <param name="schema">The schema to consider.</param>
        /// <returns>Returns this instance.</returns>
        public TestCarrier WithSchema(string schema)
        {
            Schema = schema;
            return this;
        }

        /// <summary>
        /// Sets the specified alias.
        /// </summary>
        /// <param name="alias">The alias to consider.</param>
        /// <returns>Returns this instance.</returns>
        public TestCarrier WithAlias(string alias)
        {
            Alias = alias;
            return this;
        }

        /// <summary>
        /// Sets the specified size.
        /// </summary>
        /// <param name="size">The size to consider.</param>
        /// <returns>Returns this instance.</returns>
        public TestCarrier WithSize(int size)
        {
            Size = size;
            return this;
        }

        /// <summary>
        /// Indicates that this instance represents all fields.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public TestCarrier AsAll()
        {
            IsAll = true;
            return this;
        }

        /// <summary>
        /// Indicates that this instance represents a key.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public TestCarrier AsKey()
        {
            IsKey = true;
            return this;
        }

        /// <summary>
        /// Indicates that the name of this instance is as script.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public TestCarrier WithNameAsScript()
        {
            IsNameAsScript = true;
            return this;
        }

        /// <summary>
        /// Specifies the value type of this instance.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <returns>Returns this instance.</returns>
        public TestCarrier WithValueType(DataValueType valueType)
        {
            ValueType = valueType;
            return this;
        }

        #endregion
    }
}
