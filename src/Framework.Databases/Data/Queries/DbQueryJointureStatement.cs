using BindOpen.Framework.Data.Expression;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents the Jointure statement of a database data query.
    /// </summary>
    public class DbQueryJointureStatement : IDbQueryJointureStatement
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The kind of jointure of this instance.
        /// </summary>
        public DbQueryJointureKind Kind { get; set; }

        /// <summary>
        /// The data module of this instance.
        /// </summary>
        public DbTable Table { get; set; }

        /// <summary>
        /// The condition of this instance.
        /// </summary>
        public DataExpression Condition { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryJointureStatement class.
        /// </summary>
        public DbQueryJointureStatement()
        {
        }

        #endregion
    }
}