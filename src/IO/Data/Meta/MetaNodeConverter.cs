using AutoMapper;
using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Meta.Reflection;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaNodeConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaNodeDto ToDto(this IBdoMetaNode poco)
        {
            if (poco == null) return null;

            poco.UpdateTree();

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoMetaNode, MetaNodeDto>()
                    .ForMember(q => q.ClassReference, opt => opt.Ignore())
                    .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToDto()))
                    .ForMember(q => q.MetaItems, opt => opt.Ignore())
                    .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.Spec.ToDto()))
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<MetaNodeDto>(poco);

            dto.ClassReference = poco.DataType.IsSpecified() ? poco?.DataType.ToDto() : null;
            dto.DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName;

            dto.MetaItems = poco.Items?.Select(q => q.ToDto()).ToList();
            dto.ValueType = poco?.DataType.ValueType ?? DataValueTypes.Any;
            if (poco.Spec?.DataType.ValueType == poco.DataType?.ValueType)
            {
                dto.ValueType = DataValueTypes.Any;
            }

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaNode ToPoco(
            this MetaNodeDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<MetaNodeDto, BdoMetaNode>()
                    .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToPoco()))
                    .ForMember(q => q.DataType, opt => opt.Ignore())
                    .ForMember(q => q.Items, opt => opt.Ignore())
                    .ForMember(q => q.Parent, opt => opt.Ignore())
                    .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.Spec.ToPoco()))
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoMetaNode>(dto);

            poco.DataType = new BdoDataType(dto?.ClassReference?.ToPoco());
            poco.DataType.DefinitionUniqueName = dto.DefinitionUniqueName;
            poco.DataType.ValueType = dto.ValueType;

            poco.With(dto.MetaItems?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
