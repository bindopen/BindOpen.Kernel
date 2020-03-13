namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    internal partial class DbQueryBuilder_PostgreSql
    {
        // Conversion

        /// <summary>
        /// Evaluates the script word $SQLCONVERTTOTEXT.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_ConvertToText(string value1)
        {
            return "convert(varchar," + value1 + ")";
        }
    }
}