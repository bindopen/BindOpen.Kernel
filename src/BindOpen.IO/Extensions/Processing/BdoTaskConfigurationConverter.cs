using BindOpen.Data;
using BindOpen.Data.Elements;
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
                CarrierElements = poco.Items?.Where(q => q is CarrierElement).Cast<CarrierElement>().Select(q => q?.ToDto()).ToList(),
                CollectionElements = poco.Items?.Where(q => q is CollectionElement).Cast<CollectionElement>().Select(q => q?.ToDto()).ToList(),
                CreationDate = poco.CreationDate.ToString(DataValueTypes.Date),
                DefinitionUniqueId = poco.DefinitionUniqueId,
                Description = poco.Description?.ToDto(),
                GroupId = poco.GroupId,
                Id = poco.Id,
                InputDetail = poco.InputDetail?.ToDto(),
                LastModificationDate = poco.LastModificationDate.ToString(DataValueTypes.Date),
                Name = poco.Name,
                ObjectElements = poco.Items?.Where(q => q is ObjectElement).Cast<ObjectElement>().Select(q => q?.ToDto()).ToList(),
                OutputDetail = poco.OutputDetail?.ToDto(),
                ScalarElements = poco.Items?.Where(q => q is ScalarElement).Cast<ScalarElement>().Select(q => q?.ToDto()).ToList(),
                SourceElements = poco.Items?.Where(q => q is SourceElement).Cast<SourceElement>().Select(q => q?.ToDto()).ToList(),
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
                .Add(dto.CarrierElements?.Select(q => q?.ToPoco()).ToArray())
                .Add(dto.CollectionElements?.Select(q => q?.ToPoco()).ToArray())
                .Add(dto.ObjectElements?.Select(q => q?.ToPoco()).ToArray())
                .Add(dto.ScalarElements?.Select(q => q?.ToPoco()).ToArray())
                .Add(dto.SourceElements?.Select(q => q?.ToPoco()).ToArray());

            return poco;
        }
    }
}
