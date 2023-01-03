namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ObjectElementSpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ObjectElementSpecDto ToDto(this IObjectElementSpec poco)
        {
            if (poco == null) return null;

            ObjectElementSpecDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IObjectElementSpec ToPoco(this ObjectElementSpecDto dto)
        {
            if (dto == null) return null;

            ObjectElementSpec poco = new()
            {
            };

            return poco;
        }
    }
}
