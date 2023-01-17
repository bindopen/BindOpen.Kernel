namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaElementSpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaElementSpecDto ToDto(this IBdoMetaElementSpec poco)
        {
            if (poco == null) return null;

            MetaElementSpecDto dto = null;

            if (poco is IBdoMetaCarrierSpec carrierSpec)
            {
                return carrierSpec.ToDto();
            }
            else if (poco is IBdoMetaCollectionSpec collectionSpec)
            {
                return collectionSpec.ToDto();
            }
            else if (poco is IBdoMetaObjectSpec objSpec)
            {
                return objSpec.ToDto();
            }
            else if (poco is IBdoMetaScalarSpec scalarSpec)
            {
                return scalarSpec.ToDto();
            }
            else if (poco is IBdoMetaSourceSpec sourceSpec)
            {
                return sourceSpec.ToDto();
            }

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaElementSpec ToPoco(this MetaElementSpecDto dto)
        {
            if (dto == null) return null;

            BdoMetaElementSpec poco = null;

            if (dto is MetaCarrierSpecDto carrierSpec)
            {
                return carrierSpec.ToPoco();
            }
            else if (dto is MetaCollectionSpecDto collectionSpec)
            {
                return collectionSpec.ToPoco();
            }
            else if (dto is MetaObjectSpecDto objSpec)
            {
                return objSpec.ToPoco();
            }
            else if (dto is MetaScalarSpecDto scalarSpec)
            {
                return scalarSpec.ToPoco();
            }
            else if (dto is MetaSourceSpecDto sourceSpec)
            {
                return sourceSpec.ToPoco();
            }

            return poco;
        }
    }
}
