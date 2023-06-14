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
        public static DictionaryDto ToDto(this IBdoDictionary poco)
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
        public static IBdoDictionary ToPoco(this DictionaryDto dto)
        {
            if (dto == null) return null;

            BdoDictionary poco = BdoData.NewDictionary(dto.Values?.Select(q => q.ToPoco()).ToArray());
            poco.WithId(dto.Id);

            return poco;
        }
    }
}
