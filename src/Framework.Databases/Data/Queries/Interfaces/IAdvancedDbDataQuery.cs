using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Expression;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAdvancedDbDataQuery : IDbDataQuery
    {
        /// <summary>
        /// 
        /// </summary>
        List<IDbDataQueryFromStatement> FromClauses { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDbDataQueryGroupByStatement GroupByClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDbDataQueryHavingStatement HavingClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsDistinct { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<IDbDataQueryOrderByStatement> OrderByStatements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int Top { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDataExpression WhereClause { get; set; }
    }
}