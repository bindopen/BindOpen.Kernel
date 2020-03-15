using BindOpen.Data.Common;
using BindOpen.Data.Expression;
using BindOpen.Data.Items;
using BindOpen.Data.Queries;
using BindOpen.Extensions.Runtime;
using System;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Carriers
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    [XmlType("DbField", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "dbField", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    [BdoCarrier(
        Name = "databases$dbField",
        DatasourceKind = DatasourceKind.Database,
        Description = "Database field.",
        CreationDate = "2016-09-14"
    )]
    public class DbField : BdoCarrier, IDbQueryItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Indicates wheteher this instance represents all the fields.
        /// </summary>
        [DetailProperty(Name = "isAll")]
        public Boolean IsAll { get; set; }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [DetailProperty(Name = "dataModule")]
        public string DataModule { get; set; }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [DetailProperty(Name = "schema")]
        public string Schema { get; set; }

        /// <summary>
        /// Data table of this instance.
        /// </summary>
        [DetailProperty(Name = "dataTable")]
        public string DataTable { get; set; }

        /// <summary>
        /// Alias of the data table of this instance.
        /// </summary>
        [DetailProperty(Name = "dataTableAlias")]
        public string DataTableAlias { get; set; }

        /// <summary>
        /// Alias of this instance.
        /// </summary>
        [DetailProperty(Name = "alias")]
        public string Alias { get; set; }

        /// <summary>
        /// Size of this instance.
        /// </summary>
        [DetailProperty(Name = "size")]
        public int Size { get; set; }

        /// <summary>
        /// Value of this instance.
        /// </summary>
        [DetailProperty(Name = "value")]
        public DataExpression Expression { get; set; }

        /// <summary>
        /// Value of this instance.
        /// </summary>
        [DetailProperty(Name = "query")]
        public DbQuery Query { get; set; }

        /// <summary>
        /// Indicates wheteher this instance is a key.
        /// </summary>
        [DetailProperty(Name = "isKey")]
        public bool IsKey { get; set; }

        /// <summary>
        /// Indicates wheteher this instance is a foreign key.
        /// </summary>
        [DetailProperty(Name = "isForeignKey")]
        public bool IsForeignKey { get; set; }

        /// <summary>
        /// Indicates wheteher the name of this instance can be defined by a script.
        /// </summary>
        [DetailProperty(Name = "isNameAsScript")]
        public bool IsNameAsScript { get; set; }

        /// <summary>
        /// Type of value of this instance.
        /// </summary>
        [DetailProperty(Name = "valueType")]
        public DataValueType ValueType { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        public DbField() : base()
        {
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this;
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
            Expression = expression;
        }

        /// <summary>
        /// Sets the literal value of this instance.
        /// </summary>
        /// <param name="text">The literal value.</param>
        public void SetLiteralValue(string text)
        {
            Expression = text.CreateLiteral();
        }

        /// <summary>
        /// Sets the script value of this instance.
        /// </summary>
        /// <param name="text">The script value.</param>
        public void SetScriptValue(string text)
        {
            Expression = text.CreateScript();
        }

        /// <summary>
        /// Sets the specified data module.
        /// </summary>
        /// <param name="dataModule">The data module to consider.</param>
        /// <returns>Returns this instance.</returns>
        public DbField WithDataModule(string dataModule)
        {
            DataModule = dataModule;
            return this;
        }

        /// <summary>
        /// Sets the specified data table.
        /// </summary>
        /// <param name="dataTable">The data table to consider.</param>
        /// <returns>Returns this instance.</returns>
        public DbField WithDataTable(string dataTable)
        {
            DataTable = dataTable;
            return this;
        }

        /// <summary>
        /// Sets the specified schema.
        /// </summary>
        /// <param name="schema">The schema to consider.</param>
        /// <returns>Returns this instance.</returns>
        public DbField WithSchema(string schema)
        {
            Schema = schema;
            return this;
        }

        /// <summary>
        /// Sets the specified alias.
        /// </summary>
        /// <param name="alias">The alias to consider.</param>
        /// <returns>Returns this instance.</returns>
        public DbField WithAlias(string alias)
        {
            Alias = alias;
            return this;
        }

        /// <summary>
        /// Sets the specified size.
        /// </summary>
        /// <param name="size">The size to consider.</param>
        /// <returns>Returns this instance.</returns>
        public DbField WithSize(int size)
        {
            Size = size;
            return this;
        }

        /// <summary>
        /// Indicates that this instance represents all fields.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public DbField AsAll()
        {
            IsAll = true;
            return this;
        }

        /// <summary>
        /// Indicates that this instance represents a key.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public DbField AsKey()
        {
            IsKey = true;
            return this;
        }

        /// <summary>
        /// Indicates that the name of this instance is as script.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public DbField WithNameAsScript()
        {
            IsNameAsScript = true;
            return this;
        }

        /// <summary>
        /// Specifies the value type of this instance.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <returns>Returns this instance.</returns>
        public DbField WithValueType(DataValueType valueType)
        {
            ValueType = valueType;
            return this;
        }

        #endregion

        // ------------------------------------------
        // OPERATORS
        // ------------------------------------------

        #region Operators

        /// <summary>
        /// Returns the data expression string corresponding to this instance.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        public static implicit operator string(DbField field)
            => DbQueryBuilder.GetBdoScript(field);

        #endregion
    }
}
