using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Strings;
using System;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFluent
    {
        /// <summary>
        /// Creates a BDO script representing the current date in SQL.
        /// </summary>
        public static DataExpression CurrentDate()
            => "$sqlGetCurrentDate()".CreateScript();

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter to consider.</param>
        public static DataExpression Text(string param1)
            => ("$sqlText(" + param1 + ")").CreateScript();

        /// <summary>
        /// Creates a BDO script representing a value.
        /// </summary>
        /// <param name="param1">The parameter to consider.</param>
        public static DataExpression Value(object param1)
        {
            if (param1 is DataExpression param1DataExpression)
            {
                if (param1DataExpression.Kind == DataExpressionKind.Auto
                    && param1DataExpression.Text?.StartsWith("{{") == true
                    && param1DataExpression.Text?.EndsWith("}}") == true)
                {

                    var text = param1DataExpression.Text.Substring(2);
                    return (text.Substring(0, text.Length - 2)).CreateScript();
                }
                return param1DataExpression;
            }
            else if (param1 is string param1String)
            {
                return Text(param1String);
            }
            else if (param1 is DateTime param1DateTime)
            {
                return Text(param1DateTime.ToString(StringHelper.__DateFormat));
            }

            return (param1?.ToString()).CreateScript();
        }
    }
}
