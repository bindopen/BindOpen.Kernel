using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryOrderByClause : IDbQueryItem
    {
        /// <summary>
        /// The statements of this instance.
        /// </summary>
        List<DbQueryOrderByStatement> Statements { get; set; }
    }
}