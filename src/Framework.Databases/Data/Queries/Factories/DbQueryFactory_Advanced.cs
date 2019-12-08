using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a factory of database query.
    /// </summary>
    public static partial class DbQueryFactory
    {
        /// <summary>
        /// Creates a new stored database query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <returns>Returns a new Select basic database query</returns>
        public static IStoredDbQuery CreateStored(IDbQuery query)
            => CreateStored(null, query);

        /// <summary>
        /// Creates a new stored basic database query.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="query">The query to consider.</param>
        /// <returns>Returns a new Select basic database query</returns>
        public static IStoredDbQuery CreateStored(string name, IDbQuery query)
            => new StoredDbQuery() { Name = name, Query = query, ParameterSpecSet = query?.ParameterSpecSet };


        // Delete --------------------------------

        /// <summary>
        /// Creates a new Delete basic database query.
        /// </summary>
        /// <returns>Returns a new Delete basic database query</returns>
        public static IBasicDbQuery CreateBasicDelete(string name, DbTable table)
            => new BasicDbQuery(name, DbQueryKind.Delete, table?.DataModule, table?.Schema, table?.Name);

        /// <summary>
        /// Creates a new Delete basic database query.
        /// </summary>
        /// <returns>Returns a new Delete basic database query</returns>
        public static IBasicDbQuery CreateBasicDelete(DbTable table)
            => CreateBasicDelete(null, table);

        // Drop --------------------------------

        /// <summary>
        /// Creates a new Drop basic database query.
        /// </summary>
        /// <returns>Returns a new Drop basic database query</returns>
        public static IBasicDbQuery CreateBasicDrop(string name, DbTable table)
            => new BasicDbQuery(name, DbQueryKind.Drop, table?.DataModule, table?.Schema, table?.Name);

        /// <summary>
        /// Creates a new Drop basic database query.
        /// </summary>
        /// <returns>Returns a new Drop basic database query</returns>
        public static IBasicDbQuery CreateBasicDrop(DbTable table)
            => CreateBasicDrop(null, table);

        // Duplicate --------------------------------

        /// <summary>
        /// Creates a new Duplicate basic database query.
        /// </summary>
        /// <returns>Returns a new Duplicate basic database query</returns>
        public static IBasicDbQuery CreateBasicDuplicate(string name, DbTable table)
            => new BasicDbQuery(name, DbQueryKind.Duplicate, table?.DataModule, table?.Schema, table?.Name);

        /// <summary>
        /// Creates a new Duplicate basic database query.
        /// </summary>
        /// <returns>Returns a new Duplicate basic database query</returns>
        public static IBasicDbQuery CreateBasicDuplicate(DbTable table)
            => CreateBasicDuplicate(null, table);

        // Insert --------------------------------

        /// <summary>
        /// Creates a new Insert basic database query.
        /// </summary>
        /// <returns>Returns a new Insert basic database query</returns>
        public static IBasicDbQuery CreateBasicInsert(string name, DbTable table)
            => new BasicDbQuery(name, DbQueryKind.Insert, table?.DataModule, table?.Schema, table?.Name);

        /// <summary>
        /// Creates a new Insert basic database query.
        /// </summary>
        /// <returns>Returns a new Insert basic database query</returns>
        public static IBasicDbQuery CreateBasicInsert(DbTable table)
            => CreateBasicInsert(null, table);

        // Select --------------------------------

        /// <summary>
        /// Creates a new Select basic database query.
        /// </summary>
        /// <returns>Returns a new Select basic database query</returns>
        public static IBasicDbQuery CreateBasicSelect(string name, DbTable table)
            => new BasicDbQuery(name, DbQueryKind.Select, table?.DataModule, table?.Schema, table?.Name);

        /// <summary>
        /// Creates a new Select basic database query.
        /// </summary>
        /// <returns>Returns a new Select basic database query</returns>
        public static IBasicDbQuery CreateBasicSelect(DbTable table)
            => CreateBasicSelect(null, table);

        // Update --------------------------------

        /// <summary>
        /// Creates a new Update basic database query.
        /// </summary>
        /// <returns>Returns a new Update basic database query</returns>
        public static IBasicDbQuery CreateBasicUpdate(string name, DbTable table)
            => new BasicDbQuery(name, DbQueryKind.Update, table?.DataModule, table?.Schema, table?.Name);

        /// <summary>
        /// Creates a new Update basic database query.
        /// </summary>
        /// <returns>Returns a new Update basic database query</returns>
        public static IBasicDbQuery CreateBasicUpdate(DbTable table)
            => CreateBasicUpdate(null, table);
    }
}
