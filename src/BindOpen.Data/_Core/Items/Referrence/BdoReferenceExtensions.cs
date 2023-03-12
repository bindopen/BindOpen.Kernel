using BindOpen.Script;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    public static class BdoReferenceExtensions
    {
        /// <summary>
        /// Creates a new literal exp into auto mode.
        /// </summary>
        /// <param key="text">The script text to consider.</param>
        /// <returns>Returns the script exp.</returns>
        public static BdoReference ToReference(
            this string text,
            BdoExpressionKind kind = BdoExpressionKind.Auto)
        {
            var reference = BdoData.NewReference(text, kind);
            return reference;
        }

        /// <summary>
        /// Creates a new word exp.
        /// </summary>
        /// <param key="scriptword">The script word to consider.</param>
        /// <returns>Returns the script exp.</returns>
        public static BdoReference ToReference(
            this IBdoScriptword scriptword)
        {
            var exp = BdoData.NewReference(scriptword);
            return exp;
        }
    }
}