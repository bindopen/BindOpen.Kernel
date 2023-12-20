namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a IO converter of result items.
    /// </summary>
    public static class BdoResultItemConverter
    {
        /// <summary>
        /// Converts a result item poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ResultItemDto ToDto(
            this IResultItem poco)
        {
            if (poco == null) return null;

            var dto = new ResultItemDto()
            {
                Key = poco.Key,
                Status = poco.Status
            };

            return dto;
        }

        /// <summary>
        /// Converts a result item DTO to a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IResultItem ToPoco(
            this ResultItemDto dto)
        {
            if (dto == null) return null;

            var poco = new ResultItem()
            {
                Key = dto.Key,
                Status = dto.Status
            };

            return poco;
        }
    }
}
