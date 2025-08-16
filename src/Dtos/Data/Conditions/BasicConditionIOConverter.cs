using BindOpen.Data.Meta;

namespace BindOpen.Data.Conditions;

/// <summary>
/// This class represents a IO converter of assembly references.
/// </summary>
public static class BasicConditionIOConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static BasicConditionDto ToDto(this IBdoBasicCondition poco)
    {
        if (poco == null) return null;

        BasicConditionDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static BasicConditionDto UpdateFromPoco(
        this BasicConditionDto dto,
        IBdoBasicCondition poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.ArgumentMetaData1 = poco.Argument1.ToDto();
        dto.ArgumentMetaData2 = poco.Argument2.ToDto();
        dto.Identifier = poco.Identifier;
        dto.Name = poco.Name;
        dto.Operator = poco.Operator;
        dto.ParentId = poco.Parent?.Identifier;

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoBasicCondition ToPoco(
        this BasicConditionDto dto)
    {
        if (dto == null) return null;

        BdoBasicCondition poco = new();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoBasicCondition UpdateFromDto(
        this IBdoBasicCondition poco,
        BasicConditionDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        poco.Argument1 = dto.ArgumentMetaData1.ToPoco();
        poco.Argument2 = dto.ArgumentMetaData2.ToPoco();
        poco.Identifier = dto.Identifier;
        poco.Name = dto.Name;
        poco.Operator = dto.Operator;
        poco.Parent = null;

        return poco;
    }
}
