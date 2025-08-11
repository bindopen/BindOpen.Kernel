using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;

namespace BindOpen.Data;

/// <summary>
/// This class represents a IO converter of references.
/// </summary>
public static class ReferenceIOConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static ReferenceDto ToDto(this IBdoReference poco)
    {
        if (poco == null) return null;

        ReferenceDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static ReferenceDto UpdateFromPoco(
        this ReferenceDto dto,
        IBdoReference poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        if (dto.Expression?.Identifier != poco?.Identifier)
        {
            dto.Expression = poco.Expression.ToDto();
        }
        else if (poco.Expression != null)
        {
            dto.Expression ??= new();
            dto.Expression.UpdateFromPoco(poco.Expression);
        }
        dto.Identifier = poco.Identifier;
        dto.Kind = poco.Kind;
        dto.MetaData = poco.MetaData.ToDto();

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoReference ToPoco(
        this ReferenceDto dto)
    {
        if (dto == null) return null;

        BdoReference poco = new();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoReference UpdateFromDto(
        this IBdoReference poco,
        ReferenceDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        poco.Identifier = dto.Identifier;
        poco.Expression = dto.Expression.ToPoco();
        poco.Kind = dto.Kind;
        poco.MetaData = dto.MetaData.ToPoco();

        return poco;
    }
}
