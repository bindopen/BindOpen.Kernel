using BindOpen.Framework.Core.Data.Depots;
using BindOpen.Framework.Core.Data.Items.Datasources;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Databases.Data.Queries;
using System.Collections.Generic;

namespace BindOpen.Framework.Databases.Data.Depots.DbQueries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDbQueryDepot : ITBdoDepot<DbQuery>
    {
        /// <summary>
        /// 
        /// </summary>
        List<DbQuery> Queries { get; set; }

        /// <summary>
        /// Gets the database query with the specified name.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <returns>Returns the database query with the specified name.</returns>
        DbQuery GetQuery(string name);
    }
}