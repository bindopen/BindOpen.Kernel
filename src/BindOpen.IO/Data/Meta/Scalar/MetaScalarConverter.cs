using AutoMapper;
using BindOpen.Data.Items;
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
                    .ForMember(q => q.Description, opt => opt.MapFrom(q => q.Description.ToDto()))
                    .ForMember(q => q.Detail, opt => opt.MapFrom(q => q.Detail.ToDto()))
                    .ForMember(q => q.ItemReference, opt => opt.MapFrom(q => q.ItemReference.ToDto()))
                    .ForMember(q => q.Specs, opt => opt.Ignore())
                    .ForMember(q => q.Title, opt => opt.MapFrom(q => q.Title.ToDto()))
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<MetaScalarDto>(poco);

            //dto.WithSpecifications(poco.Specs.Select(q => q?.ToDto()).Cast<IBdoDataElementSpec>().ToArray());

            if (poco.ItemizationMode == DataItemizationMode.Value)
            {
                var values = poco.Items<object>().Select(q => q.ToString(poco.ValueType)).ToList();
                if (values.Count == 1)
                {
                    dto.Item = values.FirstOrDefault();
                }
                else
                {
                    dto.Items = values;
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
                    .ForMember(q => q.Description, opt => opt.Ignore())
                    .ForMember(q => q.Detail, opt => opt.Ignore())
                    .ForMember(q => q.ItemReference, opt => opt.Ignore())
                    .ForMember(q => q.Specs, opt => opt.Ignore())
                    .ForMember(q => q.Title, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoMetaScalar>(dto);

            poco.Description = dto.Description.ToPoco();
            poco.Detail = dto.Detail.ToPoco();
            poco.ItemReference = dto.ItemReference.ToPoco();
            poco.Specs = dto.Specs?.Select(q => q.ToPoco()).ToList();
            poco.Title = dto.Title.ToPoco();

            if (!string.IsNullOrEmpty(dto.Item))
            {
                poco.WithItems(dto.Item);
            }
            else
            {
                var objects = dto.Items.Select(q => q.ToObject(poco.ValueType)).ToList();
                poco.WithItems(objects);
            }

            return poco;
        }
    }
}
