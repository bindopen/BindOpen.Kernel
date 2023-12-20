using System.Collections.Generic;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoCompositeCondition : IBdoCondition, ITParent<IBdoCondition>
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