using AutoMapper;
using BindOpen.Data;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaSetConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaSetDto ToDto(this IBdoMetaSet poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoMetaSet, MetaSetDto>()
                    .ForMember(q => q.DataExpression, opt => opt.MapFrom(q => q.DataExpression.ToDto()))
            //.ForMember(q => q.Specs, opt => opt.Ignore())
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<MetaSetDto>(poco);

            dto.MetaItems = poco.Items?.Select(q => q.ToDto()).ToList();

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaSet ToPoco(
            this MetaSetDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<MetaSetDto, BdoMetaSet>()
                    .ForMember(q => q.DataExpression, opt => opt.Ignore())
                    .ForMember(q => q.Specs, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoMetaSet>(dto);

            poco.DataExpression = dto.DataExpression.ToPoco();
            //poco.Specs = dto.Specs?.Count == 0 ? null : dto.Specs?.Select(q => q.ToPoco()).Cast<IBdoSpec>().ToList();
            poco.With(dto.MetaItems?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
