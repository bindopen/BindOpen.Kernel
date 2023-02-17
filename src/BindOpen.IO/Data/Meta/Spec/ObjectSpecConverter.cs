using BindOpen.Logging;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ObjectSpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ObjectSpecDto ToDto(this IBdoObjectSpec poco)
        {
            if (poco == null) return null;

            ObjectSpecDto dto = new()
            {
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoObjectSpec ConvertToPoco(
            this IBdoScope scope,
            ObjectSpecDto dto,
            IBdoLog log = null)
        {
            if (dto == null) return null;

            BdoObjectSpec poco = new()
            {
            };

            return poco;
        }
    }
}
