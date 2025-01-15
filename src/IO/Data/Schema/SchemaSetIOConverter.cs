using AutoMapper;
using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a IO converter of schema sets.
/// </summary>
public static class SchemaSetIOConverter
{
    /// <summary>
    /// Converts a schema set poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static SchemaSetDto ToDto(this IBdoSchemaSet poco)
    {
        if (poco == null) return null;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<BdoSchemaSet, SchemaSetDto>()
                .ForMember(q => q.Items, opt => opt.Ignore())
        );

        var mapper = new Mapper(config);
        var dto = mapper.Map<SchemaSetDto>(poco);

        dto.Items = poco.Items?.Select(q => q.ToDto()).ToList();

        return dto;
    }

    /// <summary>
    /// Converts a schema set DTO to a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoSchemaSet ToPoco(
        this SchemaSetDto dto)
    {
        if (dto == null) return null;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<SchemaSetDto, BdoSchemaSet>()
                .ForMember(q => q.Items, opt => opt.Ignore())
            );

        var mapper = new Mapper(config);
        var poco = mapper.Map<BdoSchemaSet>(dto);

        poco.With(dto.Items?.Select(q => q.ToPoco()).ToArray());

        return poco;
    }
}
