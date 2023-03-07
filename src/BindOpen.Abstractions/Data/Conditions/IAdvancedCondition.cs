using System.Collections.Generic;
using BindOpen.Abstractions.Data._Core.Enums;

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