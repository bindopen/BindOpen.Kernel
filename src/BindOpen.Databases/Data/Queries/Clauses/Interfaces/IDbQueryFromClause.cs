using BindOpen.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryFromClause : IDbQueryItem
    {
        /// <summary>
        /// The tables of this instance.
        /// </summary>
        List<DbTable> Tables { get; set; }

        /// <summary>
        /// The union tables of this instance.
        /// </summary>
        List<DbUnionTable> UnionTables { get; set; }
    }
}