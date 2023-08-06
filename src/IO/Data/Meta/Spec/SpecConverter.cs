using AutoMapper;
using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Helpers;
using BindOpen.System.Data.Meta.Reflection;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class SpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static SpecDto ToDto(this IBdoSpec poco)
        {
            if (poco == null) return null;

            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoSpec, SpecDto>()
                    .ForMember(q => q.ClassReference, opt => opt.Ignore())
                    .ForMember(q => q.DefaultItems, opt => opt.Ignore())
                    .ForMember(q => q.Description, opt => opt.MapFrom(q => q.Description.ToDto()))
                    .ForMember(q => q.Detail, opt => opt.MapFrom(q => q.Detail.ToDto()))
                    .ForMember(q => q.Title, opt => opt.MapFrom(q => q.Title.ToDto()))
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<SpecDto>(poco);

            dto.ValueType = poco?.DataType.ValueType ?? DataValueTypes.Any;
            dto.ClassReference = poco?.DataType.ClassReference?.ToDto();

            var dataList = poco.DefaultData?.ToObjectList().Select(q => q?.ToMeta().ToDto()).ToList();
            dto.DefaultItems = dataList;

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoSpec ToPoco(
            this SpecDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<SpecDto, BdoSpec>()
                    .ForMember(q => q.DataType, opt => opt.Ignore())
                    .ForMember(q => q.Description, opt => opt.Ignore())
                    .ForMember(q => q.DefaultData, opt => opt.Ignore())
                    .ForMember(q => q.Detail, opt => opt.Ignore())
                    .ForMember(q => q.Title, opt => opt.Ignore())
                    .ForMember(q => q.SubSpecs, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoSpec>(dto);

            poco.DataType = new BdoDataType()
            {
                ClassReference = dto.ClassReference.ToPoco(),
                ValueType = dto.ValueType
            };
            poco
                .WithTitle(dto.Title.ToPoco<string>())
                .WithDescription(dto.Description.ToPoco<string>())
                .WithDetail(dto.Detail.ToPoco());

            return poco;
        }
    }
}
