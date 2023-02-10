using BindOpen.Data.Items;
using BindOpen.Data.Meta;

namespace BindOpen.Data.References
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
                SourceMetaData = poco.Source?.ToDto(),
                DataHandlerUniqueName = poco.DataHandlerUniqueName,
                PathDetail = poco.PathDetail?.ToDto()
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
                Source = dto.SourceMetaData.ToPoco()
            };

            return poco;
        }
    }
}
