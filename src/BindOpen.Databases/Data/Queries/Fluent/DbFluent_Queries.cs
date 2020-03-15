using BindOpen.Extensions.Carriers;
using System;

namespace BindOpen.Data.Queries
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
        public static IDbSingleQuery DeleteQuery(string name, DbTable table, Action<IDbSingleQuery> initAction = null)
        {
            IDbSingleQuery query = new DbSingleQuery(name, DbQueryKind.Delete, table);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Delete advanced database query.
        /// </summary>
        /// <returns>Returns a new Delete advanced database query</returns>
        public static IDbSingleQuery DeleteQuery(DbTable table, Action<IDbSingleQuery> initAction = null)
            => DeleteQuery(null, table, initAction);

        // Create --------------------------------

        /// <summary>
        /// Creates a new Create advanced database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        public static IDbSingleQuery CreateQuery(string name, DbTable table, bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null)
        {
            IDbSingleQuery query = new DbSingleQuery(name, DbQueryKind.Create, table);
            query.CheckExistence(onlyIfNotExisting);

            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Create advanced database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        public static IDbSingleQuery CreateQuery(DbTable table, bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null)
            => CreateQuery(null, table, onlyIfNotExisting, initAction);

        // Drop --------------------------------

        /// <summary>
        /// Creates a new Drop advanced database query.
        /// </summary>
        /// <returns>Returns a new Drop advanced database query</returns>
        public static IDbSingleQuery DropQuery(string name, DbTable table, bool onlyIfExisting = true, Action<IDbSingleQuery> initAction = null)
        {
            IDbSingleQuery query = new DbSingleQuery(name, DbQueryKind.Drop, table);
            query.CheckExistence(onlyIfExisting);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Drop advanced database query.
        /// </summary>
        /// <returns>Returns a new Drop advanced database query</returns>
        public static IDbSingleQuery DropQuery(DbTable table, bool onlyIfExisting = true, Action<IDbSingleQuery> initAction = null)
            => DropQuery(null, table, onlyIfExisting, initAction);

        // Insert --------------------------------

        /// <summary>
        /// Creates a new Insert advanced database query.
        /// </summary>
        /// <returns>Returns a new Insert advanced database query</returns>
        public static IDbSingleQuery InsertQuery(string name, DbTable table, bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null)
        {
            IDbSingleQuery query = new DbSingleQuery(name, DbQueryKind.Insert, table);
            query.CheckExistence(onlyIfNotExisting);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Insert advanced database query.
        /// </summary>
        /// <returns>Returns a new Insert advanced database query</returns>
        public static IDbSingleQuery InsertQuery(DbTable table, bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null)
            => InsertQuery(null, table, onlyIfNotExisting, initAction);

        // Select --------------------------------

        /// <summary>
        /// Creates a new Select advanced database query.
        /// </summary>
        /// <returns>Returns a new Select advanced database query</returns>
        public static IDbSingleQuery SelectQuery(string name, DbTable table, Action<IDbSingleQuery> initAction = null)
        {
            IDbSingleQuery query = new DbSingleQuery(name, DbQueryKind.Select, table);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Select advanced database query.
        /// </summary>
        /// <returns>Returns a new Select advanced database query</returns>
        public static IDbSingleQuery SelectQuery(DbTable table, Action<IDbSingleQuery> initAction = null)
            => SelectQuery(null, table, initAction);

        // Update --------------------------------

        /// <summary>
        /// Creates a new Update advanced database query.
        /// </summary>
        /// <returns>Returns a new Update advanced database query</returns>
        public static IDbSingleQuery UpdateQuery(string name, DbTable table, Action<IDbSingleQuery> initAction = null)
        {
            IDbSingleQuery query = new DbSingleQuery(name, DbQueryKind.Update, table);
            initAction?.Invoke(query);

            return query;
        }

        /// <summary>
        /// Creates a new Update advanced database query.
        /// </summary>
        /// <returns>Returns a new Update advanced database query</returns>
        public static IDbSingleQuery UpdateQuery(DbTable table, Action<IDbSingleQuery> initAction = null)
            => UpdateQuery(null, table, initAction);
    }
}
