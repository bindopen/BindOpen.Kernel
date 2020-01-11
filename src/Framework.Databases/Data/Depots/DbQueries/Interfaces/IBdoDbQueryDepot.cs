using BindOpen.Framework.Data.Queries;
using System.Collections.Generic;

namespace BindOpen.Framework.Data.Depots
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDbQueryDepot : ITBdoDepot<StoredDbQuery>
    {
        /// <summary>
        /// 
        /// </summary>
        List<StoredDbQuery> Queries { get; set; }

        /// <summary>
        /// Gets the database query with the specified name.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <returns>Returns the database query with the specified name.</returns>
        StoredDbQuery GetQuery(string name);
    }
}