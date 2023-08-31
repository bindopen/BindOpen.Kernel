using System.Linq;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class DictionaryConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static DictionaryDto ToDto<TItem>(this ITBdoDictionary<TItem> poco)
        {
            if (poco == null) return null;

            DictionaryDto dto = new()
            {
                Id = poco.Id,
                Values = poco?.Select(q => q.ToDto()).ToList()
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ITBdoDictionary<TItem> ToPoco<TItem>(this DictionaryDto dto)
        {
            if (dto == null) return null;

            TBdoDictionary<TItem> poco = BdoData.NewDictionary(dto.Values?.Select(q => q.ToPoco<TItem>()).ToArray());
            poco.WithId(dto.Id);

            return poco;
        }
    }
}
