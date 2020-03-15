namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    internal partial class DbQueryBuilder_PostgreSql : DbQueryBuilder
    {
        // Logical

        /// <summary>
        /// Evaluates the script word $SQLIF.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_If(string condition, string value1, string value2)
        {
            return "case when (" + condition + ") then " + value1 + " else " + value2 + " end";
        }

        /// <summary>
        /// Evaluates the script word $SQLNOT.
        /// </summary>
        /// <param name="value1">The value 1 to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Not(string value1)
        {
            return "not (" + value1 + ")";
        }

        /// <summary>
        /// Evaluates the script word $SQLOR.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Or(object[] parameters)
        {
            string text = "(";
            if (parameters.Length == 1)
            {
                text += parameters[0];
            }
            else
            {
                text += string.Join(" or ", parameters);
            }

            text += ")";

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLAND.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_And(object[] parameters)
        {
            string text = "(";
            if (parameters.Length == 1)
            {
                text += parameters[0];
            }
            else
            {
                text += string.Join(" and ", parameters);
            }

            text += ")";

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLXOR.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Xor(object[] parameters)
        {
            string text = "(";
            if (parameters.Length == 1)
            {
                text += parameters[0];
            }
            else
            {
                text += string.Join(" xor ", parameters);
            }

            text += ")";

            return text;
        }
    }
}