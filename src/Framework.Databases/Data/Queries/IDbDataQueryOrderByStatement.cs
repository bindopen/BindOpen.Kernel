using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
{
    public interface IDbDataQueryOrderByStatement
    {
        DbField Field { get; set; }
        DataSortingMode Sorting { get; set; }
    }
}