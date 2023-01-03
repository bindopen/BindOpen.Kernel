namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class CollectionElementSpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static CollectionElementSpecDto ToDto(this ICollectionElementSpec poco)
        {
            if (poco == null) return null;

            CollectionElementSpecDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ICollectionElementSpec ToPoco(this CollectionElementSpecDto dto)
        {
            if (dto == null) return null;

            CollectionElementSpec poco = new()
            {
            };

            return poco;
        }
    }
}
