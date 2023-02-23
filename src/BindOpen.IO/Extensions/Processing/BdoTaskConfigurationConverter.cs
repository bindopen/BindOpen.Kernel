using AutoMapper;
using BindOpen.Data;
using BindOpen.Data.Configuration;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Extensions.Connecting;
using System.Linq;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// This class represents a entity converter.
    /// </summary>
    public static class BdoTaskConfigurationConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static TaskConfigurationDto ToTaskDto(this IBdoConfiguration poco)
        {
            if (poco == null) return null;

            var items = poco
                .ToMetaData(true)
                .AsMetaSet()?.Select(p => p.ToDto()).ToList();

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<IBdoTask, TaskConfigurationDto>()
                    .ForMember(q => q.DataExpression, opt => opt.Ignore())
                    .ForMember(q => q.DefinitionUniqueName, opt => opt.MapFrom(q => q.DefinitionUniqueName))
                    .ForMember(q => q.Description, opt => opt.Ignore())
                    .ForMember(q => q.ExtensionKind, opt => opt.MapFrom(q => BdoExtensionKind.Task))
                    .ForMember(q => q.Items, opt => opt.MapFrom(q => items))
                    .ForMember(q => q.Title, opt => opt.Ignore())
                    .ForMember(q => q.UsedItemIds, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var dto = mapper.Map<TaskConfigurationDto>(poco);

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoConfiguration ToPoco(
            this TaskConfigurationDto dto)
        {
            if (dto == null) return null;

            var metas = dto.Items.Select(p => p.ToPoco()).ToArray();
            var poco = BdoConfig.New(metas);

            return poco;
        }
    }
}
