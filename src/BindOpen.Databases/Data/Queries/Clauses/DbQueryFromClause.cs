using BindOpen.Data.Expression;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents the From clause of a database data query.
    /// </summary>
    public class DbQueryFromClause : IDbQueryFromClause
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The statements of this instance.
        /// </summary>
        public List<DbQueryFromStatement> Statements { get; set; }

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
        /// Instantiates a new instance of the DbQueryFromClause class.
        /// </summary>
        public DbQueryFromClause()
        {
        }

        #endregion
    }
}