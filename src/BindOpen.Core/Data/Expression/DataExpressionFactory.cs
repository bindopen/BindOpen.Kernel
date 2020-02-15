namespace BindOpen.Data.Expression
{
    /// <summary>
    /// This class represents a data expression that can contain a literal and script texts.
    /// </summary>
    public static class DataExpressionFactory
    {
        /// <summary>
        /// Creates a new script expression.
        /// </summary>
        /// <param name="text">The script text to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static DataExpression CreateScript(this string text)
        {
            return new DataExpression() {
                Text = text,
                Kind = DataExpressionKind.Script
            };
        }

        /// <summary>
        /// Creates a new literal expression.
        /// </summary>
        /// <param name="text">The literal text to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static DataExpression CreateLiteral(this string text)
        {
            return new DataExpression() {
                Text = text,
                Kind = DataExpressionKind.Literal
            };
        }
    }
}