using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;

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
                dto.DataExpression = poco.DataExpression?.ToDto();
            }

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaData ConvertToPoco(
            this IBdoScope scope,
            MetaDataDto dto,
            IBdoLog log = null)
        {
            if (dto == null) return null;

            BdoMetaData poco = null;

            if (dto is MetaObjectDto obj)
            {
                return scope.ConvertToPoco(obj, log);
            }
            else if (dto is MetaScalarDto scalar)
            {
                return scope.ConvertToPoco(scalar, log);
            }
            else if (dto is MetaListDto set)
            {
                return scope.ConvertToPoco(set, log);
            }

            return poco;
        }
    }
}
