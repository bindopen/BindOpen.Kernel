namespace BindOpen.Data.Conditions;

/// <summary>
/// This abstract class represents a IO converter of conditions.
/// </summary>
public static class ConditionIOConverter
{
    /// <summary>
    /// Converts a condition poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static ConditionDto ToDto(this IBdoCondition poco)
    {
        if (poco is IBdoBasicCondition basicCondition)
        {
            return basicCondition.ToDto();
        }
        else if (poco is IBdoCompositeCondition advancedCondition)
        {
            return advancedCondition.ToDto();
        }
        else if (poco is IBdoExpressionCondition referenceCondition)
        {
            return referenceCondition.ToDto();
        }

        return null;
    }

    public static ConditionDto UpdateFromPoco(
        this ConditionDto dto,
        IBdoCondition poco)
    {
        if (poco is IBdoBasicCondition basicCondition)
        {
            return dto.UpdateFromPoco(basicCondition);
        }
        else if (poco is IBdoCompositeCondition advancedCondition)
        {
            return dto.UpdateFromPoco(advancedCondition);
        }
        else if (poco is IBdoExpressionCondition referenceCondition)
        {
            return dto.UpdateFromPoco(referenceCondition);
        }

        return null;
    }

    /// <summary>
    /// Converts a condition DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoCondition ToPoco(
        this ConditionDto dto)
    {
        if (dto == null) return null;

        if (dto is BasicConditionDto basicConditionDto)
        {
            return basicConditionDto.ToPoco();
        }
        else if (dto is CompositeConditionDto advancedConditionDto)
        {
            return advancedConditionDto.ToPoco();
        }
        else if (dto is ExpressionConditionDto expressionConditionDto)
        {
            return expressionConditionDto.ToPoco();
        }

        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoCompositeCondition UpdateFromDto(
        this IBdoCompositeCondition poco,
        ConditionDto dto)
    {
        if (dto is BasicConditionDto basicConditionDto)
        {
            return poco.UpdateFromDto(basicConditionDto);
        }
        else if (dto is CompositeConditionDto advancedConditionDto)
        {
            return poco.UpdateFromDto(advancedConditionDto);
        }
        else if (dto is ExpressionConditionDto expressionConditionDto)
        {
            return poco.UpdateFromDto(expressionConditionDto);
        }

        return null;
    }
}
