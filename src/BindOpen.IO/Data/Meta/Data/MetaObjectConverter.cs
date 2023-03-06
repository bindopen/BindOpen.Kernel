using AutoMapper;
using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaObjectConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaObjectDto ToDto(this IBdoMetaObject poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoMetaObject, MetaObjectDto>()
                    .ForMember(q => q.DataExpression, opt => opt.MapFrom(q => q.DataExpression.ToDto()))
                    .ForMember(q => q.MetaItems, opt => opt.Ignore())
                    .ForMember(q => q.Item, opt => opt.Ignore())
                    .ForMember(q => q.SubSet, opt => opt.Ignore())
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<MetaObjectDto>(poco);

            dto.MetaItems = poco.Items?.Select(q => q.ToDto()).ToList();

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaObject ToPoco(
            this MetaObjectDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<MetaObjectDto, BdoMetaObject>()
                    .ForMember(q => q.DataExpression, opt => opt.Ignore())
                    .ForMember(q => q.SpecSet, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoMetaObject>(dto);

            poco.DataExpression = dto.DataExpression.ToPoco();
            //poco.Specs = dto.Specs?.Count == 0 ? null : dto.Specs?.Select(q => q.ToPoco()).Cast<IBdoSpec>().ToList();
            poco.With(dto.MetaItems?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
