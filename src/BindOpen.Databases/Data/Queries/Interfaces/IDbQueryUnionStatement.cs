namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This interface defines the statement data query union.
    /// </summary>
    public interface IDbQueryUnionStatement
    {
        /// <summary>
        /// The type.
        /// </summary>
        DbQueryUnionKind Type { get; set; }

        /// <summary>
        /// The query.
        /// </summary>
        IAdvancedDbQuery Query { get; set; }
    }
}