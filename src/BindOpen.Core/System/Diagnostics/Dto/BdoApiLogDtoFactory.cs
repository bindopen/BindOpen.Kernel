using System.Linq;

namespace BindOpen.System.Diagnostics.Dto
{
    /// <summary>
    /// This class represents a factory for API log DTOs.
    /// </summary>
    public static class BdoApiLogDtoFactory
    {
        /// <summary>
        /// Converts the specified log to the Api log DTO.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="key">The key to consider.</param>
        /// <param name="alternateKey">The alternate key to consider.</param>
        /// <returns>Returns the Api log DTO.</returns>
        public static BdoApiLogDto ToApiDto(this IBdoLog log, string key = "*", string alternateKey = null)
            => log == null ? null : new BdoApiLogDto()
            {
                CreationDate = log.CreationDate,
                Description = log.Description?.GetContent(key, alternateKey),
                DisplayName = log.Title?.GetContent(key, alternateKey),
                Events = log.Events?.Select(q => q.ToApiDto()).ToList(),
                Id = log.Id,
                LastModificationDate = log.LastModificationDate,
                Name = log.Name
            };

        /// <summary>
        /// Converts the specified log to the Api log DTO.
        /// </summary>
        /// <param name="ev">The log to consider.</param>
        /// <param name="key">The key to consider.</param>
        /// <param name="alternateKey">The alternate key to consider.</param>
        /// <returns>Returns the Api log DTO.</returns>
        public static BdoApiLogEventDto ToApiDto(this IBdoLogEvent ev, string key = "*", string alternateKey = null)
        {
            return ev == null ? null : new BdoApiLogEventDto()
            {
                CreationDate = ev.CreationDate,
                Criticality = ev.Criticality,
                Date = ev.Date,
                Description = ev.Description?.GetContent(key, alternateKey),
                DisplayName = ev.Title?.GetContent(key, alternateKey),
                Id = ev.Id,
                Kind = ev.Kind,
                LastModificationDate = ev.LastModificationDate,
                Name = ev.Name,
                ResultCode = ev.ResultCode
            };
        }
    }
}