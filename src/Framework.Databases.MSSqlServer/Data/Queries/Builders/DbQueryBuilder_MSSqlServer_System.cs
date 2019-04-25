using System;
using BindOpen.Framework.Databases.Data.Queries.Builders;

namespace BindOpen.Framework.Databases.MSSqlServer.Data.Queries.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // System

        /// <summary>
        /// Evaluates the script word $SQLNEWGUID.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_NewGuid()
        {
            return "newid()";
        }

        /// <summary>
        /// Evaluates the script word $SQLRANDOM.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Random()
        {
            return "newid()";
       }
    }
}