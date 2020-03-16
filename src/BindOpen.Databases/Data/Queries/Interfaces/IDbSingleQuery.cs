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
        /// The fields of this instance.
        /// </summary>
        List<DbField> Fields { get; set; }

        /// <summary>
        /// The returned IDs of this instance.
        /// </summary>
        /// <remarks>This string is split with a comma.</remarks>
        List<DbField> ReturnedIdFields { get; set; }

        /// <summary>
        /// The union clauses of this instance.
        /// </summary>
        List<DbQueryUnionClause> UnionClauses { get; set; }

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
        /// <param name="name"></param>
        /// <returns></returns>
        DbField GetFieldWithName(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boundFieldName"></param>
        /// <returns></returns>
        DbField GetFieldWithBoundFieldName(string boundFieldName);

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
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery WithFields(Func<IDbSingleQuery, DbField[]> initializer);

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
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddField(Func<IDbSingleQuery, DbField> initializer);

        /// <summary>
        /// Sets the field using an initialization function.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddField(bool canBeAdded, Func<IDbSingleQuery, DbField> initializer);

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
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery WithIdFields(Func<IDbSingleQuery, DbField[]> initializer);

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
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddIdField(Func<IDbSingleQuery, DbField> initializer);

        /// <summary>
        /// Adds the ID field using an initialization function.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddIdField(bool canBeAdded, Func<IDbSingleQuery, DbField> initializer);

        // From -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tables">The tables to consider.</param>
        IDbSingleQuery From(params DbTable[] tables);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery From(IDataExpression expression);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery From(Func<IDbSingleQuery, IDataExpression> initializer);

        // Union -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unionClause">The union clause to consider.</param>
        IDbSingleQuery Union(DbQueryUnionKind kind, IDbSingleQuery query);

        // Where -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery Where(IDataExpression expression);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery Where(Func<IDbSingleQuery, IDataExpression> initializer);

        // OrderBy -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery OrderBy(params IDbQueryOrderByStatement[] statements);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery OrderBy(IDataExpression expression);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery OrderBy(Func<IDbSingleQuery, IDataExpression> initializer);

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

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery GroupBy(Func<IDbSingleQuery, IDataExpression> initializer);

        // Having -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery Having(IDataExpression expression);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery Having(Func<IDbSingleQuery, IDataExpression> initializer);
    }
}