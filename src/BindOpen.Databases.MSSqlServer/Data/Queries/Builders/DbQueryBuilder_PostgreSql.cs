using BindOpen.Application.Scopes;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    internal partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryBuilder_MSSqlServer class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        public DbQueryBuilder_MSSqlServer(
            IBdoScope scope = null)
            : base(scope)
        {
            Id = "databases.postgresql$client";
        }

        #endregion
    }
}