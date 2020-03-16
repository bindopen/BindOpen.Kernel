using BindOpen.Data.Expression;
using BindOpen.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
{

    /// <summary>
    /// This class represents the GroupBy clause of a database data query.
    /// </summary>
    public class DbQueryGroupByClause : IDbQueryGroupByClause
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Fields of this instance.
        /// </summary>
        public List<DbField> Fields { get; set; }

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
        /// Instantiates a new instance of the DbQueryGroupByClause class.
        /// </summary>
        public DbQueryGroupByClause()
        {
        }

        #endregion
    }
}