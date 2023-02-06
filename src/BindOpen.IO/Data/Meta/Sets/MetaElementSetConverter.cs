using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaElementSetConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaListDto ToDto(this IBdoMetaList poco)
        {
            if (poco == null) return null;

            MetaListDto dto = new()
            {
                Id = poco.Id,
                Items = poco.Items?.Select(q => q.ToDto()).ToList()
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaList ToPoco(this MetaListDto dto)
        {
            if (dto == null) return null;

            BdoMetaList poco = new();
            poco.WithId(dto.Id);

            poco.Add(dto.Items?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
