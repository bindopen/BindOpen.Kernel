using BindOpen.Framework.Core.Data.Depots;
using BindOpen.Framework.Databases.Data.Queries;
using System.Collections.Generic;

namespace BindOpen.Framework.Databases.Data.Depots.DbQueries
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