using BindOpen.Extensions.Carriers;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a fluent factory of database query.
    /// </summary>
    public static partial class DbFluent
    {
        // Delete --------------------------------

        /// <summary>
        /// Creates a new Delete basic database query.
        /// </summary>
        /// <returns>Returns a new Delete basic database query</returns>
        public static IBasicDbQuery DeleteBasic(string name, DbTable table)
        {
            IBasicDbQuery query = new BasicDbQuery(name, DbQueryKind.Delete, table);

            return query;
        }

        /// <summary>
        /// Creates a new Delete basic database query.
        /// </summary>
        /// <returns>Returns a new Delete basic database query</returns>
        public static IBasicDbQuery DeleteBasic(DbTable table)
            => DeleteBasic(null, table);

        // Create --------------------------------

        /// <summary>
        /// Creates a new Create basic database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        public static IBasicDbQuery CreateBasic(string name, DbTable table, bool onlyIfNotExisting = true)
        {
            IBasicDbQuery query = new BasicDbQuery(name, DbQueryKind.Create, table);
            query.CheckExistence(onlyIfNotExisting);
            return query;
        }

        /// <summary>
        /// Creates a new Create basic database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        public static IBasicDbQuery CreateBasic(DbTable table, bool onlyIfNotExisting = true)
            => CreateBasic(null, table, onlyIfNotExisting);

        // Drop --------------------------------

        /// <summary>
        /// Creates a new Drop basic database query.
        /// </summary>
        /// <returns>Returns a new Drop basic database query</returns>
        public static IBasicDbQuery DropBasic(string name, DbTable table, bool onlyIfExisting = true)
        {
            IBasicDbQuery query = new BasicDbQuery(name, DbQueryKind.Drop, table);
            query.CheckExistence(onlyIfExisting);

            return query;
        }

        /// <summary>
        /// Creates a new Drop basic database query.
        /// </summary>
        /// <returns>Returns a new Drop basic database query</returns>
        public static IBasicDbQuery DropBasic(DbTable table, bool onlyIfExisting = true)
            => DropBasic(null, table, onlyIfExisting);

        // Insert --------------------------------

        /// <summary>
        /// Creates a new Insert basic database query.
        /// </summary>
        /// <returns>Returns a new Insert basic database query</returns>
        public static IBasicDbQuery InsertBasic(string name, DbTable table, bool onlyIfNotExisting = true)
        {
            IBasicDbQuery query = new BasicDbQuery(name, DbQueryKind.Insert, table);
            query.CheckExistence(onlyIfNotExisting);

            return query;
        }

        /// <summary>
        /// Creates a new Insert basic database query.
        /// </summary>
        /// <returns>Returns a new Insert basic database query</returns>
        public static IBasicDbQuery InsertBasic(DbTable table, bool onlyIfNotExisting = true)
            => InsertBasic(null, table, onlyIfNotExisting);

        // Select --------------------------------

        /// <summary>
        /// Creates a new Select basic database query.
        /// </summary>
        /// <returns>Returns a new Select basic database query</returns>
        public static IBasicDbQuery SelectBasic(string name, DbTable table)
        {
            IBasicDbQuery query = new BasicDbQuery(name, DbQueryKind.Select, table);

            return query;
        }

        /// <summary>
        /// Creates a new Select basic database query.
        /// </summary>
        /// <returns>Returns a new Select basic database query</returns>
        public static IBasicDbQuery SelectBasic(DbTable table)
            => SelectBasic(null, table);

        // Update --------------------------------

        /// <summary>
        /// Creates a new Update basic database query.
        /// </summary>
        /// <returns>Returns a new Update basic database query</returns>
        public static IBasicDbQuery UpdateBasic(string name, DbTable table)
        {
            IBasicDbQuery query = new BasicDbQuery(name, DbQueryKind.Update, table);

            return query;
        }

        /// <summary>
        /// Creates a new Update basic database query.
        /// </summary>
        /// <returns>Returns a new Update basic database query</returns>
        public static IBasicDbQuery UpdateBasic(DbTable table)
            => UpdateBasic(null, table);
    }
}
