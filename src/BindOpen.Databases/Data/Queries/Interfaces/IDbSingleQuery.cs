using BindOpen.Data.Expression;
using BindOpen.Extensions.Carriers;
using System;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbSingleQuery : IDbQuery
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsDistinct { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int Limit { get; set; }

        /// <summary>
        /// The returned IDs of this instance.
        /// </summary>
        /// <remarks>This string is split with a comma.</remarks>
        List<DbField> ReturnedIdFields { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryFromClause FromClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryWhereClause WhereClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryGroupByClause GroupByClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryHavingClause HavingClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryOrderByClause OrderByClause { get; set; }

        // Accessors ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boundFieldName"></param>
        /// <returns></returns>
        DbField GetIdFieldWithBoundFieldName(string boundFieldName);

        // Mutators ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery AsDistinct();

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery WithLimit(int limit);

        /// <summary>
        /// Sets the specified fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery WithFields(params DbField[] fields);

        /// <summary>
        /// Sets the specified returned ID fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery WithReturnedIdFields(params DbField[] fields);

        /// <summary>
        /// Sets the fields using an initialization function.
        /// </summary>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery WithFields(Func<IDbSingleQuery, DbField[]> initiliazer);

        /// <summary>
        /// Adds the specified field.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddField(DbField field);

        /// <summary>
        /// Adds the specified field.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="field">The field to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddField(bool canBeAdded, DbField field);

        /// <summary>
        /// Sets the field using an initialization function.
        /// </summary>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddField(Func<IDbSingleQuery, DbField> initiliazer);

        /// <summary>
        /// Sets the field using an initialization function.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddField(bool canBeAdded, Func<IDbSingleQuery, DbField> initiliazer);

        // IdFields -------------------------------------

        /// <summary>
        /// Sets the specified ID fields.
        /// </summary>
        /// <param name="fields">The ID fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery WithIdFields(params DbField[] fields);

        /// <summary>
        /// Sets the ID fields using an initialization function.
        /// </summary>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery WithIdFields(Func<IDbSingleQuery, DbField[]> initiliazer);

        /// <summary>
        /// Adds the specified ID field.
        /// </summary>
        /// <param name="field">The ID field to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddIdField(DbField field);

        /// <summary>
        /// Adds the specified ID field.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="field">The ID field to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddIdField(bool canBeAdded, DbField field);

        /// <summary>
        /// Adds the ID field using an initialization function.
        /// </summary>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddIdField(Func<IDbSingleQuery, DbField> initiliazer);

        /// <summary>
        /// Adds the ID field using an initialization function.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddIdField(bool canBeAdded, Func<IDbSingleQuery, DbField> initiliazer);

        // From -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tables">The tables to consider.</param>
        IDbSingleQuery From(params DbTable[] tables);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tables">The tables to consider.</param>
        /// <param name="unionClauses">The union clauses to consider.</param>
        IDbSingleQuery From(DbTable[] tables, params DbUnionTable[] unionClauses);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery From(IDataExpression expression);

        // Where -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery Where(IDataExpression expression);

        // OrderBy -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery OrderBy(params IDbQueryOrderByStatement[] statements);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery OrderBy(IDataExpression expression);

        // GroupBy -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields">The tables to consider.</param>
        IDbSingleQuery GroupBy(params DbField[] fields);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery GroupBy(IDataExpression expression);

        // Having -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery Having(IDataExpression expression);
    }
}