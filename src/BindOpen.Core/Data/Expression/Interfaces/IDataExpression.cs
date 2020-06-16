using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;

namespace BindOpen.Data.Expression
{
    /// <summary>
    /// This interface defines a data expression.
    /// </summary>
    public interface IDataExpression : IDataItem
    {
        /// <summary>
        /// The kind.
        /// </summary>
        DataExpressionKind Kind { get; }

        /// <summary>
        /// The text.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// The script word.
        /// </summary>
        BdoScriptword Word { get; }
    }
}