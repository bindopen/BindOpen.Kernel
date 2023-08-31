using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping.Script;

namespace BindOpen.System.Data
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
            BdoReferenceKind kind)
            => new()
            {
                Kind = kind
            };

        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="kind">The kind of exp to consider.</param>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoReference NewReference(
            IBdoExpression exp)
            => new()
            {
                Kind = BdoReferenceKind.Expression,
                Expression = exp
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
                Kind = BdoReferenceKind.Expression,
                Expression = NewExp(word)
            };

        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="word">The word of exp to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoReference NewReference(
            string identifier)
            => new()
            {
                Kind = BdoReferenceKind.Identifier,
                Identifier = identifier
            };

        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="word">The word of exp to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoReference NewReference(
            IBdoMetaData meta)
            => new()
            {
                Kind = BdoReferenceKind.MetaData,
                MetaData = meta
            };

        // Alias

        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="kind">The kind of exp to consider.</param>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoReference NewRef(
            IBdoExpression exp)
            => NewReference(exp);

        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="word">The word of exp to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoReference NewRef(
            IBdoScriptword word)
            => NewReference(word);

        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="word">The word of exp to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoReference NewRef(
            string identifier)
            => NewReference(identifier);

        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="word">The word of exp to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoReference NewRef(
            IBdoMetaData meta)
            => NewReference(meta);

        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="kind">The kind of exp to consider.</param>
        /// <param key="text">The text to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static BdoReference NewRef(
            BdoReferenceKind kind)
            => NewReference(kind);
    }
}