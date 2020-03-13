using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbStoredQuery : IDbQuery, IDescribedDataItem, IIdentifiedDataItem
    {
        /// <summary>
        /// The query of this instance.
        /// </summary>
        IDbQuery Query { get; set; }

        /// <summary>
        /// The SQL query texts of this instance depending on connector unique.
        /// </summary>
        Dictionary<string, string> QueryTexts { get; set; }
    }
}