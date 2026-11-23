using BindOpen.Data.Assemblies;
using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a rule converter.
/// </summary>
public static class SchemaRuleIOConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static SchemaRuleDto ToDto(this IBdoSchemaRule poco)
    {
        if (poco == null) return null;

        SchemaRuleDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static SchemaRuleDto UpdateFromPoco(
        this SchemaRuleDto dto,
        IBdoSchemaRule poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.Condition = poco.Condition.ToDto();
        dto.GroupId = poco.GroupId;
        dto.Identifier = poco.Identifier;
        dto.Kind = poco.Kind;
        dto.Reference = poco.Reference.ToDto();
        dto.ResultCode = poco.ResultCode;
        dto.ResultDescription = poco.ResultDescription;
        dto.ResultEventLevel = poco.ResultEventLevel;
        dto.ResultTitle = poco.ResultTitle;
        dto.Value = BdoData.NewScalar(poco.Value).ToDto();

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoSchemaRule ToPoco(
        this SchemaRuleDto dto)
    {
        if (dto == null) return null;

        BdoSchemaRule poco = new();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoSchemaRule UpdateFromDto(
        this IBdoSchemaRule poco,
        SchemaRuleDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        poco.Condition = dto.Condition.ToPoco();
        poco.GroupId = dto.GroupId;
        poco.Kind = dto.Kind;
        poco.Identifier = dto.Identifier;
        poco.Reference = dto.Reference.ToPoco();
        poco.ResultCode = dto.ResultCode;
        poco.ResultDescription = dto.ResultDescription;
        poco.ResultEventLevel = dto.ResultEventLevel;
        poco.ResultTitle = dto.ResultTitle;
        poco.Value = dto.Value;

        return poco;
    }
}
