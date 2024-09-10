using AutoMapper;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// This class represents a IO converter of assembly references.
/// </summary>
public static class AssemblyReferenceConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static AssemblyReferenceDto ToDto(this IBdoAssemblyReference poco)
    {
        AssemblyReferenceDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    public static AssemblyReferenceDto UpdateFromPoco(
        this AssemblyReferenceDto dto,
        IBdoAssemblyReference poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<BdoAssemblyReference, AssemblyReferenceDto>()
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
    public static IBdoAssemblyReference ToPoco(
        this AssemblyReferenceDto dto)
    {
        if (dto == null) return null;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<AssemblyReferenceDto, BdoAssemblyReference>()
        );

        var mapper = new Mapper(config);
        var poco = mapper.Map<BdoAssemblyReference>(dto);

        return poco;
    }
}
