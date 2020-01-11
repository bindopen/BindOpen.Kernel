using BindOpen.Framework.Data.Expression;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents the Having statement of a database data query.
    /// </summary>
    public class DbQueryHavingStatement : IDbQueryHavingStatement
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The data expression of this instance.
        /// </summary>
        public IDataExpression DataExpression { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryHavingStatement class.
        /// </summary>
        public DbQueryHavingStatement()
        {
        }

        #endregion
    }
}