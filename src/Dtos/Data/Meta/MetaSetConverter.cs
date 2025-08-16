using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data.Meta;

/// <summary>
/// This class represents a IO converter of meta sets.
/// </summary>
public static class MetaSetConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static MetaSetDto ToDto(this IBdoMetaSet poco)
    {
        if (poco == null) return null;

        MetaSetDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static MetaSetDto UpdateFromPoco(
        this MetaSetDto dto,
        IBdoMetaSet poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.Name = poco.Name;
        dto.Identifier = poco.Identifier;
        dto.Items = poco.Items?.Select(q => q.ToDto()).ToList();

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoMetaSet ToPoco(
        this MetaSetDto dto)
    {
        if (dto == null) return null;

        BdoMetaSet poco = new();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoMetaSet UpdateFromDto(
        this IBdoMetaSet poco,
        MetaSetDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        poco.Name = dto.Name;
        poco.Identifier = dto.Identifier;
        poco.With(dto.Items?.Select(q => q.ToPoco()).ToArray());

        return poco;
    }
}
