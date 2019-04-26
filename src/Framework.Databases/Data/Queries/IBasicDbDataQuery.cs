using System.Collections.Generic;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
{
    public interface IBasicDbDataQuery : IDbDataQuery
    {
        List<IDbDataQueryFromStatement> FromClauses { get; set; }
        List<DbField> IdFields { get; set; }
        bool IsDistinct { get; set; }
        List<IDbDataQueryOrderByStatement> OrderByStatements { get; set; }
        int Top { get; set; }

        DbField GetIdFieldWithBoundFieldName(string boundFieldName);
    }
}