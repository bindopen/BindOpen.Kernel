using AutoMapper;
using BindOpen.System.Data.Helpers;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ConfigurationConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ConfigurationDto ToDto(this IBdoConfiguration poco) => poco.ToDto<ConfigurationDto>();

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static T ToDto<T>(this IBdoConfiguration poco)
            where T : ConfigurationDto
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<IBdoConfiguration, T>()
                    .ForMember(q => q.CreationDate, opt => opt.MapFrom(q => StringHelper.ToString(q.CreationDate)))
                    .ForMember(q => q.DataReference, opt => opt.MapFrom(q => q.Reference.ToDto()))
                    .ForMember(q => q.Description, opt => opt.MapFrom(q => q.Description.ToDto()))
                    .ForMember(q => q.MetaItems, opt => opt.MapFrom(q => q.Items == null ? null : q.Items.Select(q => q.ToDto()).ToList()))
                    .ForMember(q => q.LastModificationDate, opt => opt.MapFrom(q => StringHelper.ToString(q.CreationDate)))
                    .ForMember(q => q.Title, opt => opt.MapFrom(q => q.Title.ToDto()))
                    .ForMember(q => q.Specs, opt => opt.Ignore())
                    .ForMember(q => q.UsedItemIds, opt => opt.MapFrom(q => q.UsedItemIds == null ? null : q.UsedItemIds.Select(q => q).ToList()))
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<T>(poco);

            dto.Specs = poco.Specs?.Select(q => q.ToDto()).ToList();

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoConfiguration ToPoco(this ConfigurationDto dto) => dto.ToPoco<IBdoConfiguration>();

        /// <summary>
        /// Converts to POCO.
        /// </summary>
        /// <param key="dto">The dto to consider.</param>
        /// <returns>The POCO object.</returns>
        public static T ToPoco<T>(this ConfigurationDto dto)
            where T : IBdoConfiguration
        {
            if (dto == null) return default;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<ConfigurationDto, T>()
                    .ForMember(q => q.CreationDate, opt => opt.MapFrom(q => q.CreationDate.ToDateTime(null)))
                    .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.DataReference == null ? null : q.DataReference.ToPoco()))
                    .ForMember(q => q.Description, opt => opt.Ignore())
                    .ForMember(q => q.Items, opt => opt.Ignore())
                    .ForMember(q => q.LastModificationDate, opt => opt.MapFrom(q => q.CreationDate.ToDateTime(null)))
                    .ForMember(q => q.Parent, opt => opt.Ignore())
                    .ForMember(q => q.Specs, opt => opt.Ignore())
                    .ForMember(q => q.Title, opt => opt.Ignore())
                    .ForMember(q => q.UsedItemIds, opt => opt.MapFrom(q => q.UsedItemIds == null ? null : q.UsedItemIds.Select(q => q).ToList()))
            );

            var mapper = new Mapper(config);
            var poco = mapper.Map<T>(dto);
            poco
                .WithTitle(dto.Title.ToPoco())
                .WithDescription(dto.Description.ToPoco())
                .With(dto.MetaItems.Select(q => q.ToPoco()).ToArray());

            var specs = dto.Specs?.Select(q => q.ToPoco())?.ToArray();
            poco.Specs = specs?.Length == 0 ? null : Data.BdoData.NewSet<IBdoSpec>(specs);

            return poco;
        }
    }
}
