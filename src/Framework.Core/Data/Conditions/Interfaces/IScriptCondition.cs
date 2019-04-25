using BindOpen.Framework.Core.Data.Expression;

namespace BindOpen.Framework.Core.Data.Conditions
{
    public interface IScriptCondition : ICondition
    {
        DataExpression Expression { get; set; }
    }
}