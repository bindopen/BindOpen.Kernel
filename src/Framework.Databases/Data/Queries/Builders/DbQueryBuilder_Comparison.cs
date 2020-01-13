namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public abstract partial class DbQueryBuilder
    {
        // Comparison

        /// <summary>
        /// Evaluates the script word $SQLEQ.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_Equal(string value1, string value2)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLDIFF.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_NotEqual(string value1, string value2)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLGT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_Greater(string value1, string value2)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLGTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_GreaterOrEqual(string value1, string value2)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLLT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_Less(string value1, string value2)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLLTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_LessOrEqual(string value1, string value2)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLISNULL.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_IsNull(string value1)
        {
            return "";
        }
    }
}