using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Extensions.Carriers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents an advanced database data query.
    /// </summary>
    public class DbSingleQuery : DbQuery, IDbSingleQuery
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
        public int Limit { get; set; } = -1;

        /// <summary>
        /// Fields of this instance.
        /// </summary>
        public List<DbField> Fields { get; set; } = new List<DbField>();

        /// <summary>
        /// The union tables of this instance.
        /// </summary>
        public List<DbQueryUnionClause> UnionClauses { get; set; }

        /// <summary>
        /// From clause of this instance.
        /// </summary>
        public DbQueryFromClause FromClause { get; set; }

        /// <summary>
        /// Where clause of this instance.
        /// </summary>
        public DbQueryWhereClause WhereClause { get; set; }

        /// <summary>
        /// Group by statement of this instance.
        /// </summary>
        public DbQueryGroupByClause GroupByClause { get; set; }

        /// <summary>
        /// Having statement of this instance.
        /// </summary>
        public DbQueryHavingClause HavingClause { get; set; }

        /// <summary>
        /// Order by clause of this instance.
        /// </summary>
        public DbQueryOrderByClause OrderByClause { get; set; }

        /// <summary>
        /// The returned IDs to consider.
        /// </summary>
        public List<DbField> ReturnedIdFields { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AdvancedDbQuery class.
        /// </summary>
        public DbSingleQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AdvancedDbQuery class.
        /// </summary>
        /// <param name="kind">Type of database data query.</param>
        /// <param name="table">The table to consider.</param>
        public DbSingleQuery(
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
        public DbSingleQuery(
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
                else if ((FromClause?.Statements.Count > 0) && (FromClause?.Statements[0]?.Tables?.Count > 0))
                {
                    var table = FromClause?.Statements[0].Tables[0];
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
        public DbField GetFieldWithBoundFieldName(string boundFieldName)
        {
            if ((boundFieldName != null) && (Fields != null))
            {
                foreach (DbField field in Fields)
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
        public DbField GetFieldWithName(string name)
        {
            if (Fields != null)
            {
                foreach (DbField field in Fields)
                {
                    if (field.Alias.Equals(name, StringComparison.OrdinalIgnoreCase))
                        return field;
                    if (field.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        return field;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the data field with the specified bound data field name.
        /// </summary>
        /// <param name="boundFieldName">Name of the bound data field.</param>
        /// <returns>The data field with the specified bound data field name.</returns>
        public DbField GetIdFieldWithBoundFieldName(string boundFieldName)
        {
            if ((boundFieldName != null) && (WhereClause?.IdFields != null))
            {
                foreach (DbField rField in WhereClause?.IdFields)
                {
                    if (rField.Alias?.Equals(boundFieldName, StringComparison.OrdinalIgnoreCase) == true)
                    {
                        return rField;
                    }
                    if (rField.Name?.Equals(boundFieldName, StringComparison.OrdinalIgnoreCase) == true)
                    {
                        return rField;
                    }
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
        /// <returns></returns>
        public IDbSingleQuery AsDistinct()
        {
            IsDistinct = true;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public IDbSingleQuery WithLimit(int limit)
        {
            Limit = limit;
            return this;
        }

        /// <summary>
        /// Sets the specified fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery WithFields(params DbField[] fields)
        {
            Fields = fields?.ToList();

            return this;
        }

        /// <summary>
        /// Sets the specified returned ID fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery WithReturnedIdFields(params DbField[] fields)
        {
            ReturnedIdFields = fields?.ToList();

            return this;
        }

        /// <summary>
        /// Sets the fields using an initialization function.
        /// </summary>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery WithFields(Func<IDbSingleQuery, DbField[]> initializer)
        {
            return WithFields(initializer?.Invoke(this));
        }

        /// <summary>
        /// Adds the specified field.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery AddField(DbField field)
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
        public IDbSingleQuery AddField(bool canBeAdded, DbField field)
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
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery AddField(Func<IDbSingleQuery, DbField> initializer)
        {
            return AddField(initializer?.Invoke(this));
        }

        /// <summary>
        /// Sets the field using an initialization function.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery AddField(bool canBeAdded, Func<IDbSingleQuery, DbField> initializer)
        {
            if (canBeAdded)
            {
                return AddField(initializer);
            }

            return this;
        }

        // Union -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unionClause">The union clause to consider.</param>
        public IDbSingleQuery Union(DbQueryUnionKind kind, IDbSingleQuery query)
        {
            if (UnionClauses == null)
            {
                UnionClauses = new List<DbQueryUnionClause>();
            }
            UnionClauses.Add(new DbQueryUnionClause() { Kind = kind, Query = query });

            return this;
        }

        // From -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tables">The tables to consider.</param>
        public IDbSingleQuery From(params DbTable[] tables)
        {
            if (FromClause == null)
            {
                FromClause = new DbQueryFromClause();
            }
            if (FromClause.Statements == null)
            {
                FromClause.Statements = new List<DbQueryFromStatement>();
            }
            FromClause.Statements.Add(new DbQueryFromStatement() { Tables = tables?.ToList() });

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery From(IDataExpression expression)
        {
            FromClause = new DbQueryFromClause() { Expression = expression as DataExpression };
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery From(Func<IDbSingleQuery, IDataExpression> initializer)
        {
            return From(initializer?.Invoke(this));
        }

        // Where -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery Where(IDataExpression expression)
        {
            WhereClause = new DbQueryWhereClause() { Expression = expression as DataExpression };
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery Where(Func<IDbSingleQuery, IDataExpression> initializer)
        {
            return Where(initializer?.Invoke(this));
        }

        // IdFields -------------------------------------

        /// <summary>
        /// Sets the specified ID fields.
        /// </summary>
        /// <param name="fields">The ID fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery WithIdFields(params DbField[] fields)
        {
            if (WhereClause == null)
            {
                WhereClause = new DbQueryWhereClause();
            }
            WhereClause.IdFields = fields?.ToList();
            return this;
        }

        /// <summary>
        /// Sets the ID fields using an initialization function.
        /// </summary>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery WithIdFields(Func<IDbSingleQuery, DbField[]> initializer)
        {
            return WithIdFields(initializer?.Invoke(this));
        }

        /// <summary>
        /// Adds the specified ID field.
        /// </summary>
        /// <param name="field">The ID field to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery AddIdField(DbField field)
        {
            if (WhereClause == null)
            {
                WhereClause = new DbQueryWhereClause();
            }
            if (WhereClause.IdFields == null)
            {
                WhereClause.IdFields = new List<DbField>();
            }
            WhereClause.IdFields?.Add(field);

            return this;
        }

        /// <summary>
        /// Adds the specified ID field.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="field">The ID field to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery AddIdField(bool canBeAdded, DbField field)
        {
            if (canBeAdded)
            {
                return AddIdField(field);
            }

            return this;
        }

        /// <summary>
        /// Adds the ID field using an initialization function.
        /// </summary>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery AddIdField(Func<IDbSingleQuery, DbField> initializer)
        {
            return AddIdField(initializer?.Invoke(this));
        }

        /// <summary>
        /// Adds the ID field using an initialization function.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery AddIdField(bool canBeAdded, Func<IDbSingleQuery, DbField> initializer)
        {
            if (canBeAdded)
            {
                return AddIdField(initializer);
            }

            return this;
        }

        // OrderBy -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statements"></param>
        /// <returns></returns>
        public IDbSingleQuery OrderBy(params IDbQueryOrderByStatement[] statements)
        {
            OrderByClause = new DbQueryOrderByClause() { Statements = statements?.Cast<DbQueryOrderByStatement>().ToList() };
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery OrderBy(IDataExpression expression)
        {
            OrderByClause = new DbQueryOrderByClause() { Expression = expression as DataExpression };
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery OrderBy(Func<IDbSingleQuery, IDataExpression> initializer)
        {
            return OrderBy(initializer?.Invoke(this));
        }

        // GroupBy -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields">The tables to consider.</param>
        public IDbSingleQuery GroupBy(params DbField[] fields)
        {
            GroupByClause = new DbQueryGroupByClause() { Fields = fields?.ToList() };
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery GroupBy(IDataExpression expression)
        {
            GroupByClause = new DbQueryGroupByClause() { Expression = expression as DataExpression };
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery GroupBy(Func<IDbSingleQuery, IDataExpression> initializer)
        {
            return GroupBy(initializer?.Invoke(this));
        }

        // Having -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery Having(IDataExpression expression)
        {
            HavingClause = new DbQueryHavingClause() { Expression = expression as DataExpression };
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery Having(Func<IDbSingleQuery, IDataExpression> initializer)
        {
            return Having(initializer?.Invoke(this));
        }

        #endregion
    }
}