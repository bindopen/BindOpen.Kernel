using BindOpen.Kernel.Data.Conditions;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;
using System.Collections.Generic;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    public partial class BdoSpec : TBdoSet<IBdoConstraint>, IBdoSpec
    {
        public override void Update(
            object item,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            if (item is IBdoSpec spec)
            {
                ((TBdoSet<IBdoConstraint>)item).Update(item, areas, updateModes, log);

                AccessibilityLevel = spec?.AccessibilityLevel ?? AccessibilityLevels.None;
                Aliases = spec?.Aliases == null ? null : new List<string>(spec?.Aliases);
                AvailableDataModes = spec?.AvailableDataModes == null ? null : new List<DataMode>(spec?.AvailableDataModes);
                Condition = spec?.Condition?.Clone<IBdoCondition>();
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
                Reference = spec?.Reference?.Clone<IBdoReference>();
                Title = spec?.Title?.Clone<TBdoDictionary<string>>();
            }
        }
    }
}
