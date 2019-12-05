using System;

namespace BindOpen.Framework.Databases.Data.Queries.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public abstract partial class DbQueryBuilder
    {
        // String

        /// <summary>
        /// Evaluates the script word $SQLTEXT.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted string value.</returns>
        public virtual String GetSqlText_Text(string value1)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLLIKE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public virtual String GetSqlText_Like(string value1, string value2)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLREPLACE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <returns>The interpreted string value.</returns>
        public virtual String GetSqlText_Replace(string value1, string value2, string value3)
        {
            return "";
        }

        /// <summary>
        /// Evaluates the script word $SQLCONCATENATE.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public virtual String GetSqlText_Concatenate(object[] parameters)
        {
            return "";
        }
    }
}