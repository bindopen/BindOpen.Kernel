using System.Collections.Generic;
using BindOpen.Extensions.Carriers;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryGroupByStatement
    {
        /// <summary>
        /// 
        /// </summary>
        List<DbField> Fields { get; set; }
    }
}