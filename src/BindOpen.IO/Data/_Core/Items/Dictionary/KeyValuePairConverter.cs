using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class KeyValuePairConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static KeyValuePairDto ToDto(this KeyValuePair<string, string> poco)
        {
            KeyValuePairDto dto = new()
            {
                Value = poco.Value,
                Key = poco.Key
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static KeyValuePair<string, string> ToPoco(this KeyValuePairDto dto)
        {
            KeyValuePair<string, string> poco = new(dto?.Key, dto?.Value);

            return poco;
        }
    }
}
