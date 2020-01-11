using BindOpen.Framework.Data.Expression;
using BindOpen.Framework.Databases.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAdvancedDbQuery : IDbQuery
    {
        /// <summary>
        /// 
        /// </summary>
        List<DbQueryFromStatement> FromStatements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryGroupByStatement GroupByClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryHavingStatement HavingClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsDistinct { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<DbQueryOrderByStatement> OrderByStatements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int Top { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataExpression WhereClause { get; set; }

        // Mutators ---------------------------------------


        /// <summary>
        /// Sets the specified fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        IAdvancedDbQuery WithFields(params DbField[] fields);

        /// <summary>
        /// 
        /// </summary>
        IAdvancedDbQuery From(params IDbQueryFromStatement[] statements);

        /// <summary>
        /// 
        /// </summary>
        IAdvancedDbQuery GroupBy(IDbQueryGroupByStatement clause);

        /// <summary>
        /// 
        /// </summary>
        IAdvancedDbQuery Having(IDbQueryHavingStatement clause);

        /// <summary>
        /// 
        /// </summary>
        IAdvancedDbQuery AsDistinct();

        /// <summary>
        /// 
        /// </summary>
        IAdvancedDbQuery OrderBy(params IDbQueryOrderByStatement[] statements);

        /// <summary>
        /// 
        /// </summary>
        IAdvancedDbQuery WithTop(int top);

        /// <summary>
        /// 
        /// </summary>
        IAdvancedDbQuery Where(IDataExpression clause);

        /// <summary>
        /// 
        /// </summary>
        IAdvancedDbQuery WithTableAlias(string tableAlias);
    }
}