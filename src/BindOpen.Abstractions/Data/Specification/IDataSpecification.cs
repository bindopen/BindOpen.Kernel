using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Data.Specification
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataSpecification :
        IBdoItem, IReferenced,
        IIdentified, INamed,
        IGloballyTitled, IGloballyDescribed,
        IIndexed
    {
        /// <summary>
        /// 
        /// </summary>
        DataValueTypes DataValueType { get; set; }

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
        /// <param name="item"></param>
        /// <returns></returns>
        bool IsCompatibleWithItem(object item);
    }
}