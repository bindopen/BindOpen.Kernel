using System;
using System.ComponentModel;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Configuration.Carriers;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.Extensions.Runtime.Carriers;
using BindOpen.Framework.Databases.Data.Queries;

namespace BindOpen.Framework.Databases.Extensions.Carriers
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    [Serializable()]
    [XmlType("DbField", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "dbField", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    [Carrier(
        Name = "dbField",
        DataSourceKind = DataSourceKind.Database,
        Description = "Database field.",
        CreationDate = "2016-09-14"
    )]
    public class DbField : Carrier
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Indicates wheteher this instance represents all the fields.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="isAll")]
        public Boolean IsAll
        {
            get{ return this.Get<Boolean>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="dataModule")]
        public string DataModule
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "schema")]
        public string Schema
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Data table of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="dataTable")]
        public string DataTable
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Alias of the data table of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="dataTableAlias")]
        public string DataTableAlias
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Alias of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="alias")]
        public string Alias
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Size of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="size")]
        public int Size
        {
            get { return this.Get<int>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Value of this instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [XmlIgnore()]
        [DetailProperty(Name="value")]
        public DataExpression Value
        {
            get { return this.Get<DataExpression>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Value of this instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [XmlIgnore()]
        [DetailProperty(Name = "query")]
        public DbDataQuery Query
        {
            get { return this.Get<DbDataQuery>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Indicates wheteher this instance is a key.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="isKey")]
        public Boolean IsKey
        {
            get { return this.Get<Boolean>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Indicates wheteher this instance is a foreign key.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="isForeignKey")]
        public Boolean IsForeignKey
        {
            get { return this.Get<Boolean>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Indicates wheteher the name of this instance can be defined by a script.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="isNameAsScript")]
        public Boolean IsNameAsScript
        {
            get { return this.Get<Boolean>(); }
            set { this.Set(value); }
        }

        ///// <summary>
        ///// Indicates whether the value of this instance represents the current visitor's language.
        ///// </summary>

        //public Boolean IsGlobal
        //{
        //    get { return this._IsGlobal; }
        //    set { this._IsGlobal = value; }
        //}

        /// <summary>
        /// Type of value of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="valueType")]
        public DataValueType ValueType
        {
            get { return this.Get<DataValueType>(DataValueType.None); }
            set { this.Set(value); }
        }

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

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="relativePath">The relative path to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public DbField(
            string name,
            CarrierConfiguration configuration,
            string relativePath = null,
            AppScope appScope = null)
            : base(name, "databases$dbField", configuration, "field_", relativePath, appScope)
        {
        }

        // Constructors with data expression -----

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public DbField(
            string name,
            DataExpression value,
            DataValueType valueType = DataValueType.None,
            AppScope appScope = null)
            : base(name, "databases$dbField", null, "field_", null, appScope)
        {
            this.ValueType = DataValueType.None;
            this.Value = value;
        }

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public DbField(
            string name,
            string tableName,
            DataExpression value,
            DataValueType valueType = DataValueType.None,
            AppScope appScope = null)
            : this(name, value, valueType, appScope)
        {
            this.DataTable = tableName;
        }

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public DbField(
            string name,
            string tableName,
            string schema,
            string dataModule,
            DataExpression value,
            DataValueType valueType = DataValueType.None,
            AppScope appScope = null)
            : this(name, tableName, value, valueType, appScope)
        {
            this.Schema = schema;
            this.DataModule = dataModule;
        }

        // Constructors with object -----

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public DbField(
            string name,
            DataValueType valueType = DataValueType.Any,
            object value = null,
            AppScope appScope = null)
            : base(name, "databases$dbField", null, "field_", null, appScope)
        {
            this.ValueType = valueType;
            if (value != null)
                this.Value = value.GetString(valueType).CreateLiteral()
        }

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public DbField(
            string name,
            string tableName,
            DataValueType valueType = DataValueType.Any,
            object value = null,
            AppScope appScope = null)
            : this(name, valueType, value, appScope)
        {
            this.DataTable = tableName;
        }

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public DbField(
            string name,
            string tableName,
            string schema,
            string dataModule,
            DataValueType valueType = DataValueType.Any,
            object value = null,
            AppScope appScope = null)
            : this(name, tableName, valueType, value, appScope)
        {
            this.Schema = schema;
            this.DataModule = dataModule;
        }

        // Constructors with data expression -----

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="query">The query to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public DbField(
            string name,
            DbDataQuery query,
            AppScope appScope = null)
            : base(name, "databases$dbField", null, "field_", null, appScope)
        {
            this.ValueType = DataValueType.None;
            this.Query = query;
        }

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="query">The query to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public DbField(
            string name,
            string tableName,
            DbDataQuery query,
            AppScope appScope = null)
            : this(name, query, appScope)
        {
            this.DataTable = tableName;
        }

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="query">The query to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public DbField(
            string name,
            string tableName,
            string schema,
            string dataModule,
            DbDataQuery query,
            AppScope appScope = null)
            : this(name, tableName, query, appScope)
        {
            this.Schema = schema;
            this.DataModule = dataModule;
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
            string alias = this.Alias;
            if (!string.IsNullOrEmpty(alias))
                return alias;
            else
                return this.Name ?? "";
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
            this.Value = expression;
        }

        /// <summary>
        /// Sets the literal value of this instance.
        /// </summary>
        /// <param name="text">The literal value.</param>
        public void SetLiteralValue(string text)
        {
            this.Value = new DataExpression(text, DataExpressionKind.Literal);
        }

        /// <summary>
        /// Sets the script value of this instance.
        /// </summary>
        /// <param name="text">The script value.</param>
        public void SetScriptValue(string text)
        {
            this.Value = new DataExpression(text, DataExpressionKind.Script);
        }

        #endregion
    }
}
