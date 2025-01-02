using AutoMapper;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;

namespace BindOpen.Data;

/// <summary>
/// This class represents a IO converter of references.
/// </summary>
public static class ReferenceIOConverter
{
    /// <summary>
    /// Converts a reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static ReferenceDto ToDto(this IBdoReference poco)
    {
        ReferenceDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    public static ReferenceDto UpdateFromPoco(
        this ReferenceDto dto,
        IBdoReference poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<BdoReference, ReferenceDto>()
                .ForMember(q => q.Expression, opt => opt.Ignore())
                .ForMember(q => q.MetaData, opt => opt.Ignore())
        );

        var mapper = new Mapper(config);
        mapper.Map(poco, dto);

        // Expression

        if (dto.Expression?.Identifier != poco?.Identifier)
        {
            dto.Expression = poco.Expression.ToDto();
        }
        else if (poco.Expression != null)
        {
            dto.Expression ??= new();
            dto.Expression.UpdateFromPoco(poco.Expression);
        }

        return dto;
    }

    /// <summary>
    /// Converts a reference DTO to a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoReference ToPoco(
        this ReferenceDto dto)
    {
        if (dto == null) return null;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<ReferenceDto, BdoReference>()
                .ForMember(q => q.Expression, opt => opt.MapFrom(q => q.Expression.ToPoco()))
                .ForMember(q => q.MetaData, opt => opt.MapFrom(q => q.MetaData.ToPoco()))
        );

        var mapper = new Mapper(config);
        var poco = mapper.Map<BdoReference>(dto);

        return poco;
    }
}
