using System.Collections.Generic;
using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.System.Diagnostics;

namespace BindOpen.Data.Specification
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataSpecification : IIndexedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        AccessibilityLevels AccessibilityLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        InheritanceLevel InheritanceLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        RequirementLevel RequirementLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string RequirementScript { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<SpecificationLevels> SpecificationLevels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceSpecification"></param>
        /// <returns></returns>
        IBdoLog Check(IDataSpecification referenceSpecification = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool IsCompatibleWith(IDataItem item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceSpecification"></param>
        /// <returns></returns>
        IBdoLog Repair(IDataSpecification referenceSpecification = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceSpecification"></param>
        /// <param name="specificationAreas"></param>
        /// <returns></returns>
        IBdoLog Update(IDataElementSpec referenceSpecification = null, string[] specificationAreas = null);
    }
}