using AutoMapper;
using BindOpen.Data.Helpers;
using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
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
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ConfigurationDto ToDto(this IBdoConfiguration poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoConfiguration, ConfigurationDto>()
                    .ForMember(q => q.CreationDate, opt => opt.MapFrom(q => StringHelper.ToString(q.CreationDate)))
                    .ForMember(q => q.DataExpression, opt => opt.MapFrom(q => q.DataExpression.ToDto()))
                    .ForMember(q => q.Description, opt => opt.MapFrom(q => q.Description.ToDto()))
                    .ForMember(q => q.Items, opt => opt.MapFrom(q => q.Items == null ? null : q.Items.Select(q => q.ToDto()).ToList()))
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
        /// <param name="dto">The dto to consider.</param>
        /// <returns>The POCO object.</returns>
        public static IBdoConfiguration ConvertToPoco(
            this IBdoScope scope,
            ConfigurationDto dto,
            IBdoLog log = null)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<ConfigurationDto, BdoConfiguration>()
                    .ForMember(q => q.CreationDate, opt => opt.MapFrom(q => q.CreationDate.ToDateTime(null)))
                    .ForMember(q => q.DataExpression, opt => opt.MapFrom(q => scope.ConvertToPoco(q.DataExpression, log)))
                    .ForMember(q => q.Description, opt => opt.MapFrom(q => q.Description.ToPoco()))
                    .ForMember(q => q.Items, opt => opt.MapFrom(q => q.Items == null ? null : q.Items.Select(q => scope.ConvertToPoco(q, log)).ToList()))
                    .ForMember(q => q.LastModificationDate, opt => opt.MapFrom(q => q.CreationDate.ToDateTime(null)))
                    .ForMember(q => q.Title, opt => opt.MapFrom(q => q.Title.ToPoco()))
                    .ForMember(q => q.UsedItemIds, opt => opt.MapFrom(q => q.UsedItemIds == null ? null : q.UsedItemIds.Select(q => q).ToList()))
            );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoConfiguration>(dto);

            return poco;
        }
    }
}
