using BindOpen.MetaData.Items;

namespace BindOpen.MetaData.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScriptCondition : ICondition
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoExpression Expression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        IScriptCondition WithExpression(IBdoExpression expression);
    }
}