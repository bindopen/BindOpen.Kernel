using System.Linq;

namespace BindOpen.MetaData.Elements
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
        public static BdoElementSetDto ToDto(this IBdoMetaElementSet poco)
        {
            if (poco == null) return null;

            BdoElementSetDto dto = new()
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
        public static IBdoMetaElementSet ToPoco(this BdoElementSetDto dto)
        {
            if (dto == null) return null;

            BdoMetaElementSet poco = new();
            poco.WithId(dto.Id);

            poco.Add(dto.Elements?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
