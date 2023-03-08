using System.Collections.Generic;

namespace BindOpen.Data.Conditions
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