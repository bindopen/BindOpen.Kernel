using BindOpen.Data.Configuration;
using BindOpen.Extensions.Connecting;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Linq;

namespace BindOpen.Data.Items
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
        public static DatasourceDto ToDto(this IBdoDatasource poco)
        {
            if (poco == null) return null;

            DatasourceDto dto = new()
            {
                Configurations = poco.ConfigList?.Select(q => q?.ToDto()).ToList(),
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
        public static IBdoDatasource ConvertToPoco(
            this IBdoScope scope,
            DatasourceDto dto,
            IBdoLog log = null)
        {
            var poco = BdoData.NewDatasource(
                dto.Name,
                dto.Kind,
                dto.Configurations?.Select(q => scope.ConvertToPoco(q, log)).ToArray())
                .WithInstanceName(dto.InstanceName)
                .AsDefault(dto.IsDefault)
                .WithModuleName(dto.ModuleName)
                .WithId(dto.Id).WithName(dto.Name);

            return poco;
        }
    }
}
