using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbCompositeQuery : IDbQuery, IIdentifiedDataItem
    {
        /// <summary>
        /// The queries of this instance.
        /// </summary>
        List<DbQuery> Queries { get; set; }

        /// <summary>
        /// Sets the specified queries.
        /// </summary>
        /// <param name="queries">The queries to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbCompositeQuery WithQueries(params IDbQuery[] queries);
    }
}