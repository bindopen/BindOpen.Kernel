using BindOpen.MetaData.Items;
using BindOpen.MetaData.References;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaElementConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaElementDto ToDto(this IBdoMetaElement poco)
        {
            if (poco == null) return null;

            MetaElementDto dto = null;

            if (poco is IBdoMetaCarrier carrier)
            {
                dto = carrier.ToDto();
            }
            else if (poco is IBdoMetaCollection collection)
            {
                dto = collection.ToDto();
            }
            else if (poco is IBdoMetaObject obj)
            {
                dto = obj.ToDto();
            }
            else if (poco is IBdoMetaScalar scalar)
            {
                dto = scalar.ToDto();
            }
            else if (poco is IBdoMetaSource source)
            {
                dto = source.ToDto();
            }

            if (dto != null)
            {
                dto.Description = poco.Description?.ToDto();
                dto.Detail = poco.Detail?.ToDto();
                dto.ItemReference = poco.ItemReference?.ToDto();
                dto.Title = poco.Title?.ToDto();
            }

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaElement ToPoco(this MetaElementDto dto)
        {
            if (dto == null) return null;

            BdoMetaElement poco = null;

            if (dto is MetaCarrierDto carrier)
            {
                return carrier.ToPoco();
            }
            else if (dto is MetaCollectionDto collection)
            {
                return collection.ToPoco();
            }
            else if (dto is MetaObjectDto obj)
            {
                return obj.ToPoco();
            }
            else if (dto is MetaScalarDto scalar)
            {
                return scalar.ToPoco();
            }
            else if (dto is MetaSourceDto source)
            {
                return source.ToPoco();
            }

            return poco;
        }
    }
}
