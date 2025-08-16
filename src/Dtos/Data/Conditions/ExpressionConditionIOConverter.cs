namespace BindOpen.Data.Conditions;

/// <summary>
/// This class represents a IO converter of assembly references.
/// </summary>
public static class ExpressionConditionIOConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static ExpressionConditionDto ToDto(this IBdoExpressionCondition poco)
    {
        if (poco == null) return null;

        ExpressionConditionDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static ExpressionConditionDto UpdateFromPoco(
        this ExpressionConditionDto dto,
        IBdoExpressionCondition poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.ExpressionItem = poco.Expression.ToDto();
        dto.Identifier = poco.Identifier;
        dto.Name = poco.Name;
        dto.ParentId = poco.Parent?.Identifier;

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoExpressionCondition ToPoco(
        this ExpressionConditionDto dto)
    {
        if (dto == null) return null;

        BdoExpressionCondition poco = new();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoExpressionCondition UpdateFromDto(
        this IBdoExpressionCondition poco,
        ExpressionConditionDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        poco.Expression = dto.ExpressionItem.ToPoco();
        poco.Identifier = dto.Identifier;
        poco.Name = dto.Name;
        poco.Parent = null;

        return poco;
    }
}
