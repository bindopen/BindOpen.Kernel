using System;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public abstract partial class DbQueryBuilder
    {
        // Date and time

        /// <summary>
        /// Evaluates the script word $SQLGETCURRENTDATE.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public virtual string GetSqlText_CurrentDate(object[] parameters)
        {
            return "";
        }
    }
}