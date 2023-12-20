using BindOpen.Data.Conditions;
using BindOpen.Logging;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    public partial class BdoSpec : TBdoSet<IBdoSpecRule>, IBdoSpec
    {
        public override void Update(
            object item,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            if (item is IBdoSpec spec)
            {
                this.With(spec?.Items?.Select(q => q.Clone<IBdoSpecRule>()).ToArray());

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
                Label = spec?.Label;
                MinDataItemNumber = spec?.MinDataItemNumber ?? 0;
                Name = spec?.Name;
                Reference = spec?.Reference?.Clone<IBdoReference>();
                Title = spec?.Title?.Clone<TBdoDictionary<string>>();
            }
        }
    }
}
