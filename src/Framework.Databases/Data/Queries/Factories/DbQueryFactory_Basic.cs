using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a factory of database query.
    /// </summary>
    public static partial class DbQueryFactory
    {
        // Delete --------------------------------

        /// <summary>
        /// Creates a new Delete advanced database query.
        /// </summary>
        /// <returns>Returns a new Delete advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedDelete(string name, DbTable table)
            => new AdvancedDbQuery(name, DbQueryKind.Select, table?.DataModule, table?.Schema, table?.Name);

        /// <summary>
        /// Creates a new Delete advanced database query.
        /// </summary>
        /// <returns>Returns a new Delete advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedDelete(DbTable table)
            => CreateAdvancedDelete(null, table);

        // Drop --------------------------------

        /// <summary>
        /// Creates a new Drop advanced database query.
        /// </summary>
        /// <returns>Returns a new Drop advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedDrop(string name, DbTable table)
            => new AdvancedDbQuery(name, DbQueryKind.Select, table?.DataModule, table?.Schema, table?.Name);

        /// <summary>
        /// Creates a new Drop advanced database query.
        /// </summary>
        /// <returns>Returns a new Drop advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedDrop(DbTable table)
            => CreateAdvancedDrop(null, table);

        // Duplicate --------------------------------

        /// <summary>
        /// Creates a new Duplicate advanced database query.
        /// </summary>
        /// <returns>Returns a new Duplicate advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedDuplicate(string name, DbTable table)
            => new AdvancedDbQuery(name, DbQueryKind.Duplicate, table?.DataModule, table?.Schema, table?.Name);

        /// <summary>
        /// Creates a new Duplicate advanced database query.
        /// </summary>
        /// <returns>Returns a new Duplicate advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedDuplicate(DbTable table)
            => CreateAdvancedDuplicate(null, table);

        // Insert --------------------------------

        /// <summary>
        /// Creates a new Insert advanced database query.
        /// </summary>
        /// <returns>Returns a new Insert advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedInsert(string name, DbTable table)
            => new AdvancedDbQuery(name, DbQueryKind.Insert, table?.DataModule, table?.Schema, table?.Name);

        /// <summary>
        /// Creates a new Insert advanced database query.
        /// </summary>
        /// <returns>Returns a new Insert advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedInsert(DbTable table)
            => CreateAdvancedInsert(null, table);

        // Select --------------------------------

        /// <summary>
        /// Creates a new Select advanced database query.
        /// </summary>
        /// <returns>Returns a new Select advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedSelect(string name, DbTable table)
            => new AdvancedDbQuery(name, DbQueryKind.Select, table?.DataModule, table?.Schema, table?.Name);

        /// <summary>
        /// Creates a new Select advanced database query.
        /// </summary>
        /// <returns>Returns a new Select advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedSelect(DbTable table)
            => CreateAdvancedSelect(null, table);

        // Update --------------------------------

        /// <summary>
        /// Creates a new Update advanced database query.
        /// </summary>
        /// <returns>Returns a new Update advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedUpdate(string name, DbTable table)
            => new AdvancedDbQuery(name, DbQueryKind.Update, table?.DataModule, table?.Schema, table?.Name);

        /// <summary>
        /// Creates a new Update advanced database query.
        /// </summary>
        /// <returns>Returns a new Update advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedUpdate(DbTable table)
            => CreateAdvancedUpdate(null, table);
    }
}
