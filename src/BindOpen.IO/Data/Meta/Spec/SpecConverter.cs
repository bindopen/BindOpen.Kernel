namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class SpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static SpecDto ToDto(this IBdoSpec poco)
        {
            if (poco == null) return null;

            SpecDto dto = null;

            if (poco is IBdoObjectSpec objSpec)
            {
                return objSpec.ToDto();
            }
            else if (poco is IBdoScalarSpec scalarSpec)
            {
                return scalarSpec.ToDto();
            }

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoSpec ToPoco(this SpecDto dto)
        {
            if (dto == null) return null;

            BdoSpec poco = null;

            if (dto is ObjectSpecDto objSpec)
            {
                return objSpec.ToPoco();
            }
            else if (dto is ScalarSpecDto scalarSpec)
            {
                return scalarSpec.ToPoco();
            }

            return poco;
        }
    }
}
