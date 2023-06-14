using AutoMapper;
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
                    .ForMember(q => q.DataReference, opt => opt.MapFrom(q => q.Reference.ToDto()))
                    .ForMember(q => q.Specs, opt => opt.MapFrom(q => q.Specs == null ? null : q.Specs.Select(q => q.ToDto()).ToList()))
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<MetaScalarDto>(poco);

            if (poco.DataMode == DataMode.Value)
            {
                var dataList = poco.GetDataList<object>()?.Select(q => q.ToString()).ToList();
                if (dataList?.Count > 1)
                {
                    dto.Items = dataList;
                }
                else
                {
                    dto.Item = dataList?.FirstOrDefault();
                }
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
                    .ForMember(q => q.Reference, opt => opt.Ignore())
                    .ForMember(q => q.Specs, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoMetaScalar>(dto);

            poco.Reference = dto.DataReference.ToPoco();
            poco.Specs = BdoMeta.NewSpecSet(dto.Specs.Select(q => q.ToPoco()).ToArray());

            if (!string.IsNullOrEmpty(dto.Item))
            {
                poco.WithData(dto.Item.ToObject(poco.GetSpec()?.DataType.ValueType ?? DataValueTypes.None));
            }
            else
            {
                var objects = dto.Items?.Select(q => q.ToObject(poco.GetSpec()?.DataType.ValueType ?? DataValueTypes.None)).ToList();
                poco.WithData(objects);
            }

            return poco;
        }
    }
}
