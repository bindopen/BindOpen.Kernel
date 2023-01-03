using System.Linq;

namespace BindOpen.Data.Elements
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
                CarrierElements = poco.Items?.Where(q=>q is ICarrierElement).Select(q=> (q as ICarrierElement).ToDto()).ToList(),
                Id = poco.Id,
                CollectionElements = poco.Items?.Where(q => q is ICollectionElement).Select(q => (q as ICollectionElement).ToDto()).ToList(),
                ObjectElements = poco.Items?.Where(q => q is IObjectElement).Select(q => (q as IObjectElement).ToDto()).ToList(),
                ScalarElements = poco.Items?.Where(q => q is IScalarElement).Select(q => (q as IScalarElement).ToDto()).ToList(),
                SourceElements = poco.Items?.Where(q => q is ISourceElement).Select(q => (q as ISourceElement).ToDto()).ToList(),
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

            BdoElementSet poco = new();
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
