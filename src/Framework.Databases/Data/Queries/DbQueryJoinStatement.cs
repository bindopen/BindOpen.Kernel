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
        /// The query of this instance.
        /// </summary>
        public DbQuery Query { get; set; }

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

        /// <summary>
        /// Instantiates a new instance of the DbQueryJoinStatement class.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="query">The query to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public DbQueryJoinStatement(DbQueryJoinKind kind, IDbQuery query)
        {
            Kind = kind;
            Query = query as DbQuery;
        }

        /// <summary>
        /// Instantiates a new instance of the DbQueryJoinStatement class.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table">The table to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public DbQueryJoinStatement(DbQueryJoinKind kind, DbTable table)
        {
            Kind = kind;
            Table = table;
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