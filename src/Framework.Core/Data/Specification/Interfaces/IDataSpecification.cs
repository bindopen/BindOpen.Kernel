using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Specification
{
    public interface IDataSpecification : IIndexedDataItem
    {
        AccessibilityLevel AccessibilityLevel { get; set; }
        InheritanceLevel InheritanceLevel { get; set; }
        RequirementLevel RequirementLevel { get; set; }
        string RequirementScript { get; set; }
        List<SpecificationLevel> SpecificationLevels { get; set; }

        ILog Check(IDataSpecification referenceSpecification = null);
        bool IsCompatibleWith(IDataItem item);
        ILog Repair(IDataSpecification referenceSpecification = null);
        ILog Update(IDataElementSpec referenceSpecification = null, string[] specificationAreas = null);
    }
}