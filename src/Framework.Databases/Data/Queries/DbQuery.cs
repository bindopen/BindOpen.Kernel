using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Helpers.Strings;
using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Carriers;
using System;
using System.Collections.Generic;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents a database data query.
    /// </summary>
    public abstract class DbQuery : DescribedDataItem, IDbQuery
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<DbField> _fields = new List<DbField>();

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Name of this instance.
        /// </summary>
        public new string Name { get; set; } = "dataquery_" + DateTime.Now.ToString(StringHelper.__DateFormat);

        /// <summary>
        /// Name of the data module of this instance.
        /// </summary>
        public string DataModule { get; set; }

        /// <summary>
        /// Name of the data table of this instance.
        /// </summary>
        public string DataTable { get; set; }

        /// <summary>
        /// Name of the data table alias of this instance.
        /// </summary>
        public string DataTableAlias { get; set; }

        /// <summary>
        /// Schema of this instance.
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        public DbQueryKind Kind { get; set; } = DbQueryKind.Select;

        /// <summary>
        /// Indicates whether existence is checked.
        /// </summary>
        public bool IsExistenceChecked { get; set; } = false;

        /// <summary>
        /// Fields of this instance.
        /// </summary>
        public List<DbField> Fields
        {
            get { return this._fields; }
            set { this._fields = new List<DbField>(value); }
        }

        /// <summary>
        /// The parameter specification set of this instance.
        /// </summary>
        public DataElementSpecSet ParameterSpecSet
        {
            get;
            set;
        }

        /// <summary>
        /// The parameters of this instance.
        /// </summary>
        public DataElementSet ParameterSet
        {
            get;
            set;
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQuery class.
        /// </summary>
        protected DbQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DbQuery class.
        /// </summary>
        /// <param name="kind">Kind of database data query.</param>
        /// <param name="dataModule">Name of the data module.</param>
        /// <param name="schema">Schema of the data module.</param>
        /// <param name="dataTable">Name of data table.</param>
        /// <param name="isExistenceChecked">Indicates whether existence is checked.</param>
        protected DbQuery(
            DbQueryKind kind,
            string dataModule = null,
            string schema = null,
            string dataTable = null,
            bool isExistenceChecked = false)
        {
            Kind = kind;
            DataModule = dataModule;
            Schema = schema;
            DataTable = dataTable;
            IsExistenceChecked = isExistenceChecked;
        }

        /// <summary>
        /// Instantiates a new instance of the DbQuery class.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="kind">Kind of database data query.</param>
        /// <param name="dataModule">Name of the data module.</param>
        /// <param name="schema">Schema of the data module.</param>
        /// <param name="dataTable">Name of data table.</param>
        /// <param name="isExistenceChecked">Indicates whether existence is checked.</param>
        protected DbQuery(
            string name,
            DbQueryKind kind,
            string dataModule = null,
            string schema = null,
            string dataTable = null,
            bool isExistenceChecked = false) : this(kind, dataModule, schema, dataTable, isExistenceChecked)
        {
            Name = name;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the name of this instance.
        /// </summary>
        /// <returns>Returns the name of this instance.</returns>
        /// <remarks>If the name of this instance is empty or null then the returned name is determined from this instance's properties.</remarks>
        public virtual string GetName()
        {
            return Name;
        }

        /// <summary>
        /// Gets the data field with the specified bound data field name.
        /// </summary>
        /// <param name="boundFieldName">Name of the bound data field.</param>
        /// <returns>The data field with the specified bound data field name.</returns>
        public DbField GetFieldWithBoundFieldName(string boundFieldName)
        {
            if ((boundFieldName != null) && (this._fields != null))
            {
                foreach (DbField field in this._fields)
                {
                    if (field.GetName().Equals(boundFieldName, StringComparison.OrdinalIgnoreCase))
                        return field;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the data field with the specified data field name.
        /// </summary>
        /// <param name="name">Name of the field.</param>
        /// <returns>The data field with the specified data field name.</returns>
        public DbField GetDataFieldWithName(string name)
        {
            foreach (DbField field in this._fields)
            {
                if (field.Alias.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return field;
                if (field.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return field;
            }
            return null;
        }

        /// <summary>
        /// Gets the key data fields of this instance.
        /// </summary>
        /// <returns>The key data fields of this instance.</returns>
        public List<DbField> GetKeyDataFields()
        {
            List<DbField> fields = new List<DbField>();
            foreach (DbField field in this._fields)
            {
                if ((field.IsKey) || (field.IsForeignKey))
                    fields.Add(field);
            }
            return fields;
        }

        /// <summary>
        /// Gets the primary data fields of this instance.
        /// </summary>
        /// <returns>The primary data fields of this instance.</returns>
        public List<DbField> GetPrimaryKeyDataFields()
        {
            List<DbField> fields = new List<DbField>();
            foreach (DbField field in this._fields)
            {
                if (field.IsKey)
                    fields.Add(field);
            }
            return fields;
        }

        /// <summary>
        /// Gets the foreign data fields of this instance.
        /// </summary>
        /// <returns>The foreign data fields of this instance.</returns>
        public List<DbField> GetForeignKeyDataFields()
        {
            List<DbField> fields = new List<DbField>();
            foreach (DbField field in this._fields)
            {
                if (field.IsForeignKey)
                    fields.Add(field);
            }
            return fields;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Defines the parameter specifications of this instance.
        /// </summary>
        /// <param name="parameters">The set of parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public IDbQuery WithParameters(params IDataElement[] parameters)
        {
            ParameterSet = new DataElementSet(parameters);

            return this;
        }

        /// <summary>
        /// Defines the parameter specifications of this instance.
        /// </summary>
        /// <param name="parameterSpecs">The set of parameter specifications to consider.</param>
        /// <returns>Return this instance.</returns>
        public IDbQuery UsingParameters(params IDataElementSpec[] parameterSpecs)
        {
            ParameterSpecSet = new DataElementSpecSet(parameterSpecs);

            return this;
        }

        /// <summary>
        /// Add the specified parameter to this instance.
        /// </summary>
        /// <param name="parameter">The parameter to consider.</param>
        /// <returns>Return this instance.</returns>
        public IDbQuery AddParameter(IDataElement parameter)
        {
            ParameterSet?.Add(parameter as DataElement);

            return this;
        }

        /// <summary>
        /// Adds the specified parameter to this instance.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The data table to consider.</param>
        /// <returns>Return this added parameter.</returns>
        public IDataElement UseParameter(
            string name,
            object value = null)
        {
            return UseParameter(name, DataValueType.Any, value);
        }

        /// <summary>
        /// Adds the specified parameter to this instance.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The data value type to consider.</param>
        /// <param name="value">The data table to consider.</param>
        /// <returns>Return this added parameter.</returns>
        public IDataElement UseParameter(
            string name,
            DataValueType valueType,
            object value)
        {
            if (ParameterSet == null)
            {
                ParameterSet = new DataElementSet();
            }

            DataElement parameter;
            if ((parameter = ParameterSet[name]) != null)
            {
                parameter.SetItem(value);
            }
            else
            {
                parameter = ElementFactory.CreateScalar(name, valueType, value);
                parameter.Index = ParameterSet.Count + 1;
                ParameterSet.Add(parameter);
            }

            return parameter;
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
            }
        }

        #endregion
    }
}