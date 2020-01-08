using BindOpen.Framework.Databases.Extensions.Carriers;
using System;

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
        public static IAdvancedDbQuery CreateAdvancedDelete(string name, DbTable table, Action<IAdvancedDbQuery> initAction = null)
        {
            IAdvancedDbQuery query = new AdvancedDbQuery(name, DbQueryKind.Select, table?.DataModule, table?.Schema, table?.Name);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Delete advanced database query.
        /// </summary>
        /// <returns>Returns a new Delete advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedDelete(DbTable table, Action<IAdvancedDbQuery> initAction = null)
            => CreateAdvancedDelete(null, table, initAction);

        // Create --------------------------------

        /// <summary>
        /// Creates a new Create advanced database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        public static IAdvancedDbQuery CreateAdvancedCreate(string name, DbTable table, bool onlyIfNotExisting = true, Action<IAdvancedDbQuery> initAction = null)
        {
            IAdvancedDbQuery query = new AdvancedDbQuery(name, DbQueryKind.Create, table?.DataModule, table?.Schema, table?.Name, onlyIfNotExisting);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Create advanced database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        public static IAdvancedDbQuery CreateAdvancedCreate(DbTable table, bool onlyIfNotExisting = true, Action<IAdvancedDbQuery> initAction = null)
            => CreateAdvancedCreate(null, table, onlyIfNotExisting, initAction);

        // Drop --------------------------------

        /// <summary>
        /// Creates a new Drop advanced database query.
        /// </summary>
        /// <returns>Returns a new Drop advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedDrop(string name, DbTable table, bool onlyIfExisting = true, Action<IAdvancedDbQuery> initAction = null)
        {
            IAdvancedDbQuery query = new AdvancedDbQuery(name, DbQueryKind.Drop, table?.DataModule, table?.Schema, table?.Name, onlyIfExisting);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Drop advanced database query.
        /// </summary>
        /// <returns>Returns a new Drop advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedDrop(DbTable table, bool onlyIfExisting = true, Action<IAdvancedDbQuery> initAction = null)
            => CreateAdvancedDrop(null, table, onlyIfExisting, initAction);

        // Duplicate --------------------------------

        /// <summary>
        /// Creates a new Duplicate advanced database query.
        /// </summary>
        /// <returns>Returns a new Duplicate advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedDuplicate(string name, DbTable table, Action<IAdvancedDbQuery> initAction = null)
        {
            IAdvancedDbQuery query = new AdvancedDbQuery(name, DbQueryKind.Duplicate, table?.DataModule, table?.Schema, table?.Name);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Duplicate advanced database query.
        /// </summary>
        /// <returns>Returns a new Duplicate advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedDuplicate(DbTable table, Action<IAdvancedDbQuery> initAction = null)
            => CreateAdvancedDuplicate(null, table, initAction);

        // Insert --------------------------------

        /// <summary>
        /// Creates a new Insert advanced database query.
        /// </summary>
        /// <returns>Returns a new Insert advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedInsert(string name, DbTable table, Action<IAdvancedDbQuery> initAction = null)
        {
            IAdvancedDbQuery query = new AdvancedDbQuery(name, DbQueryKind.Insert, table?.DataModule, table?.Schema, table?.Name);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Insert advanced database query.
        /// </summary>
        /// <returns>Returns a new Insert advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedInsert(DbTable table, Action<IAdvancedDbQuery> initAction = null)
            => CreateAdvancedInsert(null, table, initAction);

        // Select --------------------------------

        /// <summary>
        /// Creates a new Select advanced database query.
        /// </summary>
        /// <returns>Returns a new Select advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedSelect(string name, DbTable table, Action<IAdvancedDbQuery> initAction = null)
        {
            IAdvancedDbQuery query = new AdvancedDbQuery(name, DbQueryKind.Select, table?.DataModule, table?.Schema, table?.Name);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Select advanced database query.
        /// </summary>
        /// <returns>Returns a new Select advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedSelect(DbTable table, Action<IAdvancedDbQuery> initAction = null)
            => CreateAdvancedSelect(null, table, initAction);

        // Update --------------------------------

        /// <summary>
        /// Creates a new Update advanced database query.
        /// </summary>
        /// <returns>Returns a new Update advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedUpdate(string name, DbTable table, Action<IAdvancedDbQuery> initAction = null)
        {
            IAdvancedDbQuery query = new AdvancedDbQuery(name, DbQueryKind.Update, table?.DataModule, table?.Schema, table?.Name);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Update advanced database query.
        /// </summary>
        /// <returns>Returns a new Update advanced database query</returns>
        public static IAdvancedDbQuery CreateAdvancedUpdate(DbTable table, Action<IAdvancedDbQuery> initAction = null)
            => CreateAdvancedUpdate(null, table, initAction);
    }
}
