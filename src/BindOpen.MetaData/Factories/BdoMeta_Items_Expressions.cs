using BindOpen.Extensions.Scripting;
using BindOpen.Meta.Items;

namespace BindOpen.Meta
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param name="kind">The kind of exp to consider.</param>
        /// <param name="text">The text to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoExpression NewExpression(
            string text,
            BdoExpressionKind kind = BdoExpressionKind.Auto)
            => new()
            {
                Kind = kind,
                Text = text
            };

        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param name="word">The word of exp to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoExpression NewExpression(
            IBdoScriptword word)
            => new()
            {
                Kind = BdoExpressionKind.Word,
                Word = word
            };
    }
}