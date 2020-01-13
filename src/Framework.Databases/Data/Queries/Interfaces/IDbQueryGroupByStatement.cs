using System.Collections.Generic;
using BindOpen.Framework.Extensions.Carriers;

namespace BindOpen.Framework.Data.Queries
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