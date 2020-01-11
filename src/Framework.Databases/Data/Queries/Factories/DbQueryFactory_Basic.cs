using BindOpen.Framework.Databases.Extensions.Carriers;
using System;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents a factory of database query.
    /// </summary>
    public static partial class DbQueryFactory
    {
        // Delete --------------------------------

        /// <summary>
        /// Creates a new Delete basic database query.
        /// </summary>
        /// <returns>Returns a new Delete basic database query</returns>
        public static IBasicDbQuery CreateBasicDelete(string name, DbTable table, Action<IBasicDbQuery> initAction = null)
        {
            IBasicDbQuery query = new BasicDbQuery(name, DbQueryKind.Delete, table?.DataModule, table?.Schema, table?.Name);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Delete basic database query.
        /// </summary>
        /// <returns>Returns a new Delete basic database query</returns>
        public static IBasicDbQuery CreateBasicDelete(DbTable table, Action<IBasicDbQuery> initAction = null)
            => CreateBasicDelete(null, table, initAction);

        // Create --------------------------------

        /// <summary>
        /// Creates a new Create basic database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        public static IBasicDbQuery CreateBasicCreate(string name, DbTable table, bool onlyIfNotExisting = true, Action<IBasicDbQuery> initAction = null)
        {
            IBasicDbQuery query = new BasicDbQuery(name, DbQueryKind.Create, table?.DataModule, table?.Schema, table?.Name, onlyIfNotExisting);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Create basic database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        public static IBasicDbQuery CreateBasicCreate(DbTable table, bool onlyIfNotExisting = true, Action<IBasicDbQuery> initAction = null)
            => CreateBasicCreate(null, table, onlyIfNotExisting, initAction);

        // Drop --------------------------------

        /// <summary>
        /// Creates a new Drop basic database query.
        /// </summary>
        /// <returns>Returns a new Drop basic database query</returns>
        public static IBasicDbQuery CreateBasicDrop(string name, DbTable table, bool onlyIfExisting = true, Action<IBasicDbQuery> initAction = null)
        {
            IBasicDbQuery query = new BasicDbQuery(name, DbQueryKind.Drop, table?.DataModule, table?.Schema, table?.Name, onlyIfExisting);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Drop basic database query.
        /// </summary>
        /// <returns>Returns a new Drop basic database query</returns>
        public static IBasicDbQuery CreateBasicDrop(DbTable table, bool onlyIfExisting = true, Action<IBasicDbQuery> initAction = null)
            => CreateBasicDrop(null, table, onlyIfExisting, initAction);

        // Duplicate --------------------------------

        /// <summary>
        /// Creates a new Duplicate basic database query.
        /// </summary>
        /// <returns>Returns a new Duplicate basic database query</returns>
        public static IBasicDbQuery CreateBasicDuplicate(string name, DbTable table, Action<IBasicDbQuery> initAction = null)
        {
            IBasicDbQuery query = new BasicDbQuery(name, DbQueryKind.Duplicate, table?.DataModule, table?.Schema, table?.Name);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Duplicate basic database query.
        /// </summary>
        /// <returns>Returns a new Duplicate basic database query</returns>
        public static IBasicDbQuery CreateBasicDuplicate(DbTable table, Action<IBasicDbQuery> initAction = null)
            => CreateBasicDuplicate(null, table, initAction);

        // Insert --------------------------------

        /// <summary>
        /// Creates a new Insert basic database query.
        /// </summary>
        /// <returns>Returns a new Insert basic database query</returns>
        public static IBasicDbQuery CreateBasicInsert(string name, DbTable table, bool onlyIfNotExisting = true, Action<IBasicDbQuery> initAction = null)
        {
            IBasicDbQuery query = new BasicDbQuery(name, DbQueryKind.Insert, table?.DataModule, table?.Schema, table?.Name, onlyIfNotExisting);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Insert basic database query.
        /// </summary>
        /// <returns>Returns a new Insert basic database query</returns>
        public static IBasicDbQuery CreateBasicInsert(DbTable table, bool onlyIfNotExisting = true, Action<IBasicDbQuery> initAction = null)
            => CreateBasicInsert(null, table, onlyIfNotExisting, initAction);

        // Select --------------------------------

        /// <summary>
        /// Creates a new Select basic database query.
        /// </summary>
        /// <returns>Returns a new Select basic database query</returns>
        public static IBasicDbQuery CreateBasicSelect(string name, DbTable table, Action<IBasicDbQuery> initAction = null)
        {
            IBasicDbQuery query = new BasicDbQuery(name, DbQueryKind.Select, table?.DataModule, table?.Schema, table?.Name);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Select basic database query.
        /// </summary>
        /// <returns>Returns a new Select basic database query</returns>
        public static IBasicDbQuery CreateBasicSelect(DbTable table, Action<IBasicDbQuery> initAction = null)
            => CreateBasicSelect(null, table, initAction);

        // Update --------------------------------

        /// <summary>
        /// Creates a new Update basic database query.
        /// </summary>
        /// <returns>Returns a new Update basic database query</returns>
        public static IBasicDbQuery CreateBasicUpdate(string name, DbTable table, Action<IBasicDbQuery> initAction = null)
        {
            IBasicDbQuery query = new BasicDbQuery(name, DbQueryKind.Update, table?.DataModule, table?.Schema, table?.Name);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Update basic database query.
        /// </summary>
        /// <returns>Returns a new Update basic database query</returns>
        public static IBasicDbQuery CreateBasicUpdate(DbTable table, Action<IBasicDbQuery> initAction = null)
            => CreateBasicUpdate(null, table, initAction);
    }
}
