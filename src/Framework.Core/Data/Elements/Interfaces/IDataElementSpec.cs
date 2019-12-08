using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataElementSpec : IDataSpecification
    {
        /// <summary>
        /// 
        /// </summary>
        List<string> Aliases { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<DataAreaSpecification> AreaSpecifications { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<DataItemizationMode> AvailableItemizationModes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataConstraintStatement ConstraintStatement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<object> DefaultItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<string> DefaultStringItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataDesignStatement DesignStatement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string GroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsAllocatable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsValueList { get; }

        /// <summary>
        /// 
        /// </summary>
        RequirementLevel ItemRequirementLevel { get; }

        /// <summary>
        /// 
        /// </summary>
        string ItemScript { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<SpecificationLevels> ItemSpecificationLevels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int MaximumItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int MinimumItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueType ValueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool AddDefaultItem(object item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaName"></param>
        /// <returns></returns>
        IDataAreaSpecification GetAreaSpecification(string areaName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        object GetDefaultItemObject(IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultItems"></param>
        /// <returns></returns>
        bool SetDefaultItem(List<object> defaultItems);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool SetDefaultItem(object item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dataElement"></param>
        /// <returns></returns>
        IBdoLog CheckItem(
            object item,
            IDataElement dataElement);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataElement"></param>
        /// <param name="specificationAreas"></param>
        /// <returns></returns>
        IBdoLog CheckElement(
            IDataElement dataElement,
            string[] specificationAreas = null);
    }
}