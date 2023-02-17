using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaObjectConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaObjectDto ToDto(this IBdoMetaObject poco)
        {
            if (poco == null) return null;

            MetaObjectDto dto = new()
            {
                //Elements = poco.Elements?.Select(q => q.ToDto()).ToList(),
                //Index = poco.Index,
                ValueMode = poco.ValueMode,
                DataExpression = poco.DataExpression.ToDto(),
                //PropertySet = poco.PropertySet.ToDto(),
                ValueType = poco.DataValueType
            };
            //dto.Specifications = poco.Specifications.Select(q => q.ToDto()).ToList(),

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaObject ConvertToPoco(
            this IBdoScope scope,
            MetaObjectDto dto,
            IBdoLog log = null)
        {
            if (dto == null) return null;

            BdoMetaObject poco = new()
            {
            };

            return poco;
        }
    }
}
