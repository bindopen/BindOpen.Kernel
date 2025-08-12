using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Conditions;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a Db converter of schemas.
/// </summary>
public static class SchemaDbConverter
{
    /// <summary>
    /// Converts a schema poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static SchemaDb ToDb(
        this IBdoSchema poco,
        DataDbContext context)
    {
        SchemaDb dbItem = new()
        {
            Condition = poco.Condition.ToDb(context),
            Reference = poco.Reference.ToDb(context),
            Description = poco.Description.ToDb(context),
            Detail = poco.Detail.ToDb(context),
            Title = poco.Title.ToDb(context),

            Aliases = poco?.Aliases == null ? null : new List<string>(poco.Aliases),
            AvailableDataModes = poco?.AvailableDataModes == null ? null : new List<DataMode>(poco.AvailableDataModes),

            Children = poco?._Children?.Select(q => q.ToDb(context)).ToList(),

            ClassReference = poco.DataType.IsSpecified() ? poco?.DataType.ToDb() : null,
            Rules = poco?.RuleSet == null ? null : poco?.RuleSet.Select(q => q.ToDb(context)).ToList(),
            DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName,

            MaxDataItemNumber = (int?)(poco?.MaxDataItemNumber == -1 ? null : poco?.MaxDataItemNumber),
            MinDataItemNumber = (int?)(poco?.MinDataItemNumber == 0 ? null : poco?.MinDataItemNumber),

            ValueType = poco?.DataType?.ValueType ?? DataValueTypes.Any
        };

        var dataList = poco.DefaultData?.ToObjectList().Select(q => q?.ToMeta().ToDb(context)).ToList();
        dbItem.DefaultItems = dataList;

        return dbItem;
    }

    /// <summary>
    /// Converts a schema DTO to a poco one.
    /// </summary>
    /// <param key="dbItem">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoSchema ToPoco(
        this SchemaDb dbItem)
    {
        if (dbItem == null) return null;

        BdoSchema poco = new()
        {
            _Children = BdoData.NewItemSet(dbItem?.Children?.Select(q => q.ToPoco()).ToArray()),

            Aliases = dbItem?.Aliases == null ? null : new List<string>(dbItem.Aliases),
            AvailableDataModes = dbItem?.AvailableDataModes == null ? null : new List<DataMode>(dbItem.AvailableDataModes),
            Condition = dbItem.Condition.ToPoco(),
            DataType = new BdoDataType(dbItem?.ClassReference?.ToPoco())
            {
                DefinitionUniqueName = dbItem.DefinitionUniqueName,
                Identifier = dbItem.Identifier,
                ValueType = dbItem.ValueType
            },

            ItemSet = BdoData.NewSchemaSet(dbItem?.Items?.Select(q => q.ToPoco()).ToArray()),
            Reference = dbItem.Reference.ToPoco()
        };

        poco.WithRules(dbItem?.Rules == null ? null : dbItem.Rules.Select(q => q.ToPoco()).ToArray());

        poco
            .WithTitle(dbItem.Title.ToPoco<string>())
            .WithDescription(dbItem.Description.ToPoco<string>())
            .WithDetail(dbItem.Detail.ToPoco());

        return poco;
    }
}
