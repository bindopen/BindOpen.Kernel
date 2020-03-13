using BindOpen.Data.Expression;
using BindOpen.Extensions.Carriers;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents the Join table of a database data query.
    /// </summary>
    public class DbJoinedTable : DbTable
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
        /// The table of this instance.
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
        /// Instantiates a new instance of the DbQueryJoinedTable class.
        /// </summary>
        public DbJoinedTable()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DbQueryJoinedTable class.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table">The table to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public DbJoinedTable(DbQueryJoinKind kind, DbTable table)
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
        public DbJoinedTable WithCondition(DataExpression condition)
        {
            Condition = condition;

            return this;
        }

        /// <summary>
        /// Sets the specified condition.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <returns>Returns this instance.</returns>
        public DbJoinedTable WithCondition(string condition)
            => WithCondition(condition?.CreateScript());

        #endregion
    }
}