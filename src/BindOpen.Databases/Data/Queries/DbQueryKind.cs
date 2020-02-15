namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This enumeration lists the possible kinds of database data queries.
    /// </summary>
    public enum DbQueryKind
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

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
        /// Upsert.
        /// </summary>
        Upsert,

        /// <summary>
        /// Drop.
        /// </summary>
        Drop
    }

}