using BindOpen.Kernel.Data.Conditions;
using BindOpen.Kernel.Logging;
using System.Collections.Generic;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    public partial class BdoSpec : BdoObject, IBdoSpec
    {
        public virtual void Update(
            object item,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            if (item is IBdoSpec spec)
            {
                AccessibilityLevel = spec?.AccessibilityLevel ?? AccessibilityLevels.None;
                Aliases = spec?.Aliases == null ? null : new List<string>(spec?.Aliases);
                AvailableDataModes = spec?.AvailableDataModes == null ? null : new List<DataMode>(spec?.AvailableDataModes);
                Condition = spec?.Condition?.Clone<IBdoCondition>();
                Reference = spec?.Reference?.Clone<IBdoReference>();
                ItemRequirementStatement = spec?.ItemRequirementStatement?.Clone<ITBdoConditionalStatement<RequirementLevels>>();
                ItemSpecLevels = spec?.ItemSpecLevels == null ? null : new List<SpecificationLevels>(spec?.ItemSpecLevels);
                DataType = spec?.DataType ?? new BdoDataType();
                DefaultData = spec?.DefaultData;
                Description = spec?.Description?.Clone<TBdoDictionary<string>>();
                Detail = spec?.Detail?.Clone<IBdoMetaSet>();
                GroupId = spec?.GroupId;
                Index = spec?.Index;
                InheritanceLevel = spec?.InheritanceLevel ?? InheritanceLevels.None;
                IsAllocatable = spec?.IsAllocatable ?? false;
                IsStatic = spec?.IsStatic ?? false;
                Label = spec?.Label;
                MinDataItemNumber = spec?.MinDataItemNumber ?? 0;
                Name = spec?.Name;
                RequirementStatement = spec?.RequirementStatement?.Clone<ITBdoConditionalStatement<RequirementLevels>>();
                SpecLevels = spec?.SpecLevels == null ? null : new List<SpecificationLevels>(spec?.SpecLevels);
                Title = spec?.Title?.Clone<TBdoDictionary<string>>();
            }
        }
    }
}
