using AutoMapper;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Scoping.Script;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This class represents a IO converter of references.
    /// </summary>
    public static class ReferenceConverter
    {
        /// <summary>
        /// Converts a reference poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ReferenceDto ToDto(this IBdoReference poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoReference, ReferenceDto>()
                    .ForMember(q => q.Expression, opt => opt.MapFrom(q => q.Expression.ToDto()))
                    .ForMember(q => q.MetaData, opt => opt.MapFrom(q => q.MetaData.ToDto()))
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<ReferenceDto>(poco);

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
}
