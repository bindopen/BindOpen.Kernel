using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Data.References;

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
                ItemizationMode = poco.ItemizationMode,
                DataReference = poco.Reference.ToDto(),
                DataExpression = poco.Expression.ToDto(),
                PropertySet = poco.PropertySet.ToDto(),
                ValueType = poco.ValueType
            };
            //dto.Specifications = poco.Specifications.Select(q => q.ToDto()).ToList(),

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaObject ToPoco(this MetaObjectDto dto)
        {
            if (dto == null) return null;

            BdoMetaObject poco = new()
            {
            };

            return poco;
        }
    }
}
