using AutoMapper;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// This class represents a IO converter of assembly references.
/// </summary>
public static class ClassReferenceIOConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static ClassReferenceDto ToDto(this IBdoClassReference poco)
    {
        ClassReferenceDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    public static ClassReferenceDto UpdateFromPoco(
        this ClassReferenceDto dto,
        IBdoClassReference poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<BdoClassReference, ClassReferenceDto>()
        );

        var mapper = new Mapper(config);
        mapper.Map(poco, dto);

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoClassReference ToPoco(
        this ClassReferenceDto dto)
    {
        if (dto == null) return null;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<ClassReferenceDto, BdoClassReference>()
        );

        var mapper = new Mapper(config);
        var poco = mapper.Map<BdoClassReference>(dto);

        return poco;
    }
}
