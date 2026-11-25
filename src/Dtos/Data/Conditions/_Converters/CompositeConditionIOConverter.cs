using System.Linq;

namespace BindOpen.Data.Conditions;

/// <summary>
/// This class represents a IO converter of assembly references.
/// </summary>
public static class CompositeConditionIOConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static CompositeConditionDto ToDto(this IBdoCompositeCondition poco)
    {
        if (poco == null) return null;

        CompositeConditionDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static CompositeConditionDto UpdateFromPoco(
        this CompositeConditionDto dto,
        IBdoCompositeCondition poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.CompositionKind = poco.CompositionKind;
        dto.Children = [.. poco._Children.Select(q => q.ToDto())];
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
    public static IBdoCompositeCondition ToPoco(
        this CompositeConditionDto dto)
    {
        if (dto == null) return null;

        BdoCompositeCondition poco = new();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoCompositeCondition UpdateFromDto(
        this IBdoCompositeCondition poco,
        CompositeConditionDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        poco.CompositionKind = dto.CompositionKind;
        poco._Children = new TBdoSet<IBdoCondition>(dto.Children.Select(q => q.ToPoco()));
        poco.Identifier = dto.Identifier;
        poco.Name = dto.Name;
        poco.Parent = null;

        return poco;
    }
}
