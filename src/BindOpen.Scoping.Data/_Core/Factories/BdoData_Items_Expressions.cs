﻿namespace BindOpen.Scoping.Data
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
            BdoExpressionKind kind = BdoExpressionKind.Auto)
            => new()
            {
                Kind = kind,
                Text = text
            };

        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="kind">The kind of exp to consider.</param>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoExpression NewExp(
            string text,
            BdoExpressionKind kind = BdoExpressionKind.Auto)
            => NewExpression(text, kind);
    }
}