using System.Collections.Generic;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbDataQueryGroupByStatement
    {
        /// <summary>
        /// 
        /// </summary>
        List<DbField> Fields { get; set; }
    }
}