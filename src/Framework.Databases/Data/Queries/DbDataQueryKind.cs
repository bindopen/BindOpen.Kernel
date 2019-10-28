namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This enumeration lists the possible kinds of database data queries.
    /// </summary>
    public enum DbDataQueryKind
    {
        /// <summary>
        /// Create.
        /// </summary>
        Create,

        /// <summary>
        /// Select.
        /// </summary>
        Select,

        /// <summary>
        /// Update.
        /// </summary>
        Update,

        /// <summary>
        /// Delete.
        /// </summary>
        Delete,

        /// <summary>
        /// Insert.
        /// </summary>
        Insert,

        /// <summary>
        /// Duplicate.
        /// </summary>
        Duplicate,

        /// <summary>
        /// Drop.
        /// </summary>
        Drop
    }

}