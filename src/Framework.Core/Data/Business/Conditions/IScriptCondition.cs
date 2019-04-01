using BindOpen.Framework.Core.Data.Expression;

namespace BindOpen.Framework.Core.Data.Business.Conditions
{
    public interface IScriptCondition : ICondition
    {
        IDataExpression Expression { get; set; }
    }
}