using AutoMapper;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a IO converter of meta sets.
    /// </summary>
    public static class MetaSetConverter
    {
        /// <summary>
        /// Converts an expression poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaSetDto ToDto(this IBdoMetaSet poco)
        {
            MetaSetDto dto = new();
            dto.UpdateFromPoco(poco);

            return dto;
        }

        public static MetaSetDto UpdateFromPoco(
            this MetaSetDto dto,
            IBdoMetaSet poco)
        {
            if (dto == null) return null;

            if (poco == null) return dto;

            poco.UpdateTrees();

            MapperConfiguration config;

            config = new MapperConfiguration(
                cfg => cfg.CreateMap<IBdoMetaSet, MetaSetDto>()
                    .ForMember(q => q.Items, opt => opt.Ignore())
            );

            var mapper = new Mapper(config);
            mapper.Map(poco, dto);

            dto.Items = poco.Items?.Select(q => q.ToDto()).ToList();

            return dto;
        }

        /// <summary>
        /// Converts a meta set DTO to a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
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
