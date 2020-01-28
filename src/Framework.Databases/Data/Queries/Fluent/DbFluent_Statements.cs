using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Data.Expression;
using BindOpen.Framework.Extensions.Carriers;

namespace BindOpen.Framework.Data.Queries
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
                .Join(Join(DbQueryJoinKind.None, table));

        // Join --------------------------------

        /// <summary>
        /// Creates a new Join statement.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table">The table to consider.</param>
        /// <param name="conditionScript">The condition script to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public static IDbQueryJoinStatement Join(DbQueryJoinKind kind, DbTable table, string conditionScript)
            => Join(kind, table).WithCondition(conditionScript.CreateScript());

        /// <summary>
        /// Creates a new Join statement.
        /// </summary>
        /// <param name="table">The table to consider.</param>
        /// <param name="conditionScript">The condition script to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public static IDbQueryJoinStatement Join(DbTable table, string conditionScript)
            => Join(DbQueryJoinKind.Inner, table).WithCondition(conditionScript.CreateScript());

        /// <summary>
        /// Creates a new Join statement.
        /// </summary>
        /// <param name="table">The table to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public static IDbQueryJoinStatement Join(DbTable table)
            => Join(DbQueryJoinKind.Inner, table);

        /// <summary>
        /// Creates a new Join statement.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table">The table to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public static IDbQueryJoinStatement Join(DbQueryJoinKind kind, DbTable table)
            => new DbQueryJoinStatement() { Kind = kind, Table = table };

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
