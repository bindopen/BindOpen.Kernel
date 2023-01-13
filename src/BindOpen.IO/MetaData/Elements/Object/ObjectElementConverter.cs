namespace BindOpen.MetaData.Elements
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
        public static ObjectElementDto ToDto(this IBdoMetaObject poco)
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
        public static IBdoMetaObject ToPoco(this ObjectElementDto dto)
        {
            if (dto == null) return null;

            BdoMetaObject poco = new()
            {
            };

            return poco;
        }
    }
}
