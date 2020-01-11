using System.Linq;

namespace BindOpen.Framework.System.Diagnostics.Dto
{
    /// <summary>
    /// This class represents a factory for API logs.
    /// </summary>
    public static class ApiBdoLogDtoFactory
    {
        /// <summary>
        /// Converts the specified log to the Api log DTO.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="key">The key to consider.</param>
        /// <param name="alternateKey">The alternate key to consider.</param>
        /// <returns>Returns the Api log DTO.</returns>
        public static ApiBdoLogDto ToApiDto(this IBdoLog log, string key = "*", string alternateKey = null)
            => log == null ? null : new ApiBdoLogDto()
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
        /// <param name="aEvent">The log to consider.</param>
        /// <param name="key">The key to consider.</param>
        /// <param name="alternateKey">The alternate key to consider.</param>
        /// <returns>Returns the Api log DTO.</returns>
        public static ApiBdoLogEventDto ToApiDto(this IBdoLogEvent aEvent, string key = "*", string alternateKey = null)
        {
            return aEvent == null ? null : new ApiBdoLogEventDto()
            {
                CreationDate = aEvent.CreationDate,
                Criticality = aEvent.Criticality,
                Date = aEvent.Date,
                Description = aEvent.Description?.GetContent(key, alternateKey),
                DisplayName = aEvent.Title?.GetContent(key, alternateKey),
                Id = aEvent.Id,
                Kind = aEvent.Kind,
                LastModificationDate = aEvent.LastModificationDate,
                Name = aEvent.Name,
                ResultCode = aEvent.ResultCode
            };
        }
    }
}