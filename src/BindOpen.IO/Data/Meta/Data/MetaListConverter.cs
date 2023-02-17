using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaListConverter
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
        public static IBdoMetaList ConvertToPoco(
            this IBdoScope scope,
            MetaListDto dto,
            IBdoLog log = null)
        {
            if (dto == null) return null;

            BdoMetaList poco = new();
            poco.WithId(dto.Id);

            poco.Add(dto.Items?.Select(q => scope.ConvertToPoco(q, log)).ToArray());

            return poco;
        }
    }
}
