using BindOpen.Extensions.Scripting;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data expression that can contain a literal and script texts.
    /// </summary>
    public static partial class BdoItems
    {
        /// <summary>
        /// Creates the expression.
        /// </summary>
        /// <param name="kind">The kind of expression to consider.</param>
        /// <param name="text">The text to consider.</param>
        /// <returns>Returns the created expression.</returns>
        public static BdoExpression NewExpression(
            BdoExpressionKind kind,
            string text = null)
            => new()
            {
                Kind = kind,
                Text = text
            };

        /// <summary>
        /// Creates the expression.
        /// </summary>
        /// <param name="word">The word of expression to consider.</param>
        /// <returns>Returns the created expression.</returns>
        public static BdoExpression NewExpression(
            IBdoScriptword word)
            => new()
            {
                Kind = BdoExpressionKind.Word,
                Word = word
            };
    }
}