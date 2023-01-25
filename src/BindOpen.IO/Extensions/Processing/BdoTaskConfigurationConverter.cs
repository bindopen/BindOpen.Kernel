using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Items;
using System.Linq;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// This class represents a task configuration converter.
    /// </summary>
    public static class BdoTaskConfigurationConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static BdoTaskConfigurationDto ToDto(this IBdoTaskConfiguration poco)
        {
            if (poco == null) return null;

            BdoTaskConfigurationDto dto = new()
            {
                CreationDate = poco.CreationDate.ToString(DataValueTypes.Date),
                DefinitionUniqueId = poco.DefinitionUniqueId,
                Description = poco.Description?.ToDto(),
                Elements = poco.Items?.Select(q => q?.ToDto()).ToList(),
                GroupId = poco.GroupId,
                Id = poco.Id,
                InputDetail = poco.InputDetail?.ToDto(),
                LastModificationDate = poco.LastModificationDate.ToString(DataValueTypes.Date),
                Name = poco.Name,
                OutputDetail = poco.OutputDetail?.ToDto(),
                Title = poco.Title?.ToDto()
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoTaskConfiguration ToPoco(this BdoTaskConfigurationDto dto)
        {
            if (dto == null) return null;

            BdoTaskConfiguration poco = new(dto?.DefinitionUniqueId)
            {
                CreationDate = dto.CreationDate.ToDateTime(),
                DefinitionUniqueId = dto.DefinitionUniqueId,
                Description = dto.Description?.ToPoco(),
                GroupId = dto.GroupId,
                InputDetail = dto.InputDetail?.ToPoco(),
                LastModificationDate = dto.LastModificationDate.ToDateTime(),
                Name = dto.Name,
                OutputDetail = dto.OutputDetail?.ToPoco(),
                Title = dto.Title?.ToPoco()
            };
            poco
                .WithId(dto.Id)
                .Add(dto.Elements?.Select(q => q?.ToPoco()).ToArray());

            return poco;
        }
    }
}
