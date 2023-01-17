namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaCollectionSpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaCollectionSpecDto ToDto(this IBdoMetaCollectionSpec poco)
        {
            if (poco == null) return null;

            MetaCollectionSpecDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaCollectionSpec ToPoco(this MetaCollectionSpecDto dto)
        {
            if (dto == null) return null;

            BdoMetaCollectionSpec poco = new()
            {
            };

            return poco;
        }
    }
}
