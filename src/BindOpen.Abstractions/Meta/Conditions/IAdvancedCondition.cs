using System.Collections.Generic;

namespace BindOpen.Meta.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAdvancedCondition : ICondition
    {
        /// <summary>
        /// 
        /// </summary>
        AdvancedConditionKind Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<ICondition> Conditions { get; set; }
    }
}