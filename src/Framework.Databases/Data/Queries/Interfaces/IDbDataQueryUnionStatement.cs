namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This interface defines the statement data query union.
    /// </summary>
    public interface IDbDataQueryUnionStatement
    {
        /// <summary>
        /// The type.
        /// </summary>
        DbDataQueryUnionKind Type { get; set; }

        /// <summary>
        /// The query.
        /// </summary>
        IAdvancedDbDataQuery Query { get; set; }
    }
}