using AutoMapper;
using BindOpen.Script;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ReferenceConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ReferenceDto ToDto(this IBdoReference poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoReference, ReferenceDto>()
                    .ForMember(q => q.Word, opt => opt.MapFrom(q => q.Word.ToDto()))
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<ReferenceDto>(poco);

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoReference ToPoco(
            this ReferenceDto dto)
        {
            if (dto == null) return null;

            var poco = new BdoReference()
            {
                Kind = dto.Kind,
                Text = dto.Text,
                Word = dto.Word.ToPoco()
            };

            return poco;
        }
    }
}
