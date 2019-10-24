using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbDataQueryOrderByStatement
    {
        /// <summary>
        /// 
        /// </summary>
        DbField Field { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataSortingMode Sorting { get; set; }
    }
}