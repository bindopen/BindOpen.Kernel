using BindOpen.System.Data.Conditions;
using BindOpen.System.Logging;
using System.Collections.Generic;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    public partial class BdoSpec : BdoObject, IBdoSpec
    {
        public virtual void Update(
            IBdoSpec refItem,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            AccessibilityLevel = refItem?.AccessibilityLevel ?? AccessibilityLevels.None;
            Aliases = refItem?.Aliases == null ? null : new List<string>(refItem?.Aliases);
            AvailableDataModes = refItem?.AvailableDataModes == null ? null : new List<DataMode>(refItem?.AvailableDataModes);
            Condition = refItem?.Condition?.Clone<IBdoCondition>();
            Reference = refItem?.Reference?.Clone<IBdoReference>();
            ItemRequirementLevelStatement = refItem?.ItemRequirementLevelStatement?.Clone<ITBdoConditionalStatement<RequirementLevels>>();
            ItemSpecLevels = refItem?.ItemSpecLevels == null ? null : new List<SpecificationLevels>(refItem?.ItemSpecLevels);
            DataType = refItem?.DataType ?? new BdoDataType();
            DefaultData = refItem?.DefaultData;
            Description = refItem?.Description?.Clone<TBdoDictionary<string>>();
            Detail = refItem?.Detail?.Clone<IBdoMetaSet>();
            GroupId = refItem?.GroupId;
            Index = refItem?.Index;
            InheritanceLevel = refItem?.InheritanceLevel ?? InheritanceLevels.None;
            IsAllocatable = refItem?.IsAllocatable ?? false;
            IsStatic = refItem?.IsStatic ?? false;
            Label = refItem?.Label;
            MinDataItemNumber = refItem?.MinDataItemNumber ?? 0;
            Name = refItem?.Name;
            RequirementLevelStatement = refItem?.RequirementLevelStatement?.Clone<ITBdoConditionalStatement<RequirementLevels>>();
            SpecLevels = refItem?.SpecLevels == null ? null : new List<SpecificationLevels>(refItem?.SpecLevels);
            Title = refItem?.Title?.Clone<TBdoDictionary<string>>();
        }
    }
}
