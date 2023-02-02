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
        public static MetaSetDto ToDto(this IBdoMetaSet poco)
        {
            if (poco == null) return null;

            MetaSetDto dto = new()
            {
                Id = poco.Id,
                Elements = poco.Items?.Select(q => q.ToDto()).ToList()
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaSet ToPoco(this MetaSetDto dto)
        {
            if (dto == null) return null;

            BdoMetaSet poco = new();
            poco.WithId(dto.Id);

            poco.Add(dto.Elements?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
