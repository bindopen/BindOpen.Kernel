using BindOpen.Script;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    public static partial class BdoData
    {
        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="kind">The kind of exp to consider.</param>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoReference NewReference(
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
        /// <param key="word">The word of exp to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoReference NewReference(
            IBdoScriptword word)
            => new()
            {
                Word = word
            };

        // Alias

        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="kind">The kind of exp to consider.</param>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoReference NewRef(
            string text,
            BdoExpressionKind kind = BdoExpressionKind.Auto)
            => NewReference(text, kind);

        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="word">The word of exp to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoReference NewRef(
            IBdoScriptword word)
            => NewReference(word);
    }
}