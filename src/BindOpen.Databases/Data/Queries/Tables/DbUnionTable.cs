using BindOpen.Extensions.Carriers;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a union clause of a database data query.
    /// </summary>
    public class DbUnionTable : DbTable
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Type of this instance.
        /// </summary>
        public DbQueryUnionKind Kind { get; set; }

        /// <summary>
        /// Data query of this instance.
        /// </summary>
        public IDbSingleQuery Query { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryUnionClause class.
        /// </summary>
        public DbUnionTable()
        {
        }

        #endregion
    }
}