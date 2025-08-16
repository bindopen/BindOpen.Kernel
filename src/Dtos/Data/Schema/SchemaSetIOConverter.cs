using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a IO converter of schema sets.
/// </summary>
public static class SchemaSetIOConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static SchemaSetDto ToDto(this IBdoSchemaSet poco)
    {
        if (poco == null) return null;

        SchemaSetDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static SchemaSetDto UpdateFromPoco(
        this SchemaSetDto dto,
        IBdoSchemaSet poco)
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
    public static IBdoSchemaSet ToPoco(
        this SchemaSetDto dto)
    {
        if (dto == null) return null;

        BdoSchemaSet poco = new();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoSchemaSet UpdateFromDto(
        this IBdoSchemaSet poco,
        SchemaSetDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        poco.Name = dto.Name;
        poco.Identifier = dto.Identifier;
        poco.With(dto.Items?.Select(q => q.ToPoco()).ToArray());

        return poco;
    }
}
