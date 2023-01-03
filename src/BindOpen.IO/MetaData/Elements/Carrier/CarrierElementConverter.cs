using System.Linq;

namespace BindOpen.Data.Elements
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
        public static CarrierElementDto ToDto(this ICarrierElement poco)
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
        public static ICarrierElement ToPoco(this CarrierElementDto dto)
        {
            if (dto == null) return null;

            CarrierElement poco = new()
            {
            };

            return poco;
        }
    }
}
