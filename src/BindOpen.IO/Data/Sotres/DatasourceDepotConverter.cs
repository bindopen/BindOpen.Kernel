using BindOpen.Data.Meta;
using BindOpen.Data.Items;
using System.Linq;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This static class represents a data reference converter.
    /// </summary>
    public static class DatasourceDepotConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static BdoDatasourceDepotDto ToDto(this IBdoSourceDepot poco)
        {
            if (poco == null) return null;

            BdoDatasourceDepotDto dto = new()
            {
                Id = poco.Id,
                Sources = poco.Items?.Select(q=> q?.ToDto()).ToList()
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoSourceDepot ToPoco(this BdoDatasourceDepotDto dto)
        {
            if (dto == null) return null;

            BdoDatasourceDepot poco = new();
            poco                
                .WithItems(dto.Sources?.Select(q => q?.ToPoco()).ToArray())
                .WithId(dto.Id);
            
            return poco;
        }
    }
}
