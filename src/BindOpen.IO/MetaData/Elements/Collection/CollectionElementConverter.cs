using BindOpen.MetaData.References;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class CollectionElementConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static CollectionElementDto ToDto(this IBdoMetaCollection poco)
        {
            if (poco == null) return null;

            CollectionElementDto dto = new()
            {
                //Elements = poco.Elements?.Select(q => q.ToDto()).ToList(),
                //Index = poco.Index,
                ItemizationMode = poco.ItemizationMode,
                ItemReference = poco.ItemReference.ToDto(),
                ItemScript = poco.ItemScript,
                Detail = poco.Detail.ToDto(),
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
        public static IBdoMetaCollection ToPoco(this CollectionElementDto dto)
        {
            if (dto == null) return null;

            BdoMetaCollection poco = new()
            {
            };

            return poco;
        }
    }
}
