using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Items;
using BindOpen.Extensions.Carriers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a database data query.
    /// </summary>
    public abstract class DbQuery : DescribedDataItem, IDbQuery
    {
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

        /// <summary>
        /// The CTE tables of this instance.
        /// </summary>
        public List<DbTable> CTETables { get; set; }

        /// <summary>
        /// Value of this instance.
        /// </summary>
        public DataExpression Expression { get; set; }

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
        /// <param name="kind">Type of database data query.</param>
        /// <param name="table">The table to consider.</param>
        protected DbQuery(
            DbQueryKind kind,
            DbTable table = null)
        {
            Kind = kind;
            DataModule = table?.DataModule;
            Schema = table?.Schema;
            DataTable = table?.Name;
        }

        /// <summary>
        /// Instantiates a new instance of the DbQuery class.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="kind">Type of database data query.</param>
        /// <param name="table">The table to consider.</param>
        protected DbQuery(
            string name,
            DbQueryKind kind,
            DbTable table = null) : this(kind, table)
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

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Indicates that this instance checks the existence of table or data according to the kind of queries.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether this instance checks the existence of table or data.</param>
        /// <returns>Return this instance.</returns>
        public IDbQuery CheckExistence(bool isExistenceChecked = true)
        {
            IsExistenceChecked = isExistenceChecked;

            return this;
        }

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
        public IDbQuery AddParameter(ScalarElement parameter)
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
        public ScalarElement UseParameter(
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
        public ScalarElement UseParameter(
            string name,
            DataValueType valueType,
            object value = null)
        {
            if (ParameterSet == null)
            {
                ParameterSet = new DataElementSet();
            }

            ScalarElement parameter;
            if ((parameter = ParameterSet[name] as ScalarElement) != null)
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

        /// <summary>
        /// Sets the specified CTE tables.
        /// </summary>
        /// <param name="tables">The CTE tables to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbQuery WithCTE(params DbTable[] tables)
        {
            CTETables = tables?.ToList();
            return this;
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