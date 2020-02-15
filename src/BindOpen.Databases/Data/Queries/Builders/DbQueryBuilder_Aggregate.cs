using System;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public abstract partial class DbQueryBuilder
    {
        // Aggregate

        /// <summary>
        /// Evaluates the script word $SQLCOUNT.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual string GetSqlText_Count(object[] parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Evaluates the script word $SQLCOUNT.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_TextCount(params object[] parameters)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLSUM.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_Sum(params object[] parameters)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLAVG.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_Average(params object[] parameters)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLIN.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_In(params object[] parameters)
        {
            return "";
        }
    }
}