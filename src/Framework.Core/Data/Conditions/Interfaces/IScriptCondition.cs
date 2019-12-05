using BindOpen.Framework.Core.Data.Expression;

namespace BindOpen.Framework.Core.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScriptCondition : ICondition
    {
        /// <summary>
        /// 
        /// </summary>
        DataExpression Expression { get; set; }
    }
}