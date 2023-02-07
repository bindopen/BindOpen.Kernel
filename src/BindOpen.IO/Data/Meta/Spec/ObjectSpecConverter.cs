namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ObjectSpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ObjectSpecDto ToDto(this IBdoObjectSpec poco)
        {
            if (poco == null) return null;

            ObjectSpecDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoObjectSpec ToPoco(this ObjectSpecDto dto)
        {
            if (dto == null) return null;

            BdoObjectSpec poco = new()
            {
            };

            return poco;
        }
    }
}
