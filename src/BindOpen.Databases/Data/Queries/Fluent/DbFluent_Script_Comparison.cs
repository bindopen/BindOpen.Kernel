using BindOpen.Data.Expression;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFluent
    {
        // Comparison

        /// <summary>
        /// Evaluates the script word $SQLEQ.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static DataExpression Eq(object value1, object value2)
            => ("$sqlEq(" + Value(value1) + ", " + Value(value2) + ")").CreateScript();


        /// <summary>
        /// Evaluates the script word $SQLDIFF.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static DataExpression Diff(object value1, object value2)
            => ("$sqlDiff(" + Value(value1) + ", " + Value(value2) + ")").CreateScript();


        /// <summary>
        /// Evaluates the script word $SQLGT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static DataExpression Gt(object value1, object value2)
            => ("$sqlGt(" + Value(value1) + ", " + Value(value2) + ")").CreateScript();

        /// <summary>
        /// Evaluates the script word $SQLGTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static DataExpression Gte(object value1, object value2)
            => ("$sqlGte(" + Value(value1) + ", " + Value(value2) + ")").CreateScript();

        /// <summary>
        /// Evaluates the script word $SQLLT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static DataExpression Lt(object value1, object value2)
            => ("$sqlLt(" + Value(value1) + ", " + Value(value2) + ")").CreateScript();

        /// <summary>
        /// Evaluates the script word $SQLLTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static DataExpression Lte(object value1, object value2)
            => ("$sqlLte(" + Value(value1) + ", " + Value(value2) + ")").CreateScript();

        /// <summary>
        /// Evaluates the script word $SQLISNULL.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted object value.</returns>
        public static DataExpression IsNull(object value1)
            => ("$sqlNull(" + Value(value1) + ")").CreateScript();
    }
}
