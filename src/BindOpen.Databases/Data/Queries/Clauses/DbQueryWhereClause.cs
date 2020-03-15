using BindOpen.Data.Expression;
using BindOpen.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents the Where clause of a database data query.
    /// </summary>
    public class DbQueryWhereClause : IDbQueryWhereClause
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public List<DbField> IdFields { get; set; }

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
        /// Instantiates a new instance of the DbQueryWhereClause class.
        /// </summary>
        public DbQueryWhereClause()
        {
        }

        #endregion
    }
}