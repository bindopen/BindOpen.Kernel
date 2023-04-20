using BindOpen.Data.Conditions;
using System;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoSpec :
        IBdoObject, IReferenced, IBdoConditional,
        IIdentified, INamed, IIndexed,
        IBdoTitled, IBdoDescribed, IBdoDetailed,
        ITUpdatable<IBdoSpec>
    {
        /// <summary>
        /// The identifier of the group of this instance.
        /// </summary>
        string GroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IList<SpecificationLevels> SpecLevels { get; set; }

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
        IList<string> Aliases { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ITBdoSet<IBdoSpec> SubSpecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        IBdoSpec GetSubSpec(string name);

        /// <summary>
        /// 
        /// </summary>
        IList<DataMode> ValueModes { get; set; }

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
        BdoDataType DataType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueTypes DataValueType { get; }

        /// <summary>
        /// 
        /// </summary>
        Type DataClassType { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsStatic { get; set; }

        /// <summary>
        /// The label of this instance.
        /// </summary>
        string Label { get; set; }

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
        object DefaultData { get; set; }
    }
}