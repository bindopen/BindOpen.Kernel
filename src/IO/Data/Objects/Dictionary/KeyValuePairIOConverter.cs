using BindOpen.Data.Helpers;
using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a IO converter of key value pairs.
    /// </summary>
    public static class KeyValuePairIOConverter
    {
        /// <summary>
        /// Converts a key value pair poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static KeyValuePairDto ToDto<TItem>(this KeyValuePair<string, TItem> poco)
        {
            KeyValuePairDto dto = new();
            dto.UpdateFromPoco(poco);

            return dto;
        }

        public static KeyValuePairDto UpdateFromPoco<TItem>(
            this KeyValuePairDto dto,
            KeyValuePair<string, TItem> poco)
        {
            if (dto == null) return null;

            var valueType = typeof(TItem).GetValueType();

            dto.Value = poco.Value.ToString(valueType);
            dto.Key = poco.Key;

            return dto;
        }

        /// <summary>
        /// Converts a key value pair DTO to a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static KeyValuePair<string, TItem> ToPoco<TItem>(this KeyValuePairDto dto)
        {
            var valueType = typeof(TItem).GetValueType();

            TItem item = dto == null ? default : dto.Value.ToObject(valueType).As<TItem>();

            KeyValuePair<string, TItem> poco = new(dto?.Key, item);

            return poco;
        }
    }
}
