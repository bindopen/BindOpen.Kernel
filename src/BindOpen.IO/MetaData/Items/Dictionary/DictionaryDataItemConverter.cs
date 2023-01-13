using System.Linq;

namespace BindOpen.MetaData.Items
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class DictionaryDataItemConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static DictionaryDataItemDto ToDto(this IBdoDictionary poco)
        {
            if (poco == null) return null;

            DictionaryDataItemDto dto = new()
            {
                Id = poco.Id,
                Values = poco?.Select(q => q.ToDto()).ToList()
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoDictionary ToPoco(this DictionaryDataItemDto dto)
        {
            if (dto == null) return null;

            BdoDictionary poco = BdoMeta.NewDictionary(dto.Values?.Select(q => q.ToPoco()).ToArray());
            poco.WithId(dto.Id);

            return poco;
        }
    }
}
