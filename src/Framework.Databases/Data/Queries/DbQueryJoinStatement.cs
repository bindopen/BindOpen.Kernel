using BindOpen.Framework.Data.Expression;
using BindOpen.Framework.Extensions.Carriers;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents the Join statement of a database data query.
    /// </summary>
    public class DbQueryJoinStatement : IDbQueryJoinStatement
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The kind of join of this instance.
        /// </summary>
        public DbQueryJoinKind Kind { get; set; }

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
        /// Instantiates a new instance of the DbQueryJoinStatement class.
        /// </summary>
        public DbQueryJoinStatement()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Sets the specified condition.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbQueryJoinStatement WithCondition(DataExpression condition)
        {
            Condition = condition;

            return this;
        }

        /// <summary>
        /// Sets the specified condition.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbQueryJoinStatement WithCondition(string condition)
            => WithCondition(condition?.CreateScript());

        #endregion
    }
}