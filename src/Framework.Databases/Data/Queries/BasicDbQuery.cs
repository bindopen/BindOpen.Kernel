using BindOpen.Framework.Databases.Extensions.Carriers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents an simple database data query.
    /// </summary>
    public class BasicDbQuery : DbQuery, IBasicDbQuery
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Indicates whether this instance is distinct. When distinct an advanced Select 
        /// database data query only returns distinct records.
        /// </summary>
        public bool IsDistinct { get; set; }

        /// <summary>
        /// Number of top items of this instance. Top items are the items a advanced Select 
        /// database data query will return.
        /// </summary>
        /// <remarks>By default it is -1 meaning no limit.</remarks>
        public int Top { get; set; } = -1;

        /// <summary>
        /// ID fields of this instance.
        /// </summary>
        public List<DbField> IdFields { get; set; } = new List<DbField>();

        /// <summary>
        /// From clause of this instance.
        /// </summary>
        public List<DbQueryFromStatement> FromStatements { get; set; } = new List<DbQueryFromStatement>();

        /// <summary>
        /// Order by statements of this instance.
        /// </summary>
        public List<DbQueryOrderByStatement> OrderByStatements { get; set; } = new List<DbQueryOrderByStatement>();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BasicDbQuery class.
        /// </summary>
        public BasicDbQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BasicDbQuery class.
        /// </summary>
        /// <param name="kind">Kind of database data query.</param>
        /// <param name="dataModule">Name of the data module.</param>
        /// <param name="schema">Schema of the data module.</param>
        /// <param name="dataTable">Name of data table.</param>
        public BasicDbQuery(
            DbQueryKind kind,
            string dataModule = null,
            string schema = null,
            string dataTable = null)
        {
            Kind = kind;
            DataModule = dataModule;
            Schema = schema;
            DataTable = dataTable;
        }

        /// <summary>
        /// Instantiates a new instance of the BasicDbQuery class.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="kind">Kind of database data query.</param>
        /// <param name="dataModule">Name of the data module.</param>
        /// <param name="schema">Schema of the data module.</param>
        /// <param name="dataTable">Name of data table.</param>
        public BasicDbQuery(
            string name,
            DbQueryKind kind,
            string dataModule = null,
            string schema = null,
            string dataTable = null) : this(kind, dataModule, schema, dataTable)
        {
            Name = name;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the data field with the specified bound data field name.
        /// </summary>
        /// <param name="boundFieldName">Name of the bound data field.</param>
        /// <returns>The data field with the specified bound data field name.</returns>
        public DbField GetIdFieldWithBoundFieldName(string boundFieldName)
        {
            if ((boundFieldName != null) & (IdFields != null))
            {
                foreach (DbField rField in IdFields)
                {
                    if (rField.Alias?.Equals(boundFieldName, StringComparison.OrdinalIgnoreCase) == true)
                        return rField;
                    if (rField.Name?.Equals(boundFieldName, StringComparison.OrdinalIgnoreCase) == true)
                        return rField;
                }
            }

            return null;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IBasicDbQuery WithFields(params DbField[] fields)
        {
            Fields = fields?.ToList();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBasicDbQuery From(params IDbQueryFromStatement[] statements)
        {
            FromStatements = statements?.Cast<DbQueryFromStatement>().ToList();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBasicDbQuery WithIdFields(params DbField[] fields)
        {
            IdFields = fields?.ToList();
            return this;
        }

        /// <summary>
        /// Indicates that this instance is distinct.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public IBasicDbQuery AsDistinct()
        {
            IsDistinct = true;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBasicDbQuery OrderBy(params IDbQueryOrderByStatement[] statements)
        {
            OrderByStatements = statements?.Cast<DbQueryOrderByStatement>().ToList();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBasicDbQuery WithTop(int top)
        {
            Top = top;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBasicDbQuery WithTableAlias(string tableAlias)
        {
            DataTableAlias = tableAlias;
            return this;
        }

        #endregion
    }
}