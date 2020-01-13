using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Extensions.Carriers;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryOrderByStatement
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