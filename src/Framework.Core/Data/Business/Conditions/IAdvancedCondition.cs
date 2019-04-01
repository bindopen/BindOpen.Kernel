using System.Collections.Generic;

namespace BindOpen.Framework.Core.Data.Business.Conditions
{
    public interface IAdvancedCondition: ICondition
    {
        List<ICondition> Conditions { get; set; }
        AdvancedCondition.AdvancedConditionKind Kind { get; set; }
    }
}