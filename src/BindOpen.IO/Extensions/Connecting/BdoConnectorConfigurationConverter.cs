using BindOpen.Meta;
using BindOpen.Meta.Elements;
using BindOpen.Meta.Items;
using BindOpen.Extensions.Connecting;
using System.Linq;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// This class represents a connector configuration converter.
    /// </summary>
    public static class BdoConnectorConfigurationConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static BdoConnectorConfigurationDto ToDto(this IBdoConnectorConfiguration poco)
        {
            if (poco == null) return null;

            BdoConnectorConfigurationDto dto = new()
            {
                CreationDate = poco.CreationDate.ToString(DataValueTypes.Date),
                DefinitionUniqueId = poco.DefinitionUniqueId,
                Description = poco.Description?.ToDto(),
                GroupId = poco.GroupId,
                Id = poco.Id,
                LastModificationDate = poco.LastModificationDate.ToString(DataValueTypes.Date),
                Name = poco.Name,
                ScalarElements = poco.Items?.Where(q => q is BdoMetaScalar).Cast<BdoMetaScalar>().Select(q => q?.ToDto()).ToList(),
                Title = poco.Title?.ToDto()
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoConnectorConfiguration ToPoco(this BdoConnectorConfigurationDto dto)
        {
            if (dto == null) return null;

            BdoConnectorConfiguration poco = new(dto?.DefinitionUniqueId)
            {
                CreationDate = dto.CreationDate.ToDateTime(),
                DefinitionUniqueId = dto.DefinitionUniqueId,
                Description = dto.Description?.ToPoco(),
                GroupId = dto.GroupId,
                LastModificationDate = dto.LastModificationDate.ToDateTime(),
                Name = dto.Name,
                Title = dto.Title?.ToPoco()
            };
            poco.WithId(dto.Id)
                .WithItems(dto.ScalarElements?.Select(q => q?.ToPoco()).Cast<IBdoMetaElement>().ToArray());

            return poco;
        }
    }
}
