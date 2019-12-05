using System.Collections.Generic;

namespace BindOpen.Framework.Core.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAdvancedCondition: ICondition
    {
        /// <summary>
        /// 
        /// </summary>
        List<ICondition> Conditions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        AdvancedCondition.AdvancedConditionKind Kind { get; set; }
    }
}