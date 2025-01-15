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
/// This class represents a IO converter of schemas.
/// </summary>
public static class SchemaIOConverter
{
    /// <summary>
    /// Converts a schema poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static SchemaDto ToDto(this IBdoSchema poco)
    {
        if (poco == null) return null;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<BdoSchema, SchemaDto>()
                .ForMember(q => q.Aliases, opt => opt.Ignore())
                .ForMember(q => q.AvailableDataModes, opt => opt.Ignore())

                .ForMember(q => q.Children, opt => opt.Ignore())
                .ForMember(q => q.ClassReference, opt => opt.Ignore())
                .ForMember(q => q.Condition, opt => opt.MapFrom(q => q.Condition.ToDto()))
                .ForMember(q => q.Rules, opt => opt.Ignore())
                .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToDto()))
                .ForMember(q => q.DefaultItems, opt => opt.Ignore())
                .ForMember(q => q.Description, opt => opt.MapFrom(q => q.Description.ToDto()))
                .ForMember(q => q.Detail, opt => opt.MapFrom(q => q.Detail.ToDto()))
                .ForMember(q => q.Title, opt => opt.MapFrom(q => q.Title.ToDto()))
        );

        var mapper = new Mapper(config);
        var dto = mapper.Map<SchemaDto>(poco);

        dto.Aliases = poco?.Aliases == null ? null : new List<string>(poco.Aliases);
        dto.AvailableDataModes = poco?.AvailableDataModes == null ? null : new List<DataMode>(poco.AvailableDataModes);

        dto.Children = poco?._Children?.Select(q => q.ToDto()).ToList();

        dto.ClassReference = poco.DataType.IsSpecified() ? poco?.DataType.ToDto() : null;
        dto.Rules = poco?.RuleSet == null ? null : poco?.RuleSet.Select(q => q.ToDto()).ToList();
        dto.DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName;

        dto.MaxDataItemNumber = (int?)(poco?.MaxDataItemNumber == -1 ? null : poco?.MaxDataItemNumber);
        dto.MinDataItemNumber = (int?)(poco?.MinDataItemNumber == 0 ? null : poco?.MinDataItemNumber);

        dto.ValueType = poco?.DataType?.ValueType ?? DataValueTypes.Any;

        var dataList = poco.DefaultData?.ToObjectList().Select(q => q?.ToMeta().ToDto()).ToList();
        dto.DefaultItems = dataList;

        return dto;
    }

    /// <summary>
    /// Converts a schema DTO to a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoSchema ToPoco(
        this SchemaDto dto)
    {
        if (dto == null) return null;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<SchemaDto, BdoSchema>()
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
        var poco = mapper.Map<BdoSchema>(dto);

        poco._Children = BdoData.NewItemSet(dto?.Children?.Select(q => q.ToPoco()).ToArray());

        poco.Aliases = dto?.Aliases == null ? null : new List<string>(dto.Aliases);
        poco.AvailableDataModes = dto?.AvailableDataModes == null ? null : new List<DataMode>(dto.AvailableDataModes);

        poco.DataType = new BdoDataType(dto?.ClassReference?.ToPoco())
        {
            DefinitionUniqueName = dto.DefinitionUniqueName,
            Identifier = dto.Identifier,
            ValueType = dto.ValueType
        };

        poco.ItemSet = BdoData.NewSchemaSet(dto?.Items?.Select(q => q.ToPoco()).ToArray());

        poco.WithRules(dto?.Rules == null ? null : dto.Rules.Select(q => q.ToPoco()).ToArray());

        poco
            .WithTitle(dto.Title.ToPoco<string>())
            .WithDescription(dto.Description.ToPoco<string>())
            .WithDetail(dto.Detail.ToPoco());

        return poco;
    }
}
