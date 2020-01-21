namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFactory
    {
        /// <summary>
        /// Creates a BDO script representing and Sql And condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static string CreateAndCondition(params string[] conditions)
        {
            string query = "$sqlAnd(";

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
        public static string CreateOrCondition(params string[] conditions)
        {
            string query = "$sqlOr(";

            if (conditions.Length > 0)
            {
                query += string.Join(",", conditions);
            }

            query += ")";

            return query;
        }
    }
}
