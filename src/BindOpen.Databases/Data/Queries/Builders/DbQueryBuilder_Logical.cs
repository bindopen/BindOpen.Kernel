using System;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public abstract partial class DbQueryBuilder
    {
        // Logical

        /// <summary>
        /// Evaluates the script word $SQLIF.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_If(string condition, string value1, string value2)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLNOT.
        /// </summary>
        /// <param name="value1">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_Not(string value1)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLOR.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_Or(object[] parameters)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLAND.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_And(object[] parameters)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLXOR.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_Xor(object[] parameters)
        {
            return "";
        }
    }
}