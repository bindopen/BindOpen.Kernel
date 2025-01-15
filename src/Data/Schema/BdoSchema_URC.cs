using BindOpen.Data.Assemblies;
using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using System.Collections.Generic;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a data element schema.
/// </summary>
public partial class BdoSchema : BdoObject, IBdoSchema
{
    public void Update(
        object item,
        string[] areas = null,
        UpdateModes[] updateModes = null,
        IBdoLog log = null)
    {
        if (item is IBdoSchema schema)
        {
            _children = schema?._Children?.Clone<ITBdoSet<IBdoSchema>>();
            AccessibilityLevel = schema?.AccessibilityLevel ?? AccessibilityLevels.None;
            Aliases = schema?.Aliases == null ? null : new List<string>(schema?.Aliases);
            AvailableDataModes = schema?.AvailableDataModes == null ? null : new List<DataMode>(schema?.AvailableDataModes);
            Condition = schema?.Condition?.Clone<IBdoCondition>();
            DataType = schema?.DataType ?? new BdoDataType();
            DefaultData = schema?.DefaultData;
            Description = schema?.Description?.Clone<TBdoDictionary<string>>();
            Detail = schema?.Detail?.Clone<IBdoMetaSet>();
            GroupId = schema?.GroupId;
            Index = schema?.Index;
            InheritanceLevel = schema?.InheritanceLevel ?? InheritanceLevels.None;
            ItemSet = schema?.ItemSet?.Clone<BdoSchemaSet>();
            Label = schema?.Label;
            MinDataItemNumber = schema?.MinDataItemNumber ?? 0;
            Name = schema?.Name;
            Reference = schema?.Reference?.Clone<IBdoReference>();
            RuleSet = schema?.RuleSet?.Clone<ITBdoGroupsOf<IBdoSchemaRule>>();
            Title = schema?.Title?.Clone<TBdoDictionary<string>>();
        }
    }
}
