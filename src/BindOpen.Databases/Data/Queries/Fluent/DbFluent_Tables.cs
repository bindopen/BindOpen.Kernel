using BindOpen.Data.Expression;
using BindOpen.Extensions.Carriers;
using System.Linq;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data table.
    /// </summary>
    public static partial class DbFluent
    {
        /// <summary>
        /// Creates a new instance of the DbTable class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModuleName">The name of the data module to consider.</param>
        public static DbTable Table(string name = null, string schema = null, string dataModuleName = null)
            => new DbTable()
            {
                Name = name,
                DataModule = dataModuleName,
                Schema = schema,
            };

        // Derived --------------------------------

        /// <summary>
        /// Creates a new instance of the DbDerivedTable class.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        public static DbDerivedTable Table(IDbQuery query)
            => new DbDerivedTable() { Query = query as DbQuery };

        // Tupled --------------------------------

        /// <summary>
        /// Creates a new instance of the DbTupledTable class.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        public static DbTupledTable Table(params IDbTuple[] tuples)
            => new DbTupledTable(tuples?.Cast<DbTuple>().ToArray());

        // Join --------------------------------

        /// <summary>
        /// Creates a new joined table.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table">The table to consider.</param>
        /// <param name="conditionScript">The condition script to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public static DbJoinedTable Table(DbQueryJoinKind kind, DbTable table, string conditionScript = null)
            => new DbJoinedTable() { Kind = kind, Table = table }.WithCondition(conditionScript.CreateScript());

        /// <summary>
        /// Creates a new joined table.
        /// </summary>
        /// <param name="table">The table to consider.</param>
        /// <param name="conditionScript">The condition script to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public static DbJoinedTable Table(DbTable table, string conditionScript = null)
            => Table(DbQueryJoinKind.Inner, table, conditionScript);
    }
}
