using System.Linq;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class TextDictionaryConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static TextDictionaryDto ToDto(this IBdoTextDictionary poco)
        {
            if (poco == null) return null;

            TextDictionaryDto dto = new()
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
        public static IBdoTextDictionary ToPoco(this TextDictionaryDto dto)
        {
            if (dto == null) return null;

            BdoTextDictionary poco = BdoData.NewDictionary(dto.Values?.Select(q => q.ToPoco()).ToArray());
            poco.WithId(dto.Id);

            return poco;
        }
    }
}
