﻿using System;
using System.ComponentModel;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Carriers;
using BindOpen.Framework.Core.Extensions.Items.Carriers;
using BindOpen.Framework.Databases.Data.Queries;

namespace BindOpen.Framework.Databases.Extensions.Carriers
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    [Serializable()]
    [XmlType("DbField", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "dbField", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    [Carrier(
        Name = "databases$dbField",
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [XmlIgnore()]
        [DetailProperty(Name = "value")]
        public DataExpression Value { get; set; }

        /// <summary>
        /// Value of this instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [XmlIgnore()]
        [DetailProperty(Name = "query")]
        public DbDataQuery Query { get; set; }

        /// <summary>
        /// Indicates wheteher this instance is a key.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "isKey")]
        public Boolean IsKey { get; set; }

        /// <summary>
        /// Indicates wheteher this instance is a foreign key.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "isForeignKey")]
        public Boolean IsForeignKey { get; set; }

        /// <summary>
        /// Indicates wheteher the name of this instance can be defined by a script.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "isNameAsScript")]
        public Boolean IsNameAsScript { get; set; }

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
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        public DbField() : base()
        {
        }

        // Constructors with data expression -----

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        public DbField(
            string name,
            DataExpression value,
            DataValueType valueType = DataValueType.None)
            : base(name, "field_")
        {
            ValueType = DataValueType.None;
            Value = value;
        }

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        public DbField(
            string name,
            string tableName,
            DataExpression value,
            DataValueType valueType = DataValueType.None)
            : this(name, value, valueType)
        {
            DataTable = tableName;
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
        public DbField(
            string name,
            string tableName,
            string schema,
            string dataModule,
            DataExpression value,
            DataValueType valueType = DataValueType.None)
            : this(name, tableName, value, valueType)
        {
            Schema = schema;
            DataModule = dataModule;
        }

        // Constructors with object -----

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        public DbField(
            string name,
            DataValueType valueType = DataValueType.Any,
            object value = null)
            : base(name, "field_")
        {
            ValueType = valueType;
            if (value != null)
                Value = value.ToString(valueType).CreateLiteral();
        }

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="value">The value to consider.</param>
        public DbField(
            string name,
            string tableName,
            DataValueType valueType = DataValueType.Any,
            object value = null)
            : this(name, valueType, value)
        {
            DataTable = tableName;
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
        public DbField(
            string name,
            string tableName,
            string schema,
            string dataModule,
            DataValueType valueType = DataValueType.Any,
            object value = null)
            : this(name, tableName, valueType, value)
        {
            Schema = schema;
            DataModule = dataModule;
        }

        // Constructors with data expression -----

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="query">The query to consider.</param>
        public DbField(
            string name,
            DbDataQuery query)
            : base(name, "field_")
        {
            ValueType = DataValueType.None;
            Query = query;
        }

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="query">The query to consider.</param>
        public DbField(
            string name,
            string tableName,
            DbDataQuery query)
            : this(name, query)
        {
            DataTable = tableName;
        }

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="tableName">The data table to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModule">The data module to consider.</param>
        /// <param name="query">The query to consider.</param>
        public DbField(
            string name,
            string tableName,
            string schema,
            string dataModule,
            DbDataQuery query)
            : this(name, tableName, query)
        {
            Schema = schema;
            DataModule = dataModule;
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

        #endregion
    }
}
