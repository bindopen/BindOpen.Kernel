using System.Collections.Generic;

namespace BindOpen.Framework.Core.Data.Conditions
{
    public interface IAdvancedCondition: ICondition
    {
        List<ICondition> Conditions { get; set; }
        AdvancedCondition.AdvancedConditionKind Kind { get; set; }
    }
}