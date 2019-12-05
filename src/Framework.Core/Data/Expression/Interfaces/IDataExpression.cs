using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Expression
{
    /// <summary>
    /// This interface defines a data expression.
    /// </summary>
    public interface IDataExpression : IDataItem
    {
        /// <summary>
        /// The kind.
        /// </summary>
        DataExpressionKind Kind { get; set; }

        /// <summary>
        /// The text.
        /// </summary>
        string Text { get; set; }
    }
}