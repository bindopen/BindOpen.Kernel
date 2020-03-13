using BindOpen.Data.Expression;
using BindOpen.Extensions.Carriers;
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
        /// The tables of this instance.
        /// </summary>
        public List<DbTable> Tables { get; set; }

        /// <summary>
        /// The union clauses of this instance.
        /// </summary>
        public List<DbUnionTable> UnionTables { get; set; }

        /// <summary>
        /// Value of this instance.
        /// </summary>
        public DataExpression Value { get; set; }

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