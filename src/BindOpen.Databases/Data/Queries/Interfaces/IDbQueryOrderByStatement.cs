using BindOpen.Data.Common;
using BindOpen.Extensions.Carriers;

namespace BindOpen.Data.Queries
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