using System.Collections.Generic;

namespace BindOpen.System.Data.Conditions
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