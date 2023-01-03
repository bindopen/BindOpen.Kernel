using BindOpen.Extensions.Scripting;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data expression that can contain a literal and script texts.
    /// </summary>
    public static class BdoExpressionExtension
    {
        /// <summary>
        /// Creates a new literal expression into auto mode.
        /// </summary>
        /// <param name="text">The script text to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static BdoExpression AsExpression(
            this string text,
            BdoExpressionKind kind = BdoExpressionKind.Auto)
        {
            var exp = BdoItems.NewExpression(kind);
            exp.Text = text;
            return exp;
        }

        /// <summary>
        /// Creates a new word expression.
        /// </summary>
        /// <param name="scriptword">The script word to consider.</param>
        /// <returns>Returns the script expression.</returns>
        public static BdoExpression AsExpression(
            this IBdoScriptword scriptword)
        {
            var expression = BdoItems.NewExpression(BdoExpressionKind.Word);
            expression.Word = scriptword;
            return expression;
        }
    }
}