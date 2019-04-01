using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.Data.Specification.Constraints;
using BindOpen.Framework.Core.Data.Specification.Design;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements
{
    public interface IDataElementSpec : IDataSpecification
    {
        List<string> Aliases { get; set; }
        List<IDataAreaSpecification> AreaSpecifications { get; set; }
        List<DataItemizationMode> AvailableItemizationModes { get; set; }
        IDataConstraintStatement ConstraintStatement { get; set; }
        List<object> DefaultItems { get; set; }
        List<string> DefaultStringItems { get; set; }
        IDataDesignStatement DesignStatement { get; set; }
        string GroupId { get; set; }
        bool IsAllocatable { get; set; }
        bool IsValueList { get; }
        RequirementLevel ItemRequirementLevel { get; }
        string ItemScript { get; set; }
        List<SpecificationLevel> ItemSpecificationLevels { get; set; }
        int MaximumItemNumber { get; set; }
        int MinimumItemNumber { get; set; }
        DataValueType ValueType { get; set; }

        bool AddDefaultItem(object item);
        IDataAreaSpecification GetAreaSpecification(string areaName);
        object GetDefaultItemObject(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        bool IsCompatibleWith(IDataItem item);
        bool SetDefaultItem(List<object> defaultItems);
        bool SetDefaultItem(object item);
    }
}