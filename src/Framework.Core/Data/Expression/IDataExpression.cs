using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Expression
{
    public interface IDataExpression : IDataItem
    {
        DataExpressionKind Kind { get; set; }

        string Text { get; set; }
    }
}