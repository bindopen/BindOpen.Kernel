using BindOpen.Scripting;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a data expression.
    /// </summary>
    public interface IBdoExpression : IBdoNotMetableItem
    {
        /// <summary>
        /// The kind.
        /// </summary>
        BdoExpressionKind Kind { get; set; }

        /// <summary>
        /// The text.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// The script word.
        /// </summary>
        IBdoScriptword Word { get; set; }
    }
}