using BindOpen.Data.Conditions;
using BindOpen.Data.Configuration;
using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoSpec :
        IBdoItem, IReferenced,
        IIdentified, INamed,
        IBdoTitled, IBdoDescribed,
        IIndexed
    {
        /// <summary>
        /// 
        /// </summary>
        DataValueTypes ValueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        AccessibilityLevels AccessibilityLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        InheritanceLevels InheritanceLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        RequirementLevels RequirementLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoExpression RequirementExpression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<SpecificationLevels> SpecificationLevels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="item"></param>
        /// <returns></returns>
        bool IsCompatibleWithData(object item);

        /// <summary>
        /// 
        /// </summary>
        ICondition Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<string> Aliases { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<IBdoSpec> SubSpecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        IBdoSpec GetSubSpec(string name);

        /// <summary>
        /// 
        /// </summary>
        List<DataValueMode> ValueModes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoConfigurationSet ConstraintStatement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsAllocatable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        RequirementLevels ItemRequirementLevel { get; }

        /// <summary>
        /// 
        /// </summary>
        IBdoExpression ItemExpression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<SpecificationLevels> ItemSpecificationLevels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        uint MinimumItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        uint? MaximumItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object DefaultItem { get; set; }
    }
}