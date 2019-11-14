using BindOpen.Framework.Databases.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Framework.Databases.Data.Queries.Factories
{
    /// <summary>
    /// This class represents a factory pf basic database query.
    /// </summary>
    public static class DbQueryFactory
    {
        /// <summary>
        /// Creates a new Delete basic database query.
        /// </summary>
        /// <returns>Returns a new Delete basic database query</returns>
        public static StoredProcedureDbQuery CreateStoredProcedure(
            string dataModule = null,
            string schema = null,
            string storedProcedureName = null)
            => new StoredProcedureDbQuery(dataModule, schema, storedProcedureName);

        /// <summary>
        /// Creates a new Delete basic database query.
        /// </summary>
        /// <returns>Returns a new Delete basic database query</returns>
        public static BasicDbQuery CreateBasicDelete(DbTable table, List<DbField> idFields)
            => new BasicDbQuery(DbQueryKind.Select, table?.DataModule, table?.Schema, table?.Name) { IdFields = idFields };

        /// <summary>
        /// Creates a new Drop basic database query.
        /// </summary>
        /// <returns>Returns a new Drop basic database query</returns>
        public static BasicDbQuery CreateBasicDrop(DbTable table)
            => new BasicDbQuery(DbQueryKind.Select, table?.DataModule, table?.Schema, table?.Name);

        /// <summary>
        /// Creates a new Duplicate basic database query.
        /// </summary>
        /// <returns>Returns a new Duplicate basic database query</returns>
        public static BasicDbQuery CreateBasicDuplicate(DbTable table, List<DbField> fields, List<DbField> idFields)
            => new BasicDbQuery(DbQueryKind.Duplicate, table?.DataModule, table?.Schema, table?.Name) { Fields = fields, IdFields = idFields };

        /// <summary>
        /// Creates a new Insert basic database query.
        /// </summary>
        /// <returns>Returns a new Insert basic database query</returns>
        public static BasicDbQuery CreateBasicInsert(DbTable table, List<DbField> fields)
            => new BasicDbQuery(DbQueryKind.Insert, table?.DataModule, table?.Schema, table?.Name) { Fields = fields };

        /// <summary>
        /// Creates a new Select basic database query.
        /// </summary>
        /// <returns>Returns a new Select basic database query</returns>
        public static BasicDbQuery CreateBasicSelect(DbTable table, List<DbField> fields, List<DbField> idFields)
            => new BasicDbQuery(DbQueryKind.Select, table?.DataModule, table?.Schema, table?.Name) { Fields = fields, IdFields = idFields };

        /// <summary>
        /// Creates a new Update basic database query.
        /// </summary>
        /// <returns>Returns a new Update basic database query</returns>
        public static BasicDbQuery CreateBasicUpdate(DbTable table, List<DbField> fields, List<DbField> idFields)
            => new BasicDbQuery(DbQueryKind.Update, table?.DataModule, table?.Schema, table?.Name) { Fields = fields, IdFields = idFields };
    }
}