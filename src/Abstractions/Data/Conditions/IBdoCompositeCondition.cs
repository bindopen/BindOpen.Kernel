using System.Collections.Generic;

namespace BindOpen.Kernel.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoCompositeCondition : IBdoCondition
    {
        /// <summary>
        /// 
        /// </summary>
        BdoCompositeConditionKind CompositionKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IList<IBdoCondition> Conditions { get; set; }
    }
}