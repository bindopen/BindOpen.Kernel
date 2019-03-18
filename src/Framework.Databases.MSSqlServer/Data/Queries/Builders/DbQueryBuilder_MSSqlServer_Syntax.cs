using System;
using BindOpen.Framework.Databases.Data.Queries.Builders;

namespace BindOpen.Framework.Databases.MSSqlServer.Data.Queries.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // Syntax

        /// <summary>
        /// Evaluates the script word $SQLNULL.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_Null()
        {
            return "NULL";
        }

        /// <summary>
        /// Evaluates the script word $SQLTRUE.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_True()
        {
            return "1";
        }

        /// <summary>
        /// Evaluates the script word %SQLDATABASE.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_Database(string name)
        {
            return "[" + name + "]";
        }

        /// <summary>
        /// Evaluates the script word %SQLSCHEMA.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_Schema(
            string name,
            string location = null)
        {
            return (!String.IsNullOrEmpty(location) ? location + "." : "") +
                "[" + name + "]";
        }

        /// <summary>
        /// Evaluates the script word [%DATABASE->]%TABLE.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_Table(
            string name,
            string location = null)
        {
            return (!String.IsNullOrEmpty(location) ? location + "." : "") +
                "[" + name + "]";
        }

        /// <summary>
        /// Evaluates the script word [[%DATABASE->]%TABLE->]%FIELD.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_Field(
            string name,
            string location = null)
        {
            return (!String.IsNullOrEmpty(location) ? location + "." : "") +
                "[" + name + "]";
        }

        /// <summary>
        /// Evaluates the script word $SQLLIST.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_List(params Object[] parameters)
        {
            return "";
        }
    }
}