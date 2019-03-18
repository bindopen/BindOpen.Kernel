using BindOpen.Framework.Core.Data.Expression;

namespace BindOpen.Framework.Databases.Data.Queries
{

    /// <summary>
    /// This class represents the Having statement of a database data query.
    /// </summary>
    public class DbDataQueryHavingStatement
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private DataExpression _DataExpression;

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The data expression of this instance.
        /// </summary>
        public DataExpression DataExpression
        {
            get { return this._DataExpression; }
            set { this._DataExpression = value; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbDataQueryHavingStatement class.
        /// </summary>
        public DbDataQueryHavingStatement()
        {
        }

        #endregion

    }
}