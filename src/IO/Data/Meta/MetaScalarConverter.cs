using AutoMapper;
using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Helpers;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaScalarConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaScalarDto ToDto(this IBdoMetaScalar poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoMetaScalar, MetaScalarDto>()
                    .ForMember(q => q.ClassReference, opt => opt.Ignore())
                    .ForMember(q => q.Item, opt => opt.Ignore())
                    .ForMember(q => q.DataReference, opt => opt.MapFrom(q => q.DataReference.ToDto()))
                    .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.Spec.ToDto()))
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<MetaScalarDto>(poco);

            dto.ClassReference = poco.DataType.IsSpecified() ? poco?.DataType.ToDto() : null;
            dto.DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName;

            var dataList = poco.GetDataList<object>()?.Select(q => q.ToString(dto.ValueType)).ToList();
            if (dataList?.Count > 1)
            {
                dto.Items = dataList;
            }
            else
            {
                dto.Item = dataList?.FirstOrDefault();
            }

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
        public static IBdoMetaScalar ToPoco(
            this MetaScalarDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<MetaScalarDto, BdoMetaScalar>()
                    .ForMember(q => q.DataReference, opt => opt.MapFrom(q => q.DataReference.ToPoco()))
                    .ForMember(q => q.DataType, opt => opt.Ignore())
                    .ForMember(q => q.Parent, opt => opt.Ignore())
                    .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.Spec.ToPoco()))
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoMetaScalar>(dto);

            poco.DataType = new BdoDataType(dto?.ClassReference?.ToPoco());
            poco.DataType.DefinitionUniqueName = dto.DefinitionUniqueName;
            poco.DataType.ValueType = dto.ValueType;

            if (!string.IsNullOrEmpty(dto.Item))
            {
                poco.WithData(dto.Item.ToObject(poco.DataType.ValueType));
            }
            else
            {
                var objects = dto.Items?.Select(q => q.ToObject(poco.DataType.ValueType)).ToList();
                poco.WithData(objects);
            }

            return poco;
        }
    }
}
