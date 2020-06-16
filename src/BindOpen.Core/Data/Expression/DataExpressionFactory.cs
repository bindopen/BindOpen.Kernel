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
        public static DataExpression Create(DataExpressionKind kind, string text = null)
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
        public static DataExpression CreateAuto(this string text)
            => Create(DataExpressionKind.Auto, text);

        /// <summary>
        /// Creates a new script expression.
        /// </summary>
        /// <param name="text">The script text to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static DataExpression CreateScript(this string text)
            => Create(DataExpressionKind.Script, text);

        /// <summary>
        /// Creates a new literal expression.
        /// </summary>
        /// <param name="text">The literal text to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static DataExpression CreateLiteral(this string text)
            => Create(DataExpressionKind.Literal, text);

        /// <summary>
        /// Creates a new word expression.
        /// </summary>
        /// <param name="scriptword">The script word to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static DataExpression CreateExpression(this BdoScriptword scriptword)
        {
            var expression = Create(DataExpressionKind.Word);
            expression.Word = scriptword;
            return expression;
        }
    }
}