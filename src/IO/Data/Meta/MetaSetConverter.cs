using AutoMapper;
using BindOpen.System.Data.Meta.Reflection;
using System.Linq;

namespace BindOpen.System.Data.Meta
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

            poco.UpdateTrees();

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoMetaSet, MetaSetDto>()
                    .ForMember(q => q.Items, opt => opt.Ignore())
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<MetaSetDto>(poco);

            dto.Items = poco.Items?.Select(q => q.ToDto()).ToList();

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
                    .ForMember(q => q.Items, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoMetaSet>(dto);

            poco.With(dto.Items?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
