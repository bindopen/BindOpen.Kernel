using System.Collections.Generic;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
{
    public interface IDbDataQueryGroupByStatement
    {
        List<DbField> Fields { get; set; }
    }
}