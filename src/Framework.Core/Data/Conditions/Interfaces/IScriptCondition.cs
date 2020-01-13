using BindOpen.Framework.Data.Expression;

namespace BindOpen.Framework.Data.Conditions
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