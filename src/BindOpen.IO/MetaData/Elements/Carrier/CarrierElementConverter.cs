using System.Linq;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// This class represents a carrier element converter.
    /// </summary>
    public static class CarrierElementConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static CarrierElementDto ToDto(this IBdoMetaCarrier poco)
        {
            if (poco == null) return null;

            CarrierElementDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to POCO.
        /// </summary>
        /// <param name="dto">The dto to consider.</param>
        /// <returns>The POCO object.</returns>
        public static IBdoMetaCarrier ToPoco(this CarrierElementDto dto)
        {
            if (dto == null) return null;

            BdoMetaCarrier poco = new()
            {
            };

            return poco;
        }
    }
}
