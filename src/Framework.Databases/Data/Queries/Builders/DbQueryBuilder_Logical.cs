using System;

namespace BindOpen.Framework.Databases.Data.Queries.Builders
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
        /// <param name="valu2"></param>
        /// <returns>The interpreted string value.</returns>
        public virtual String GetSqlText_If(string condition, string value1, string value2)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLNOT.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public virtual String GetSqlText_Not(string value1)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLOR.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public virtual String GetSqlText_Or(object[] parameters)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLAND.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public virtual String GetSqlText_And(object[] parameters)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLXOR.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public virtual String GetSqlText_Xor(object[] parameters)
        {
            return "";
        }
    }
}