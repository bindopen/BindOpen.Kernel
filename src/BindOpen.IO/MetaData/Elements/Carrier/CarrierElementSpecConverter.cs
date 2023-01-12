namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class CarrierElementSpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static CarrierElementSpecDto ToDto(this IBdoMetaCarrierSpec poco)
        {
            if (poco == null) return null;

            CarrierElementSpecDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaCarrierSpec ToPoco(this CarrierElementSpecDto dto)
        {
            if (dto == null) return null;

            BdoMetaCarrierSpec poco = new()
            {
            };

            return poco;
        }
    }
}
