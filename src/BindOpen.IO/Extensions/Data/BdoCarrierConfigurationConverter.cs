using BindOpen.Extensions.Connecting;
using BindOpen.Data;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using System.Linq;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a carrier configuration converter.
    /// </summary>
    public static class BdoCarrierConfigurationConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static BdoCarrierConfigurationDto ToDto(this IBdoCarrierConfiguration poco)
        {
            if (poco == null) return null;

            BdoCarrierConfigurationDto dto = new()
            {
                CarrierElements = poco.Items?.Where(q => q is CarrierElement).Cast<CarrierElement>().Select(q => q?.ToDto()).ToList(),
                CreationDate = poco.CreationDate.ToString(DataValueTypes.Date),
                DefinitionUniqueId = poco.DefinitionUniqueId,
                Description = poco.Description?.ToDto(),
                GroupId = poco.GroupId,
                Id = poco.Id,
                LastModificationDate = poco.LastModificationDate.ToString(DataValueTypes.Date),
                Name = poco.Name,
                Title = poco.Title?.ToDto()      
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoCarrierConfiguration ToPoco(this BdoCarrierConfigurationDto dto)
        {
            if (dto == null) return null;

            BdoCarrierConfiguration poco = new(dto?.DefinitionUniqueId)
            {                 
                CreationDate = dto.CreationDate.ToDateTime(),
                DefinitionUniqueId = dto.DefinitionUniqueId,
                Description = dto.Description?.ToPoco(),
                GroupId = dto.GroupId,
                LastModificationDate = dto.LastModificationDate.ToDateTime(),
                Name = dto.Name,
                Title = dto.Title?.ToPoco()
            };
            poco
                .WithId(dto.Id)
                .WithItems(dto.CarrierElements?.Select(q => q?.ToPoco()).Cast<IBdoElement>().ToArray());

            return poco;
        }
    }
}
