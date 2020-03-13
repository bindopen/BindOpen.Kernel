using BindOpen.Application.Scopes;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    internal partial class DbQueryBuilder_PostgreSql : DbQueryBuilder
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryBuilder_PostgreSql class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        public DbQueryBuilder_PostgreSql(
            IBdoScope scope = null)
            : base(scope)
        {
            Id = "databases.postgresql$client";
        }

        #endregion
    }
}