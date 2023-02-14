using AutoMapper;
using BindOpen.Data;
using BindOpen.Data.Configuration;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Scripting;
using BindOpen.Runtime.Scopes;
using System.Linq;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// This class represents a connector converter.
    /// </summary>
    public static class BdoConnectorConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ConnectorConfigurationDto ToDto(this IBdoConnector poco)
        {
            if (poco == null) return null;

            var items = poco.ToMetaArray()?.Select(p => p.ToDto()).ToList();

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<IBdoConnector, ConnectorConfigurationDto>()
                    .ForMember(q => q.DataReference, opt => opt.Ignore())
                    .ForMember(q => q.DefinitionUniqueName, opt => opt.MapFrom(q => q.Definition == null ? null : q.Definition.UniqueName))
                    .ForMember(q => q.Description, opt => opt.Ignore())
                    .ForMember(q => q.Items, opt => opt.MapFrom(q => items))
                    .ForMember(q => q.Title, opt => opt.Ignore())
                    .ForMember(q => q.UsedItemIds, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var dto = mapper.Map<ConnectorConfigurationDto>(poco);

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoConnector ToPoco(
            this ConnectorConfigurationDto dto,
            IBdoScope scope)
        {
            if (dto == null) return null;

            var metas = dto.Items.Select(p => p.ToPoco()).ToArray();
            var config = BdoConfig.New(metas);

            var poco = scope.CreateConnector(config);

            return poco;
        }
    }
}
