using System;
using BindOpen.Framework.Databases.Data.Queries.Builders;

namespace BindOpen.Framework.Databases.MSSqlServer.Data.Queries.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // Logical

        /// <summary>
        /// Evaluates the script word $SQLIF.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="value1"></param>
        /// <param name="valu2"></param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_If(string condition, string value1, string value2)
        {
            return "CASE WHEN (" + condition + ") THEN " + value1 + " ELSE " + value2 + " END";
        }

        /// <summary>
        /// Evaluates the script word $SQLNOT.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_Not(string value1)
        {
            return "NOT (" + value1 + ")";
        }

        /// <summary>
        /// Evaluates the script word $SQLOR.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_Or(object[] parameters)
        {
            string text = "(";
            foreach (Object object1 in parameters)
            {
                if (object1 != null)
                {
                    string st = object1.ToString();
                    text += (text == "(" ? st : " OR " + st);
                }
            }

            text += ")";

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLAND.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_And(object[] parameters)
        {
            string text = "(";
            foreach (Object object1 in parameters)
            {
                if (object1 != null)
                {
                    string st = object1.ToString();
                    text += (text == "(" ? st : " AND " + st);
                }
            }

            text += ")";

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLXOR.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_Xor(object[] parameters)
        {
            string text = "(";
            foreach (Object object1 in parameters)
            {
                if (object1 != null)
                {
                    string st = object1.ToString();
                    text += (text == "(" ? st : " XOR " + st);
                }
            }

            text += ")";

            return text;
        }
    }
}