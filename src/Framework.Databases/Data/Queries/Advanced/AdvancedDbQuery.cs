﻿using BindOpen.Framework.Data.Expression;
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
        /// <param name="dataModule">Name of the data module.</param>
        /// <param name="schema">Schema of the data module.</param>
        /// <param name="dataTable">Name of data table.</param>
        /// <param name="isExistenceChecked">Indicates whether existence is checked.</param>
        public AdvancedDbQuery(
            DbQueryKind kind,
            string dataModule = null,
            string schema = null,
            string dataTable = null,
            bool isExistenceChecked = false) : base(kind, dataModule, schema, dataTable, isExistenceChecked)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AdvancedDbQuery class.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="kind">Type of database data query.</param>
        /// <param name="dataModule">Name of the data module.</param>
        /// <param name="schema">Schema of the data module.</param>
        /// <param name="dataTable">Name of data table.</param>
        public AdvancedDbQuery(
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
        public IAdvancedDbQuery From(params IDbQueryFromStatement[] statements)
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
        public IAdvancedDbQuery WithTableAlias(string tableAlias)
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
        /// Sets the fields using an initialization function.
        /// </summary>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IAdvancedDbQuery WithFields(Func<IAdvancedDbQuery, DbField[]> initiliazer)
        {
            return WithFields(initiliazer?.Invoke(this));
        }

        public IAdvancedDbQuery AddField(DbField field)
        {
            Fields?.Add(field);

            return this;
        }

        public IAdvancedDbQuery AddField(Func<IAdvancedDbQuery, DbField> initiliazer)
        {
            return AddField(initiliazer?.Invoke(this));
        }

        #endregion
    }
}