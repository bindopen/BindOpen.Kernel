
namespace BindOpen.Data.Specification
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class DataSpecificationConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static DataSpecificationDto ToDto(this IDataSpecification poco)
        {
            if (poco == null) return null;

            DataSpecificationDto dto = default;

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IDataSpecification ToPoco(this DataSpecificationDto dto)
        {
            if (dto == null) return null;

            IDataSpecification poco = default;

            return poco;
        }
    }
}
