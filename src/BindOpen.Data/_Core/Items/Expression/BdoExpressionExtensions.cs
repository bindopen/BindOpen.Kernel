using BindOpen.Scripting;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    public static class BdoExpressionExtensions
    {
        /// <summary>
        /// Creates a new literal exp into auto mode.
        /// </summary>
        /// <param key="text">The script text to consider.</param>
        /// <returns>Returns the script exp.</returns>
        public static BdoExpression ToExpression(
            this string text,
            BdoExpressionKind kind = BdoExpressionKind.Auto)
        {
            var exp = BdoData.NewExpression(text, kind);
            return exp;
        }

        /// <summary>
        /// Creates a new word exp.
        /// </summary>
        /// <param key="scriptword">The script word to consider.</param>
        /// <returns>Returns the script exp.</returns>
        public static BdoExpression ToExpression(
            this IBdoScriptword scriptword)
        {
            var exp = BdoData.NewExpression(scriptword);
            return exp;
        }
    }
}