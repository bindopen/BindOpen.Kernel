﻿using BindOpen.Kernel.Scoping.Script;

namespace BindOpen.Kernel.Data
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
        public static BdoExpression NewExpression(
            string text,
            BdoExpressionKind kind = BdoExpressionKind.Script)
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
        public static BdoExpression NewExpression(
            IBdoScriptword word)
            => new()
            {
                Kind = BdoExpressionKind.Word,
                Word = word
            };

        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="kind">The kind of exp to consider.</param>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoExpression NewExp(
            string text,
            BdoExpressionKind kind = BdoExpressionKind.Script)
            => NewExpression(text, kind);

        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="kind">The kind of exp to consider.</param>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoExpression NewExp(
            IBdoScriptword word)
            => NewExpression(word);
    }
}