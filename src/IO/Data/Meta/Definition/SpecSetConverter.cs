using AutoMapper;
using BindOpen.Kernel.Data.Meta;
using System.Linq;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class SpecSetConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static SpecSetDto ToDto(this IBdoSpecSet poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoSpecSet, SpecSetDto>()
                    .ForMember(q => q.Items, opt => opt.Ignore())
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<SpecSetDto>(poco);

            dto.Items = poco.Items?.Select(q => q.ToDto()).ToList();

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoSpecSet ToPoco(
            this SpecSetDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<SpecSetDto, BdoSpecSet>()
                    .ForMember(q => q.Items, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoSpecSet>(dto);

            poco.With(dto.Items?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
