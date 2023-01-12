using System.Linq;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class BdoElementSetConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static BdoElementSetDto ToDto(this IBdoElementSet poco)
        {
            if (poco == null) return null;

            BdoElementSetDto dto = new()
            {
                CarrierElements = poco.Items?.Where(q=>q is IBdoMetaCarrier).Select(q=> (q as IBdoMetaCarrier).ToDto()).ToList(),
                Id = poco.Id,
                CollectionElements = poco.Items?.Where(q => q is IBdoMetaCollection).Select(q => (q as IBdoMetaCollection).ToDto()).ToList(),
                ObjectElements = poco.Items?.Where(q => q is IBdoMetaObject).Select(q => (q as IBdoMetaObject).ToDto()).ToList(),
                ScalarElements = poco.Items?.Where(q => q is IBdoMetaScalar).Select(q => (q as IBdoMetaScalar).ToDto()).ToList(),
                SourceElements = poco.Items?.Where(q => q is IBdoMetaSource).Select(q => (q as IBdoMetaSource).ToDto()).ToList(),
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoElementSet ToPoco(this BdoElementSetDto dto)
        {
            if (dto == null) return null;

            BdoMetaElementSet poco = new();
            poco.WithId(dto.Id);

            poco.Add(dto.CarrierElements?.Select(q => q.ToPoco()).ToArray());
            poco.Add(dto.CollectionElements?.Select(q => q.ToPoco()).ToArray());
            poco.Add(dto.ObjectElements?.Select(q => q.ToPoco()).ToArray());
            poco.Add(dto.ScalarElements?.Select(q => q.ToPoco()).ToArray());
            poco.Add(dto.SourceElements?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
