using BindOpen.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryFromStatement : IDbQueryItem
    {
        /// <summary>
        /// The tables of this instance.
        /// </summary>
        List<DbTable> Tables { get; set; }
    }
}