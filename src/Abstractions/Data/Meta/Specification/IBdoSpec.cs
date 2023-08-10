﻿using BindOpen.System.Data.Conditions;
using System.Collections.Generic;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoSpec :
        IBdoObject, IReferenced, IBdoDataTyped, IBdoConditional,
        IIdentified, INamed, IIndexed,
        IBdoTitled, IBdoDescribed, IBdoDetailed,
        ITChild<IBdoCompositeSpec>,
        ITUpdatable<IBdoSpec>
    {
        /// <summary>
        /// The identifier of the group of this instance.
        /// </summary>
        string GroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IList<string> Aliases { get; set; }

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
        bool IsStatic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsAllocatable { get; set; }

        /// <summary>
        /// The label of this instance.
        /// </summary>
        string Label { get; set; }

        // Levels

        /// <summary>
        /// 
        /// </summary>
        IList<DataMode> AvailableDataModes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IList<SpecificationLevels> SpecLevels { get; set; }

        IList<SpecificationLevels> DataSpecLevels { get; set; }

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
        string RequirementExp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        RequirementLevels DataRequirementLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string DataRequirementExp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object DefaultData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="item"></param>
        /// <returns></returns>
        bool IsCompatibleWithData(object item);
    }
}