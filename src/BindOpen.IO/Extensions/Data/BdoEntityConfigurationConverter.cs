using BindOpen.Extensions.Connecting;
using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Items;
using System.Linq;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a entity configuration converter.
    /// </summary>
    public static class BdoEntityConfigurationConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static BdoEntityConfigurationDto ToDto(this IBdoEntityConfiguration poco)
        {
            if (poco == null) return null;

            BdoEntityConfigurationDto dto = new()
            {
                CreationDate = poco.CreationDate.ToString(DataValueTypes.Date),
                DefinitionUniqueId = poco.DefinitionUniqueId,
                Description = poco.Description?.ToDto(),
                Elements = poco.Items?.Select(q => q?.ToDto()).ToList(),
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
        public static IBdoEntityConfiguration ToPoco(this BdoEntityConfigurationDto dto)
        {
            if (dto == null) return null;

            BdoEntityConfiguration poco = new(dto?.DefinitionUniqueId)
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
                .WithItems(dto.Elements?.Select(q => q?.ToPoco()).Cast<IBdoMetaData>().ToArray());

            return poco;
        }
    }
}
