using BindOpen.Framework.Data.Expression;
using BindOpen.Framework.Data.Helpers.Strings;
using BindOpen.Framework.Extensions.Carriers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents an advanced database data query.
    /// </summary>
    public class AdvancedDbQuery : DbQuery, IAdvancedDbQuery
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
        /// From clause of this instance.
        /// </summary>
        public List<DbQueryFromStatement> FromStatements { get; set; } = new List<DbQueryFromStatement>();

        /// <summary>
        /// Where clause of this instance.
        /// </summary>
        public DataExpression WhereClause { get; set; }

        /// <summary>
        /// Group by statement of this instance.
        /// </summary>
        public DbQueryGroupByStatement GroupByClause { get; set; }

        /// <summary>
        /// Having statement of this instance.
        /// </summary>
        public DbQueryHavingStatement HavingClause { get; set; }

        /// <summary>
        /// Order statements of this instance.
        /// </summary>
        public List<DbQueryOrderByStatement> OrderByStatements { get; set; } = new List<DbQueryOrderByStatement>();

        /// <summary>
        /// The returned IDs to consider.
        /// </summary>
        public List<DbField> ReturnedIdFields { get; set; } = new List<DbField>();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AdvancedDbQuery class.
        /// </summary>
        public AdvancedDbQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AdvancedDbQuery class.
        /// </summary>
        /// <param name="kind">Type of database data query.</param>
        /// <param name="table">The table to consider.</param>
        public AdvancedDbQuery(
            DbQueryKind kind,
            DbTable table = null) : base(kind, table)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AdvancedDbQuery class.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="kind">Type of database data query.</param>
        /// <param name="table">The table to consider.</param>
        public AdvancedDbQuery(
            string name,
            DbQueryKind kind,
            DbTable table = null) : base(name, kind, table)
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
                st += Schema.ConcatenateIfFirstNotEmpty("_");

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

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statements"></param>
        /// <returns></returns>
        public IAdvancedDbQuery Froms(params IDbQueryFromStatement[] statements)
        {
            FromStatements = statements?.Cast<DbQueryFromStatement>().ToList();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clause"></param>
        /// <returns></returns>
        public IAdvancedDbQuery GroupBy(IDbQueryGroupByStatement clause)
        {
            GroupByClause = clause as DbQueryGroupByStatement;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clause"></param>
        /// <returns></returns>
        public IAdvancedDbQuery Having(IDbQueryHavingStatement clause)
        {
            HavingClause = clause as DbQueryHavingStatement;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IAdvancedDbQuery AsDistinct()
        {
            IsDistinct = true;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statements"></param>
        /// <returns></returns>
        public IAdvancedDbQuery OrderBy(params IDbQueryOrderByStatement[] statements)
        {
            OrderByStatements = statements?.Cast<DbQueryOrderByStatement>().ToList();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public IAdvancedDbQuery WithTop(int top)
        {
            Top = top;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clause"></param>
        /// <returns></returns>
        public IAdvancedDbQuery Where(IDataExpression clause)
        {
            WhereClause = clause as DataExpression;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IAdvancedDbQuery WithTableWithAlias(string tableAlias)
        {
            DataTableAlias = tableAlias;
            return this;
        }

        /// <summary>
        /// Sets the specified fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IAdvancedDbQuery WithFields(params DbField[] fields)
        {
            Fields = fields?.ToList();

            return this;
        }

        /// <summary>
        /// Sets the specified returned ID fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IAdvancedDbQuery WithReturnedIdFields(params DbField[] fields)
        {
            ReturnedIdFields = fields?.ToList();

            return this;
        }

        /// <summary>
        /// Sets the fields using an initialization function.
        /// </summary>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IAdvancedDbQuery WithFields(Func<IAdvancedDbQuery, DbField[]> initiliazer)
        {
            return WithFields(initiliazer?.Invoke(this));
        }

        /// <summary>
        /// Adds the specified field.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IAdvancedDbQuery AddField(DbField field)
        {
            Fields?.Add(field);

            return this;
        }

        /// <summary>
        /// Adds the specified field.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="field">The field to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IAdvancedDbQuery AddField(bool canBeAdded, DbField field)
        {
            if (canBeAdded)
            {
                return AddField(field);
            }

            return this;
        }

        /// <summary>
        /// Sets the field using an initialization function.
        /// </summary>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IAdvancedDbQuery AddField(Func<IAdvancedDbQuery, DbField> initiliazer)
        {
            return AddField(initiliazer?.Invoke(this));
        }

        /// <summary>
        /// Sets the field using an initialization function.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IAdvancedDbQuery AddField(bool canBeAdded, Func<IAdvancedDbQuery, DbField> initiliazer)
        {
            if (canBeAdded)
            {
                return AddField(initiliazer);
            }

            return this;
        }

        #endregion
    }
}