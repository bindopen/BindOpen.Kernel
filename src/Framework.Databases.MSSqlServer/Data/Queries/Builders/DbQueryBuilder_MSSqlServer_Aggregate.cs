using System;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Queries.Builders;

namespace BindOpen.Framework.Databases.MSSqlServer.Data.Queries.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // Aggregate

        /// <summary>
        /// Evaluates the script word $SQLCOUNT.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_TextCount(params Object[] parameters)
        {
            string text = "count(";
            foreach (Object object1 in parameters)
            {
                if (object1 != null)
                {
                    string st = object1.ToString();
                    text += st.GetValueFromText() + (text == "count(" ? "," : "");
                }
            }

            text += ")";

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLSUM.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_Sum(params Object[] parameters)
        {
            string text = "sum(";
            foreach (Object object1 in parameters)
            {
                if (object1 != null)
                {
                    string st = object1.ToString();
                    text += st.GetValueFromText() + (text == "sum(" ? "," : "");
                }
            }

            text += ")";

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLAVG.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_Average(params Object[] parameters)
        {
            string text = "avg(";
            foreach (Object object1 in parameters)
            {
                if (object1 != null)
                {
                    string st = object1.ToString();
                    text += st.GetValueFromText() + (text == "avg(" ? "," : "");
                }
            }

            text += ")";

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLIN.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override String GetSqlText_In(params Object[] parameters)
        {
            string text = "[";
            foreach (Object object1 in parameters)
            {
                if (object1 != null)
                {
                    string st = object1.ToString();
                    text += "'" + st.GetValueFromText() + "'" + (text == "[" ? "," : "");
                }
            }

            text += "]";

            return text;
        }

    }
}