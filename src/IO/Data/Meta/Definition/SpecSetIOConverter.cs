using AutoMapper;
using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a IO converter of specification sets.
    /// </summary>
    public static class SpecSetIOConverter
    {
        /// <summary>
        /// Converts a specification set poco into a DTO one.
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
        /// Converts a specification set DTO to a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
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
