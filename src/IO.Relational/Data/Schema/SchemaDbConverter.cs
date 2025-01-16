using AutoMapper;
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
        if (poco == null) return null;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<BdoSchema, SchemaDb>()
                .ForMember(q => q.Aliases, opt => opt.Ignore())
                .ForMember(q => q.AvailableDataModes, opt => opt.Ignore())

                .ForMember(q => q.Children, opt => opt.Ignore())
                .ForMember(q => q.ClassReference, opt => opt.Ignore())
                .ForMember(q => q.Condition, opt => opt.MapFrom(q => q.Condition.ToDb(context)))
                .ForMember(q => q.Rules, opt => opt.Ignore())
                .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToDb(context)))
                .ForMember(q => q.DefaultItems, opt => opt.Ignore())
                .ForMember(q => q.Description, opt => opt.MapFrom(q => q.Description.ToDb(context)))
                .ForMember(q => q.Detail, opt => opt.MapFrom(q => q.Detail.ToDb(context)))
                .ForMember(q => q.Title, opt => opt.MapFrom(q => q.Title.ToDb(context)))
        );

        var mapper = new Mapper(config);
        var dbItem = mapper.Map<SchemaDb>(poco);

        dbItem.Aliases = poco?.Aliases == null ? null : new List<string>(poco.Aliases);
        dbItem.AvailableDataModes = poco?.AvailableDataModes == null ? null : new List<DataMode>(poco.AvailableDataModes);

        dbItem.Children = poco?._Children?.Select(q => q.ToDb(context)).ToList();

        dbItem.ClassReference = poco.DataType.IsSpecified() ? poco?.DataType.ToDb() : null;
        dbItem.Rules = poco?.RuleSet == null ? null : poco?.RuleSet.Select(q => q.ToDb(context)).ToList();
        dbItem.DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName;

        dbItem.MaxDataItemNumber = (int?)(poco?.MaxDataItemNumber == -1 ? null : poco?.MaxDataItemNumber);
        dbItem.MinDataItemNumber = (int?)(poco?.MinDataItemNumber == 0 ? null : poco?.MinDataItemNumber);

        dbItem.ValueType = poco?.DataType?.ValueType ?? DataValueTypes.Any;

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

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<SchemaDb, BdoSchema>()
                .ForMember(q => q._Children, opt => opt.Ignore())

                .ForMember(q => q.Aliases, opt => opt.Ignore())
                .ForMember(q => q.AvailableDataModes, opt => opt.Ignore())

                .ForMember(q => q.Condition, opt => opt.MapFrom(q => q.Condition.ToPoco()))
                .ForMember(q => q.ItemSet, opt => opt.Ignore())
                .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToPoco()))
                .ForMember(q => q.DataType, opt => opt.Ignore())
                .ForMember(q => q.Description, opt => opt.Ignore())
                .ForMember(q => q.DefaultData, opt => opt.Ignore())
                .ForMember(q => q.Detail, opt => opt.Ignore())
                .ForMember(q => q.Title, opt => opt.Ignore())
            );

        var mapper = new Mapper(config);
        var poco = mapper.Map<BdoSchema>(dbItem);

        poco._Children = BdoData.NewItemSet(dbItem?.Children?.Select(q => q.ToPoco()).ToArray());

        poco.Aliases = dbItem?.Aliases == null ? null : new List<string>(dbItem.Aliases);
        poco.AvailableDataModes = dbItem?.AvailableDataModes == null ? null : new List<DataMode>(dbItem.AvailableDataModes);

        poco.DataType = new BdoDataType(dbItem?.ClassReference?.ToPoco())
        {
            DefinitionUniqueName = dbItem.DefinitionUniqueName,
            Identifier = dbItem.Identifier,
            ValueType = dbItem.ValueType
        };

        poco.ItemSet = BdoData.NewSchemaSet(dbItem?.Items?.Select(q => q.ToPoco()).ToArray());

        poco.WithRules(dbItem?.Rules == null ? null : dbItem.Rules.Select(q => q.ToPoco()).ToArray());

        poco
            .WithTitle(dbItem.Title.ToPoco<string>())
            .WithDescription(dbItem.Description.ToPoco<string>())
            .WithDetail(dbItem.Detail.ToPoco());

        return poco;
    }
}
