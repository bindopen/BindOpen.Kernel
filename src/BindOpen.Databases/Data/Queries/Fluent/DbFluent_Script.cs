namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFluent
    {
        /// <summary>
        /// Creates a BDO script representing the current date in SQL.
        /// </summary>
        public static string CurrentDate()
            => "$SqlGetCurrentDate()";


        /// <summary>
        /// Creates a BDO script representing and Sql And condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static string And(params string[] conditions)
        {
            var query = "$sqlAnd(";

            if (conditions.Length > 0)
            {
                query += string.Join(",", conditions);
            }

            query += ")";

            return query;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Or condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static string Or(params string[] conditions)
        {
            var query = "$sqlOr(";

            if (conditions.Length > 0)
            {
                query += string.Join(",", conditions);
            }

            query += ")";

            return query;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Xor condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static string Xor(params string[] conditions)
        {
            var query = "$sqlXor(";

            if (conditions.Length > 0)
            {
                query += string.Join(",", conditions);
            }

            query += ")";

            return query;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Not condition including the specified condition strings.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        public static string Not(string condition)
        {
            var query = "$sqlNot(" + condition + ")";

            return query;
        }
    }
}
