
namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents a union statement of a database data query.
    /// </summary>
    public class DbQueryUnionStatement : IDbQueryUnionStatement
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Type of this instance.
        /// </summary>
        public DbQueryUnionKind Type { get; set; }

        /// <summary>
        /// Data query of this instance.
        /// </summary>
        public IAdvancedDbQuery Query { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryUnionStatement class.
        /// </summary>
        public DbQueryUnionStatement()
        {
        }

        #endregion
    }
}