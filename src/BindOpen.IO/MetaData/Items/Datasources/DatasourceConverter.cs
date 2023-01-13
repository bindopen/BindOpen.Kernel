using BindOpen.Extensions.Connecting;
using System.Linq;

namespace BindOpen.MetaData.Items
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class DatasourceConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static DatasourceDto ToDto(this IBdoDataSource poco)
        {
            if (poco == null) return null;

            DatasourceDto dto = new()
            {
                Configurations = poco.Configurations?.Select(q => q?.ToDto()).ToList(),
                Id = poco.Id,
                InstanceName = poco.InstanceName,
                IsDefault = poco.IsDefault,
                Kind = poco.Kind,
                ModuleName = poco.ModuleName,
                Name = poco.Name
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoDataSource ToPoco(this DatasourceDto dto)
        {
            var poco = BdoMeta.NewDatasource(
                dto.Name,
                dto.Kind,
                dto.Configurations?.Select(q => q?.ToPoco()).ToArray())
                .WithInstanceName(dto.InstanceName)
                .AsDefault(dto.IsDefault)
                .WithModuleName(dto.ModuleName)
                .WithId(dto.Id).WithName(dto.Name);

            return poco;
        }
    }
}
