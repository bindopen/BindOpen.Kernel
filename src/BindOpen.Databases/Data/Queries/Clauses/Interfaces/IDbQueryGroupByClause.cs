using BindOpen.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryGroupByClause : IDbQueryItem
    {
        /// <summary>
        /// 
        /// </summary>
        List<DbField> Fields { get; set; }
    }
}