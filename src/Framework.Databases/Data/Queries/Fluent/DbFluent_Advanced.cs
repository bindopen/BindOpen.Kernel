using BindOpen.Framework.Extensions.Carriers;
using System;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents a fluent factory of database query.
    /// </summary>
    public static partial class DbFluent
    {
        // Delete --------------------------------

        /// <summary>
        /// Creates a new Delete advanced database query.
        /// </summary>
        /// <returns>Returns a new Delete advanced database query</returns>
        public static IAdvancedDbQuery DeleteAdvanced(string name, DbTable table, Action<IAdvancedDbQuery> initAction = null)
        {
            IAdvancedDbQuery query = new AdvancedDbQuery(name, DbQueryKind.Select, table?.DataModule, table?.Schema, table?.Name);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Delete advanced database query.
        /// </summary>
        /// <returns>Returns a new Delete advanced database query</returns>
        public static IAdvancedDbQuery DeleteAdvanced(DbTable table, Action<IAdvancedDbQuery> initAction = null)
            => DeleteAdvanced(null, table, initAction);

        // Create --------------------------------

        /// <summary>
        /// Creates a new Create advanced database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        public static IAdvancedDbQuery CreateAdvanced(string name, DbTable table, bool onlyIfNotExisting = true, Action<IAdvancedDbQuery> initAction = null)
        {
            IAdvancedDbQuery query = new AdvancedDbQuery(name, DbQueryKind.Create, table?.DataModule, table?.Schema, table?.Name, onlyIfNotExisting);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Create advanced database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        public static IAdvancedDbQuery CreateAdvanced(DbTable table, bool onlyIfNotExisting = true, Action<IAdvancedDbQuery> initAction = null)
            => CreateAdvanced(null, table, onlyIfNotExisting, initAction);

        // Drop --------------------------------

        /// <summary>
        /// Creates a new Drop advanced database query.
        /// </summary>
        /// <returns>Returns a new Drop advanced database query</returns>
        public static IAdvancedDbQuery DropAdvanced(string name, DbTable table, bool onlyIfExisting = true, Action<IAdvancedDbQuery> initAction = null)
        {
            IAdvancedDbQuery query = new AdvancedDbQuery(name, DbQueryKind.Drop, table?.DataModule, table?.Schema, table?.Name, onlyIfExisting);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Drop advanced database query.
        /// </summary>
        /// <returns>Returns a new Drop advanced database query</returns>
        public static IAdvancedDbQuery DropAdvanced(DbTable table, bool onlyIfExisting = true, Action<IAdvancedDbQuery> initAction = null)
            => DropAdvanced(null, table, onlyIfExisting, initAction);

        // Duplicate --------------------------------

        /// <summary>
        /// Creates a new Duplicate advanced database query.
        /// </summary>
        /// <returns>Returns a new Duplicate advanced database query</returns>
        public static IAdvancedDbQuery DuplicateAdvanced(string name, DbTable table, Action<IAdvancedDbQuery> initAction = null)
        {
            IAdvancedDbQuery query = new AdvancedDbQuery(name, DbQueryKind.Duplicate, table?.DataModule, table?.Schema, table?.Name);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Duplicate advanced database query.
        /// </summary>
        /// <returns>Returns a new Duplicate advanced database query</returns>
        public static IAdvancedDbQuery DuplicateAdvanced(DbTable table, Action<IAdvancedDbQuery> initAction = null)
            => DuplicateAdvanced(null, table, initAction);

        // Insert --------------------------------

        /// <summary>
        /// Creates a new Insert advanced database query.
        /// </summary>
        /// <returns>Returns a new Insert advanced database query</returns>
        public static IAdvancedDbQuery InsertAdvanced(string name, DbTable table, Action<IAdvancedDbQuery> initAction = null)
        {
            IAdvancedDbQuery query = new AdvancedDbQuery(name, DbQueryKind.Insert, table?.DataModule, table?.Schema, table?.Name);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Insert advanced database query.
        /// </summary>
        /// <returns>Returns a new Insert advanced database query</returns>
        public static IAdvancedDbQuery InsertAdvanced(DbTable table, Action<IAdvancedDbQuery> initAction = null)
            => InsertAdvanced(null, table, initAction);

        // Select --------------------------------

        /// <summary>
        /// Creates a new Select advanced database query.
        /// </summary>
        /// <returns>Returns a new Select advanced database query</returns>
        public static IAdvancedDbQuery SelectAdvanced(string name, DbTable table, Action<IAdvancedDbQuery> initAction = null)
        {
            IAdvancedDbQuery query = new AdvancedDbQuery(name, DbQueryKind.Select, table?.DataModule, table?.Schema, table?.Name);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Select advanced database query.
        /// </summary>
        /// <returns>Returns a new Select advanced database query</returns>
        public static IAdvancedDbQuery SelectAdvanced(DbTable table, Action<IAdvancedDbQuery> initAction = null)
            => SelectAdvanced(null, table, initAction);

        // Update --------------------------------

        /// <summary>
        /// Creates a new Update advanced database query.
        /// </summary>
        /// <returns>Returns a new Update advanced database query</returns>
        public static IAdvancedDbQuery UpdateAdvanced(string name, DbTable table, Action<IAdvancedDbQuery> initAction = null)
        {
            IAdvancedDbQuery query = new AdvancedDbQuery(name, DbQueryKind.Update, table?.DataModule, table?.Schema, table?.Name);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Update advanced database query.
        /// </summary>
        /// <returns>Returns a new Update advanced database query</returns>
        public static IAdvancedDbQuery UpdateAdvanced(DbTable table, Action<IAdvancedDbQuery> initAction = null)
            => UpdateAdvanced(null, table, initAction);
    }
}
