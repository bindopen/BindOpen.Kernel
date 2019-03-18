using System;
using BindOpen.Framework.Databases.Data.Queries.Builders;

namespace BindOpen.Framework.Databases.MSSqlServer.Data.Queries.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // Date and time

        /// <summary>
        /// Evaluates the script word $SQLGETCURRENTDATE.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_CurrentDate(object[] parameters)
        {
            return "getdate()";
        }
    }
}