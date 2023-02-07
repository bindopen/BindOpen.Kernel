using AutoMapper;
using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Data.References;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaScalarConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaScalarDto ToDto(this IBdoMetaScalar poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoMetaScalar, MetaScalarDto>()
                    .ForMember(q => q.DataReference, opt => opt.MapFrom(q => q.Reference.ToDto()))
                    .ForMember(q => q.Specs, opt => opt.Ignore())
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<MetaScalarDto>(poco);

            //dto.WithSpecifications(poco.Specs.Select(q => q?.ToDto()).Cast<IBdoDataElementSpec>().ToArray());

            if (poco.ItemizationMode == DataItemizationMode.Value)
            {
                var dataList = poco.GetDataList<object>().Select(q => q.ToString(poco.ValueType)).ToList();
                if (dataList.Count == 1)
                {
                    dto.Item = dataList.FirstOrDefault();
                }
                else
                {
                    dto.Items = dataList;
                }
            }

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaScalar ToPoco(this MetaScalarDto dto)
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
            poco.Specs = dto.Specs?.Select(q => q.ToPoco()).ToList();

            if (!string.IsNullOrEmpty(dto.Item))
            {
                poco.WithData(dto.Item);
            }
            else
            {
                var objects = dto.Items.Select(q => q.ToObject(poco.ValueType)).ToList();
                poco.WithDataList(objects);
            }

            return poco;
        }
    }
}
