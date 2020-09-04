using BindOpen.Extensions.Runtime;

namespace BindOpen.Data.Expression
{
    /// <summary>
    /// This class represents a data expression that can contain a literal and script texts.
    /// </summary>
    public static class DataExpressionFactory
    {
        /// <summary>
        /// Creates the expression.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <param name="kind">The kind of expression to consider.</param>
        /// <returns>Returns the created expression.</returns>
        public static DataExpression CreateExp(DataExpressionKind kind, string text = null)
            => new DataExpression()
            {
                Kind = kind,
                Text = text
            };

        /// <summary>
        /// Creates a new literal expression into auto mode.
        /// </summary>
        /// <param name="text">The script text to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static DataExpression CreateExpAsAuto(this string text)
            => CreateExp(DataExpressionKind.Auto, text);

        /// <summary>
        /// Creates a new script expression.
        /// </summary>
        /// <param name="text">The script text to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static DataExpression CreateExpAsScript(this string text)
            => CreateExp(DataExpressionKind.Script, text);

        /// <summary>
        /// Creates a new literal expression.
        /// </summary>
        /// <param name="text">The literal text to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static DataExpression CreateExpAsLiteral(this string text)
            => CreateExp(DataExpressionKind.Literal, text);

        /// <summary>
        /// Creates a new word expression.
        /// </summary>
        /// <param name="scriptword">The script word to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static DataExpression CreateExp(this IBdoScriptword scriptword)
        {
            var expression = CreateExp(DataExpressionKind.Word);
            expression.Word = scriptword as BdoScriptword;
            return expression;
        }
    }
}