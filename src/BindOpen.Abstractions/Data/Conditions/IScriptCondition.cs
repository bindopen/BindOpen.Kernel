namespace BindOpen.Data.Conditions
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
        /// <param key="expression"></param>
        IScriptCondition WithExpression(IBdoExpression expression);
    }
}