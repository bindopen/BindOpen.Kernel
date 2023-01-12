using System.Collections.Generic;

namespace BindOpen.Meta.Items
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class KeyValuePairConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static DataKeyValueDto ToDto(this KeyValuePair<string, string> poco)
        {
            DataKeyValueDto dto = new()
            {
                Content = poco.Value,
                Key = poco.Key
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static KeyValuePair<string, string> ToPoco(this DataKeyValueDto dto)
        {
            KeyValuePair<string, string> poco = new(dto?.Key, dto?.Content);

            return poco;
        }
    }
}
