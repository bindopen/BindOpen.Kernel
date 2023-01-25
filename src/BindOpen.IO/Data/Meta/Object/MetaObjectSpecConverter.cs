namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaObjectSpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaObjectSpecDto ToDto(this IBdoMetaObjectSpec poco)
        {
            if (poco == null) return null;

            MetaObjectSpecDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaObjectSpec ToPoco(this MetaObjectSpecDto dto)
        {
            if (dto == null) return null;

            BdoMetaObjectSpec poco = new()
            {
            };

            return poco;
        }
    }
}
