using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.Data.Specification.Constraints;
using BindOpen.Framework.Core.Data.Specification.Design;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Elements
{
    public interface IDataElementSpec : IDataSpecification
    {
        List<string> Aliases { get; set; }
        List<DataAreaSpecification> AreaSpecifications { get; set; }
        List<DataItemizationMode> AvailableItemizationModes { get; set; }
        DataConstraintStatement ConstraintStatement { get; set; }
        List<object> DefaultItems { get; set; }
        List<string> DefaultStringItems { get; set; }
        DataDesignStatement DesignStatement { get; set; }
        string GroupId { get; set; }
        bool IsAllocatable { get; set; }
        bool IsValueList { get; }
        RequirementLevel ItemRequirementLevel { get; }
        string ItemScript { get; set; }
        List<SpecificationLevels> ItemSpecificationLevels { get; set; }
        int MaximumItemNumber { get; set; }
        int MinimumItemNumber { get; set; }
        DataValueType ValueType { get; set; }

        bool AddDefaultItem(object item);
        IDataAreaSpecification GetAreaSpecification(string areaName);
        object GetDefaultItemObject(ILog log = null);
        bool SetDefaultItem(List<object> defaultItems);
        bool SetDefaultItem(object item);

        ILog CheckItem(
            object item,
            IDataElement dataElement);

        ILog CheckElement(
            IDataElement dataElement,
            string[] specificationAreas = null);
    }
}