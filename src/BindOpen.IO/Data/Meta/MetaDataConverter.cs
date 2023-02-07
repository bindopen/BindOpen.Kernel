using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Data.References;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaDataConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaDataDto ToDto(
            this IBdoMetaData poco)
        {
            if (poco == null) return null;

            MetaDataDto dto = null;

            if (poco is IBdoMetaObject obj)
            {
                dto = obj.ToDto();
            }
            else if (poco is IBdoMetaScalar scalar)
            {
                dto = scalar.ToDto();
            }
            else if (poco is IBdoMetaList set)
            {
                dto = set.ToDto();
            }

            if (dto != null)
            {
                dto.DataExpression = poco.Expression?.ToDto();
                dto.DataReference = poco.Reference?.ToDto();
            }

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaData ToPoco(this MetaDataDto dto)
        {
            if (dto == null) return null;

            BdoMetaData poco = null;

            if (dto is MetaObjectDto obj)
            {
                return obj.ToPoco();
            }
            else if (dto is MetaScalarDto scalar)
            {
                return scalar.ToPoco();
            }
            else if (dto is MetaListDto set)
            {
                return set.ToPoco();
            }

            return poco;
        }
    }
}
