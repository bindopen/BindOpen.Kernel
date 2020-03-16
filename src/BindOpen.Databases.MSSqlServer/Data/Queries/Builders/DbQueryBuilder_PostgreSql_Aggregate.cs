using BindOpen.System.Scripting;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    internal partial class DbQueryBuilder_MSSqlServer
    {
        // Aggregate

        /// <summary>
        /// Evaluates the script word $SQLCOUNT.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_TextCount(params object[] parameters)
        {
            string text = "count(";
            foreach (object object1 in parameters)
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
        public override string GetSqlText_Sum(params object[] parameters)
        {
            string text = "sum(";
            foreach (object object1 in parameters)
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
        public override string GetSqlText_Average(params object[] parameters)
        {
            string text = "avg(";
            foreach (object object1 in parameters)
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
        public override string GetSqlText_In(params object[] parameters)
        {
            string text = "[";
            foreach (object object1 in parameters)
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