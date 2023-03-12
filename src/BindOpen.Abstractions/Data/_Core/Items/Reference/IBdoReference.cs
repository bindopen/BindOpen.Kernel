using BindOpen.Script;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a data expression.
    /// </summary>
    public interface IBdoReference : IBdoExpression
    {
        /// <summary>
        /// The script word.
        /// </summary>
        IBdoScriptword Word { get; set; }
    }
}