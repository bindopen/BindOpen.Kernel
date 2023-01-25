using System.Collections.Generic;

namespace BindOpen.Data.Items
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
        public static KeyValuePairDto ToDto(this KeyValuePair<string, string> poco)
        {
            KeyValuePairDto dto = new()
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
        public static KeyValuePair<string, string> ToPoco(this KeyValuePairDto dto)
        {
            KeyValuePair<string, string> poco = new(dto?.Key, dto?.Content);

            return poco;
        }
    }
}
