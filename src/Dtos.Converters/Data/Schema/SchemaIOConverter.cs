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
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static SchemaDto ToDto(this IBdoSchema poco)
    {
        if (poco == null) return null;

        SchemaDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static SchemaDto UpdateFromPoco(
        this SchemaDto dto,
        IBdoSchema poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.Aliases = poco?.Aliases == null ? null : [.. poco.Aliases];
        dto.AvailableDataModes = poco?.AvailableDataModes == null ? null : [.. poco.AvailableDataModes];
        dto.Children = poco?._Children?.Select(q => q.ToDto()).ToList();
        dto.ClassReference = poco.DataType.IsSpecified() ? poco?.DataType.ToDto() : null;
        dto.DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName;
        dto.Description = poco.Description.ToDto();

        var dataList = poco.DefaultData?.ToObjectList().Select(q => q?.ToMeta().ToDto()).ToList();
        dto.DefaultItems = dataList;

        dto.Index = poco.Index ?? -1;
        dto.Identifier = poco.Identifier;
        dto.MaxDataItemNumber = (int?)(poco?.MaxDataItemNumber == -1 ? null : poco?.MaxDataItemNumber);
        dto.MinDataItemNumber = (int?)(poco?.MinDataItemNumber == 0 ? null : poco?.MinDataItemNumber);
        dto.Name = poco.Name;
        dto.Reference = poco.Reference.ToDto();
        dto.Rules = poco?.RuleSet == null ? null : poco?.RuleSet.Select(q => q.ToDto()).ToList();
        dto.Title = poco.Title.ToDto();
        dto.ValueType = poco?.DataType?.ValueType ?? DataValueTypes.Any;

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoSchema ToPoco(
        this SchemaDto dto)
    {
        if (dto == null) return null;

        BdoSchema poco = new();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoSchema UpdateFromDto(
        this IBdoSchema poco,
        SchemaDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        poco.Aliases = dto?.Aliases == null ? null : new List<string>(dto.Aliases);
        poco.AvailableDataModes = dto?.AvailableDataModes == null ? null : new List<DataMode>(dto.AvailableDataModes);
        poco.DataType = new BdoDataType(dto?.ClassReference?.ToPoco())
        {
            DefinitionUniqueName = dto.DefinitionUniqueName,
            Identifier = dto.Identifier,
            ValueType = dto.ValueType
        };

        poco
            .WithChildren(dto?.Children?.Select(q => q.ToPoco()).ToArray())
            .WithDescription(dto.Description.ToPoco<string>())
            .WithDetail(dto.Detail.ToPoco())
            .WithItems(dto?.Items?.Select(q => q.ToPoco()).ToArray())
            .WithRules(dto?.Rules == null ? null : [.. dto.Rules.Select(q => q.ToPoco())])
            .WithTitle(dto.Title.ToPoco<string>());

        return poco;
    }
}
