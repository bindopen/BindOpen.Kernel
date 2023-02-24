using AutoMapper;
using BindOpen.Data.Helpers;
using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data.Configuration
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
        public static ConfigurationDto ToDto(this IBdoConfiguration poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoConfiguration, ConfigurationDto>()
                    .ForMember(q => q.CreationDate, opt => opt.MapFrom(q => StringHelper.ToString(q.CreationDate)))
                    .ForMember(q => q.DataExpression, opt => opt.MapFrom(q => q.DataExpression.ToDto()))
                    .ForMember(q => q.Description, opt => opt.MapFrom(q => q.Description.ToDto()))
                    .ForMember(q => q.MetaItems, opt => opt.MapFrom(q => q.Items == null ? null : q.Items.Select(q => q.ToDto()).ToList()))
                    .ForMember(q => q.LastModificationDate, opt => opt.MapFrom(q => StringHelper.ToString(q.CreationDate)))
                    .ForMember(q => q.Title, opt => opt.MapFrom(q => q.Title.ToDto()))
                    .ForMember(q => q.UsedItemIds, opt => opt.MapFrom(q => q.UsedItemIds == null ? null : q.UsedItemIds.Select(q => q).ToList()))
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<ConfigurationDto>(poco);

            return dto;
        }

        /// <summary>
        /// Converts to POCO.
        /// </summary>
        /// <param key="dto">The dto to consider.</param>
        /// <returns>The POCO object.</returns>
        public static IBdoConfiguration ToPoco(
            this ConfigurationDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<ConfigurationDto, BdoConfiguration>()
                    .ForMember(q => q.CreationDate, opt => opt.MapFrom(q => q.CreationDate.ToDateTime(null)))
                    .ForMember(q => q.DataExpression, opt => opt.MapFrom(q => q.DataExpression == null ? null : q.DataExpression.ToPoco()))
                    .ForMember(q => q.Description, opt => opt.Ignore())
                    .ForMember(q => q.Items, opt => opt.Ignore())
                    .ForMember(q => q.LastModificationDate, opt => opt.MapFrom(q => q.CreationDate.ToDateTime(null)))
                    .ForMember(q => q.Parent, opt => opt.Ignore())
                    .ForMember(q => q.Specs, opt => opt.Ignore())
                    .ForMember(q => q.Title, opt => opt.Ignore())
                    .ForMember(q => q.UsedItemIds, opt => opt.MapFrom(q => q.UsedItemIds == null ? null : q.UsedItemIds.Select(q => q).ToList()))
            );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoConfiguration>(dto);
            poco
                .WithTitle(dto.Title.ToPoco())
                .WithDescription(dto.Description.ToPoco())
                .With(dto.MetaItems.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
