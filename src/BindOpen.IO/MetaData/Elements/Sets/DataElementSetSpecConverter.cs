namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class BdoElementSpecSetConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static BdoElementSpecSetDto ToDto(this IBdoElementSpecSet poco)
        {
            if (poco == null) return null;

            BdoElementSpecSetDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoElementSpecSet ToPoco(this BdoElementSpecSetDto dto)
        {
            if (dto == null) return null;

            BdoMetaElementSpecSet poco = new()
            {
            };

            return poco;
        }
    }
}
