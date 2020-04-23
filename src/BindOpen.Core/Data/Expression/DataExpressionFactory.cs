namespace BindOpen.Data.Expression
{
    /// <summary>
    /// This class represents a data expression that can contain a literal and script texts.
    /// </summary>
    public static class DataExpressionFactory
    {
        static DataExpression CreateExpression(DataExpressionKind kind, string text)
        {
            var expression = new DataExpression()
            {
                Kind = kind,                
                Text = text
            };

            return expression;
        }

        /// <summary>
        /// Creates a new literal expression into auto mode.
        /// </summary>
        /// <param name="text">The script text to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static DataExpression CreateAuto(this string text)
            => CreateExpression(DataExpressionKind.Auto, text);

        /// <summary>
        /// Creates a new script expression.
        /// </summary>
        /// <param name="text">The script text to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static DataExpression CreateScript(this string text)
            => CreateExpression(DataExpressionKind.Script, text);

        /// <summary>
        /// Creates a new literal expression.
        /// </summary>
        /// <param name="text">The literal text to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static DataExpression CreateLiteral(this string text)
            => CreateExpression(DataExpressionKind.Literal, text);
    }
}