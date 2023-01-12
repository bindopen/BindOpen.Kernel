using BindOpen.Meta.Elements;
using BindOpen.Meta.Items;

namespace BindOpen.Meta.References
{
    /// <summary>
    /// This static class represents a data reference converter.
    /// </summary>
    public static class DataReferenceConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static DataReferenceDto ToDto(this IBdoReference poco)
        {
            if (poco == null) return null;

            DataReferenceDto dto = new()
            {
                CarrierElement = poco.SourceElement is IBdoMetaCarrier ? (poco.SourceElement as IBdoMetaCarrier)?.ToDto() : null,
                DataHandlerUniqueName = poco.DataHandlerUniqueName,
                PathDetail = poco.PathDetail?.ToDto(),
                SourceElement = poco.SourceElement is IBdoMetaSource ? (poco.SourceElement as IBdoMetaSource)?.ToDto() : null
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoReference ToPoco(this DataReferenceDto dto)
        {
            if (dto == null) return null;

            BdoReference poco = new()
            {
                DataHandlerUniqueName = dto.DataHandlerUniqueName,
                PathDetail = dto.PathDetail?.ToPoco(),
                SourceElement = dto.CarrierElement != null ? dto.CarrierElement.ToPoco() : dto.SourceElement?.ToPoco()
            };

            return poco;
        }
    }
}
