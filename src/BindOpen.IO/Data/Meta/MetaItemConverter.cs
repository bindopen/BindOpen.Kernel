namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaItemConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaItemDto ToDto(
            this IBdoMetaItem poco)
        {
            if (poco == null) return null;

            MetaItemDto dto = null;

            if (poco is IBdoMetaObject obj)
            {
                dto = obj.ToDto();
            }
            else if (poco is IBdoMetaScalar scalar)
            {
                dto = scalar.ToDto();
            }

            if (dto != null)
            {
            }

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaItem ToPoco(
            this MetaItemDto dto)
        {
            if (dto == null) return null;

            IBdoMetaItem poco = default;

            if (dto is MetaObjectDto obj)
            {
                return obj.ToPoco();
            }
            else if (dto is MetaScalarDto scalar)
            {
                return scalar.ToPoco();
            }
            else if (dto is MetaSetDto set)
            {
                return set.ToPoco();
            }

            return poco;
        }
    }
}
