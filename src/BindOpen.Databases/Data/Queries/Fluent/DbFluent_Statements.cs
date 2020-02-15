using BindOpen.Data.Common;
using BindOpen.Data.Expression;
using BindOpen.Extensions.Carriers;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a factory of basic database query statement.
    /// </summary>
    public static partial class DbFluent
    {
        // From --------------------------------

        /// <summary>
        /// Creates a new From statement.
        /// </summary>
        /// <param name="table">The table to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public static IDbQueryFromStatement From(DbTable table = null)
            => new DbQueryFromStatement()
                .WithJoins(Join(DbQueryJoinKind.None, table));

        /// <summary>
        /// Creates a new From statement.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public static IDbQueryFromStatement From(IDbQuery query)
            => new DbQueryFromStatement()
                .WithJoins(Join(DbQueryJoinKind.None, query));

        // Join --------------------------------


        // Table

        /// <summary>
        /// Creates a new Join statement.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table">The table to consider.</param>
        /// <param name="conditionScript">The condition script to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public static IDbQueryJoinStatement Join(DbQueryJoinKind kind, DbTable table, string conditionScript = null)
            => new DbQueryJoinStatement() { Kind = kind, Table = table }.WithCondition(conditionScript.CreateScript());

        /// <summary>
        /// Creates a new Join statement.
        /// </summary>
        /// <param name="table">The table to consider.</param>
        /// <param name="conditionScript">The condition script to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public static IDbQueryJoinStatement Join(DbTable table, string conditionScript = null)
            => Join(DbQueryJoinKind.Inner, table, conditionScript);

        // Query

        /// <summary>
        /// Creates a new Join statement.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="query">The query to consider.</param>
        /// <param name="conditionScript">The condition script to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public static IDbQueryJoinStatement Join(DbQueryJoinKind kind, IDbQuery query, string conditionScript = null)
            => new DbQueryJoinStatement(kind, query).WithCondition(conditionScript.CreateScript());

        /// <summary>
        /// Creates a new Join statement.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="conditionScript">The condition script to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public static IDbQueryJoinStatement Join(IDbQuery query, string conditionScript = null)
            => Join(DbQueryJoinKind.Inner, query).WithCondition(conditionScript.CreateScript());

        // Join condition

        /// <summary>
        /// Creates a new instance of the DbQueryJoinCondition class.
        /// </summary>
        /// <param name="field1">The field 1 to consider.</param>
        /// <param name="field2">The field 2 to consider.</param>
        /// <param name="op">The operation to consider.</param>
        public static DbQueryJoinCondition JoinCondition(DbField field1, DbField field2, DataOperator op = DataOperator.Equal)
        {
            return new DbQueryJoinCondition()
            {
                Field1 = field1,
                Field2 = field2,
                Operator = op
            };
        }
    }
}
