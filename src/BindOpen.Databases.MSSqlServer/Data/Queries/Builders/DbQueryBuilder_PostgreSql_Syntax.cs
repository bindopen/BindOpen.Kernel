using BindOpen.Data.Queries;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    internal partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // Syntax

        /// <summary>
        /// Evaluates the script word $SQLNULL.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Null()
        {
            return "NULL";
        }

        /// <summary>
        /// Evaluates the script word $SQLTRUE.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_True()
        {
            return "1";
        }

        /// <summary>
        /// Evaluates the script word %SQLDATABASE.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Database(string name)
        {
            return "\"" + name?.Replace("\"", "'\"\"") + "\"";
        }

        /// <summary>
        /// Evaluates the script word %SQLSCHEMA.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Schema(
            string name,
            string location = null)
        {
            return (!string.IsNullOrEmpty(location) ? location + "." : "") + "\"" + name?.Replace("\"", "'\"\"") + "\"";
        }

        /// <summary>
        /// Evaluates the script word [%DATABASE->]%TABLE.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Table(
            string name,
            string location = null)
        {
            return (!string.IsNullOrEmpty(location) ? location + "." : "") + "\"" + name?.Replace("\"", "'\"\"") + "\"";
        }

        /// <summary>
        /// Evaluates the script word [[%DATABASE->]%TABLE->]%FIELD.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Field(
            string name,
            string location = null)
        {
            return (!string.IsNullOrEmpty(location) ? location + "." : "") + "\"" + name?.Replace("\"", "'\"\"") + "\"";
        }

        /// <summary>
        /// Evaluates the script word $SQLLIST.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_List(params object[] parameters)
        {
            return "";
        }
    }
}