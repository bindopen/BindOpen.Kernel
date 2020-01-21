namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents an extension for join condition.
    /// </summary>
    public static class DbQueryJoinConditionExtension
    {
        /// <summary>
        /// Creates a new data expression condition from the specified join conditions.
        /// </summary>
        /// <param name="conditions">The join conditions to consider.</param>
        public static string ToString(this IDbQueryJoinCondition[] conditions)
        {
            string query = "";

            if (conditions.Length > 0)
            {
                foreach (var condition in conditions)
                {
                    query += condition?.ToString();
                }
                query = conditions.Length > 1 ? "$sqlAnd(" + query + ")" : query;
            }

            return query;
        }
    }
}
