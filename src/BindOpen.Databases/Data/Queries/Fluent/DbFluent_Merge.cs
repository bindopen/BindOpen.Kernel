using BindOpen.Extensions.Carriers;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a fluent factory of database query.
    /// </summary>
    public static partial class DbFluent
    {
        // Upsert --------------------------------

        /// <summary>
        /// Creates a new Upsert basic database query.
        /// </summary>
        /// <param name="name">The name of the query to consider.</param>
        /// <param name="table">The table to consider.</param>
        /// <returns>Returns a new Upsert basic database query</returns>
        public static IDbCompositeQuery Upsert(
            string name,
            DbTable table)
        {
            var query = new DbCompositeQuery(name, DbQueryKind.Upsert, table);

            return query;
        }

        /// <summary>
        /// Creates a new Upsert basic database query.
        /// </summary>
        /// <param name="table">The table to consider.</param>
        /// <param name="selectQuery">The select query to consider.</param>
        /// <returns>Returns a new Upsert basic database query</returns>
        public static IDbCompositeQuery Upsert(
            DbTable table)
            => Upsert(null, table);
    }
}
