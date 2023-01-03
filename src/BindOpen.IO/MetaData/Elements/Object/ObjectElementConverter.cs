namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ObjectElementConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ObjectElementDto ToDto(this IObjectElement poco)
        {
            if (poco == null) return null;

            ObjectElementDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IObjectElement ToPoco(this ObjectElementDto dto)
        {
            if (dto == null) return null;

            ObjectElement poco = new()
            {
            };

            return poco;
        }
    }
}
