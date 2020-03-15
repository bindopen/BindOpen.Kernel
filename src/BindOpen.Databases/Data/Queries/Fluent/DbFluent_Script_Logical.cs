using BindOpen.Data.Expression;
using System.Linq;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFluent
    {
        // Condition --------------------------------

        /// <summary>
        /// Creates a BDO script representing and Sql And condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static DataExpression And(params object[] conditions)
        {
            var query = "$sqlAnd(";

            if (conditions.Length > 0)
            {
                query += string.Join(",", conditions.Select(p => Value(p)));
            }

            query += ")";

            return query.CreateScript(); ;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Or condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static DataExpression Or(params object[] conditions)
        {
            var query = "$sqlOr(";

            if (conditions.Length > 0)
            {
                query += string.Join(",", conditions.Select(p => Value(p)));
            }

            query += ")";

            return query.CreateScript(); ;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Xor condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static DataExpression Xor(params object[] conditions)
        {
            var query = "$sqlXor(";

            if (conditions.Length > 0)
            {
                query += string.Join(",", conditions.Select(p => Value(p)));
            }

            query += ")";

            return query.CreateScript(); ;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Not condition including the specified condition strings.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        public static DataExpression Not(string condition)
        {
            return ("$sqlNot(" + condition + ")").CreateScript();
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Xor condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static DataExpression Like(string param1, string param2)
            => ("$sqlLike(" + param1 + ", " + param2 + ")").CreateScript();
    }
}
