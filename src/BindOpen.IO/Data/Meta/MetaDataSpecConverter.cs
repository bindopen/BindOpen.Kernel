namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaDataSpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaDataSpecDto ToDto(this IBdoMetaSpec poco)
        {
            if (poco == null) return null;

            MetaDataSpecDto dto = null;

            if (poco is IBdoMetaObjectSpec objSpec)
            {
                return objSpec.ToDto();
            }
            else if (poco is IBdoMetaScalarSpec scalarSpec)
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
        public static IBdoMetaSpec ToPoco(this MetaDataSpecDto dto)
        {
            if (dto == null) return null;

            BdoMetaSpec poco = null;

            if (dto is MetaObjectSpecDto objSpec)
            {
                return objSpec.ToPoco();
            }
            else if (dto is MetaScalarSpecDto scalarSpec)
            {
                return scalarSpec.ToPoco();
            }

            return poco;
        }
    }
}
