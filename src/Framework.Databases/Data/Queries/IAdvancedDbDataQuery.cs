using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Expression;

namespace BindOpen.Framework.Databases.Data.Queries
{
    public interface IAdvancedDbDataQuery : IDbDataQuery
    {
        List<IDbDataQueryFromStatement> FromClauses { get; set; }
        IDbDataQueryGroupByStatement GroupByClause { get; set; }
        IDbDataQueryHavingStatement HavingClause { get; set; }
        bool IsDistinct { get; set; }
        List<IDbDataQueryOrderByStatement> OrderByStatements { get; set; }
        int Top { get; set; }
        IDataExpression WhereClause { get; set; }
    }
}