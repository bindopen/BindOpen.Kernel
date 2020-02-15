namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public abstract partial class DbQueryBuilder
    {
        // System

        /// <summary>
        /// Evaluates the script word $SQLNEWGUID.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_NewGuid()
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLRANDOM.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_Random()
        {
            return "";
        }
    }
}