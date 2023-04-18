using System.Collections.Generic;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoAdvancedCondition : IBdoCondition
    {
        /// <summary>
        /// 
        /// </summary>
        AdvancedConditionKind Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<IBdoCondition> Conditions { get; set; }
    }
}