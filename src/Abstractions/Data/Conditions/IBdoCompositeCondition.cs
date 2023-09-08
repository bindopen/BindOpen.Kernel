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
        CompositeConditionKind Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IList<IBdoCondition> Conditions { get; set; }
    }
}