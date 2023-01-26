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
        DataValueTypes ValueType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        IDataSpecification WithValueType(DataValueTypes valueType);

        /// <summary>
        /// 
        /// </summary>
        AccessibilityLevels AccessibilityLevel { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        IDataSpecification WithAccessibilityLevel(AccessibilityLevels level);

        /// <summary>
        /// 
        /// </summary>
        InheritanceLevels InheritanceLevel { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        IDataSpecification WithInheritanceLevel(InheritanceLevels level);

        /// <summary>
        /// 
        /// </summary>
        RequirementLevels RequirementLevel { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        IDataSpecification WithRequirementLevel(RequirementLevels level);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDataSpecification AsOptional();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDataSpecification AsRequired();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDataSpecification AsForbidden();

        /// <summary>
        /// 
        /// </summary>
        string RequirementScript { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        IDataSpecification WithRequirementScript(string script);

        /// <summary>
        /// 
        /// </summary>
        List<SpecificationLevels> SpecificationLevels { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levels"></param>
        IDataSpecification WithSpecificationLevels(params SpecificationLevels[] levels);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool IsCompatibleWithItem(object item);
    }
}