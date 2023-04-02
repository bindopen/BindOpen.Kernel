using BindOpen.Data.Conditions;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoSpec :
        IBdoItem, IReferenced,
        IIdentified, INamed, IIndexed,
        IBdoTitled, IBdoDescribed,
        ITUpdatable<IBdoSpec>
    {
        /// <summary>
        /// 
        /// </summary>
        DataValueTypes ValueType { get; set; }

        /// <summary>
        /// The identifier of the group of this instance.
        /// </summary>
        string GroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<SpecificationLevels> SpecLevels { get; set; }

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
        RequirementLevels Requirement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string RequirementExp { get; set; }

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
        List<DataMode> ValueModes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsAllocatable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        uint MinDataItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        uint? MaxDataItemNumber { get; set; }

        // Data

        /// <summary>
        /// 
        /// </summary>
        RequirementLevels DataRequirement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string DataRequirementExp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoExpression DataReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object DefaultData { get; set; }
    }
}