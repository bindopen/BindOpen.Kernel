namespace BindOpen.Meta.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBasicCondition : ICondition
    {
        /// <summary>
        /// 
        /// </summary>
        object Argument1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object Argument2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ConditionOperator Operator { get; set; }
    }
}