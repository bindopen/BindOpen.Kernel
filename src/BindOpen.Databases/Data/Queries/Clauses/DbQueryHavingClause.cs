using BindOpen.Data.Expression;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents the Having clause of a database data query.
    /// </summary>
    public class DbQueryHavingClause : IDbQueryHavingClause
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Value of this instance.
        /// </summary>
        public DataExpression Expression { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryHavingClause class.
        /// </summary>
        public DbQueryHavingClause()
        {
        }

        #endregion
    }
}