using AutoMapper;
using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Meta.Reflection;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaCompositeConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaCompositeDto ToDto(this IBdoMetaComposite poco)
        {
            if (poco == null) return null;

            poco.UpdateTree();

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoMetaComposite, MetaCompositeDto>()
                    .ForMember(q => q.ClassReference, opt => opt.Ignore())
                    .ForMember(q => q.MetaItems, opt => opt.Ignore())
                    .ForMember(q => q.DataReference, opt => opt.MapFrom(q => q.DataReference.ToDto()))
                    .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.ToDto()))
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<MetaCompositeDto>(poco);

            dto.ClassReference = poco?.DataType.ClassReference?.ToDto();
            dto.DataReference = poco.DataReference?.ToDto();
            dto.MetaItems = poco.Items?.Select(q => q.ToDto()).ToList();
            if (poco.Spec?.DataType.ValueType == poco.DataType.ValueType)
            {
                dto.ValueType = DataValueTypes.Any;
            }
            dto.ValueType = poco?.DataType.ValueType ?? DataValueTypes.Any;

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaComposite ToPoco(
            this MetaCompositeDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<MetaCompositeDto, BdoMetaComposite>()
                    .ForMember(q => q.DataType, opt => opt.Ignore())
                    .ForMember(q => q.Items, opt => opt.Ignore())
                    .ForMember(q => q.Parent, opt => opt.Ignore())
                    .ForMember(q => q.DataReference, opt => opt.Ignore())
                    .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.ToPoco()))
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoMetaComposite>(dto);

            poco.DataReference = dto.DataReference.ToPoco();
            poco.DataType = new BdoDataType()
            {
                ClassReference = dto.ClassReference.ToPoco(),
                ValueType = dto.ValueType
            };

            poco.With(dto.MetaItems?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
