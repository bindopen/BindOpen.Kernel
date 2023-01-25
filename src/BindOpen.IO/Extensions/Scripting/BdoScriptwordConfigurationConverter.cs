using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Items;
using System.Linq;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This class represents a task configuration converter.
    /// </summary>
    public static class BdoScriptwordConfigurationConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static BdoScriptwordConfigurationDto ToDto(this IBdoScriptwordConfiguration poco)
        {
            if (poco == null) return null;

            BdoScriptwordConfigurationDto dto = new()
            {
                CreationDate = poco.CreationDate.ToString(DataValueTypes.Date),
                DefinitionUniqueId = poco.DefinitionUniqueId,
                Description = poco.Description?.ToDto(),
                GroupId = poco.GroupId,
                Elements = poco.Items?.Select(q => q?.ToDto()).ToList(),
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
        public static IBdoScriptwordConfiguration ToPoco(this BdoScriptwordConfigurationDto dto)
        {
            if (dto == null) return null;

            BdoScriptwordConfiguration poco = new(dto?.DefinitionUniqueId)
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
                .Add(dto.Elements?.Select(q => q?.ToPoco()).ToArray());

            return poco;
        }
    }
}
