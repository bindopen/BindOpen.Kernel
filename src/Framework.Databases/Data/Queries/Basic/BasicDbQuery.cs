using BindOpen.Framework.Data.Helpers.Strings;
using BindOpen.Framework.Extensions.Carriers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Framework.Data.Queries
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
        /// <param name="isExistenceChecked">Indicates whether existence is checked.</param>
        public BasicDbQuery(
            DbQueryKind kind,
            string dataModule = null,
            string schema = null,
            string dataTable = null,
            bool isExistenceChecked = false) : base(kind, dataModule, schema, dataTable, isExistenceChecked)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BasicDbQuery class.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="kind">Kind of database data query.</param>
        /// <param name="dataModule">Name of the data module.</param>
        /// <param name="schema">Schema of the data module.</param>
        /// <param name="dataTable">Name of data table.</param>
        /// <param name="isExistenceChecked">Indicates whether existence is checked.</param>
        public BasicDbQuery(
            string name,
            DbQueryKind kind,
            string dataModule = null,
            string schema = null,
            string dataTable = null,
            bool isExistenceChecked = false) : base(name, kind, dataModule, schema, dataTable, isExistenceChecked)
        {
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
        public override string GetName()
        {
            var st = base.GetName();


            if (string.IsNullOrEmpty(st))
            {
                st += Schema.ConcatenateIfNotEmpty("_");

                if (!string.IsNullOrEmpty(DataTableAlias) || !string.IsNullOrEmpty(DataTable))
                {
                    st += (DataTableAlias ?? DataTable) + "_";
                }
                else if (FromStatements != null && FromStatements.Count > 0
                    && FromStatements[0].JoinStatements != null && FromStatements[0].JoinStatements.Count > 0)
                {
                    var table = FromStatements[0].JoinStatements[0].Table;
                    if (!string.IsNullOrEmpty(table?.Alias) || !string.IsNullOrEmpty(table?.Name))
                    {
                        st += (table.Alias ?? table.Name) + "_";
                    }
                }

                st += Kind;
            }

            return st;
        }

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
        /// 
        /// </summary>
        public IBasicDbQuery From(params IDbQueryFromStatement[] statements)
        {
            FromStatements = statements?.Cast<DbQueryFromStatement>().ToList();
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
        /// Sets the fields using an initialization function.
        /// </summary>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IBasicDbQuery WithFields(Func<IBasicDbQuery, DbField[]> initiliazer)
        {
            return WithFields(initiliazer?.Invoke(this));
        }

        /// <summary>
        /// Adds the specified fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <returns>Returns this instance.</returns>
        public IBasicDbQuery AddField(DbField field, bool canBeAdded = true)
        {
            if (canBeAdded)
            {
                Fields?.Add(field);
            }

            return this;
        }

        /// <summary>
        /// Adds the fields using an initialization function.
        /// </summary>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <returns>Returns this instance.</returns>
        public IBasicDbQuery AddField(Func<IBasicDbQuery, DbField> initiliazer, bool canBeAdded = true)
        {
            if (canBeAdded)
            {
                return AddField(initiliazer?.Invoke(this));
            }

            return this;
        }

        /// <summary>
        /// Sets the specified ID fields.
        /// </summary>
        /// <param name="fields">The ID fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IBasicDbQuery WithIdFields(params DbField[] fields)
        {
            IdFields = fields?.ToList();
            return this;
        }

        /// <summary>
        /// Sets the ID fields using an initialization function.
        /// </summary>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IBasicDbQuery WithIdFields(Func<IBasicDbQuery, DbField[]> initiliazer)
        {
            return WithIdFields(initiliazer?.Invoke(this));
        }

        /// <summary>
        /// Adds the specified ID field.
        /// </summary>
        /// <param name="field">The ID field to consider.</param>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <returns>Returns this instance.</returns>
        public IBasicDbQuery AddIdField(DbField field, bool canBeAdded = true)
        {
            if (canBeAdded)
            {
                IdFields?.Add(field);
            }

            return this;
        }

        /// <summary>
        /// Adds the ID field using an initialization function.
        /// </summary>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <returns>Returns this instance.</returns>
        public IBasicDbQuery AddIdField(Func<IBasicDbQuery, DbField> initiliazer, bool canBeAdded = true)
        {
            if (canBeAdded)
            {
                return AddIdField(initiliazer?.Invoke(this));
            }

            return this;
        }

        #endregion
    }
}